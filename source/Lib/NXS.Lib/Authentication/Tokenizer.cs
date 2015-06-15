using Nancy;
using Nancy.Authentication.Token;
using Nancy.Authentication.Token.Storage;
using Nancy.ErrorHandling;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NXS.Lib.Authentication
{
	/// <summary>
	/// Default implementation of <see cref="ITokenizer"/>
	/// </summary>
	public class Tokenizer : ITokenizer
	{
		private readonly TokenValidator validator;
		private IFarmTokenKeyStore keyStore;// = new FileSystemTokenKeyStore();
		private Encoding encoding = Encoding.UTF8;
		private string claimsDelimiter = "|";
		private string hashDelimiter = ":";
		private string itemDelimiter = Environment.NewLine;
		private Func<DateTime> tokenStamp = () => DateTime.UtcNow;
		private Func<DateTime> now = () => DateTime.UtcNow;
		private Func<TimeSpan> tokenExpiration = () => TimeSpan.FromDays(1);
		private Func<TimeSpan> keyExpiration = () => TimeSpan.FromDays(7);

		private Func<NancyContext, string>[] additionalItems =
        {
            ctx => ctx.Request.Headers.UserAgent
        };

		/// <summary>
		/// Initializes a new instance of the <see cref="Tokenizer"/> class.
		/// </summary>
		public Tokenizer()
			: this(null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tokenizer"/> class.
		/// </summary>
		/// <param name="configuration">The configuration that should be used by the tokenizer.</param>
		public Tokenizer(Action<TokenizerConfigurator> configuration)
		{
			if (configuration != null)
			{
				var configurator = new TokenizerConfigurator(this);
				configuration.Invoke(configurator);
			}
			var keyRing = new TokenKeyRing(this);
			this.validator = new TokenValidator(keyRing);
		}

		/// <summary>
		/// Creates a token from a <see cref="IUserIdentity"/>.
		/// </summary>
		/// <param name="userIdentity">The user identity from which to create a token.</param>
		/// <param name="context">Current <see cref="NancyContext"/>.</param>
		/// <returns>The generated token.</returns>
		public string Tokenize(IUserIdentity userIdentity, NancyContext context)
		{
			var items = new List<string>
            {
                userIdentity.UserName,
                string.Join(this.claimsDelimiter, userIdentity.Claims),
                this.tokenStamp().Ticks.ToString(CultureInfo.InvariantCulture)
            };

			if (this.additionalItems != null)
			{
				foreach (var item in this.additionalItems.Select(additionalItem => additionalItem(context)))
				{
					if (string.IsNullOrWhiteSpace(item))
					{
						throw new RouteExecutionEarlyExitException(new Response { StatusCode = HttpStatusCode.Unauthorized });
					}
					items.Add(item);
				}
			}

			var message = string.Join(this.itemDelimiter, items);
			var token = CreateToken(message);
			return token;
		}

		/// <summary>
		/// Creates a <see cref="IUserIdentity"/> from a token.
		/// </summary>
		/// <param name="token">The token from which to create a user identity.</param>
		/// <param name="context">Current <see cref="NancyContext"/>.</param>
		/// <param name="userIdentityResolver">The user identity resolver.</param>
		/// <returns>The detokenized user identity.</returns>
		public IUserIdentity Detokenize(string token, NancyContext context, IUserIdentityResolver userIdentityResolver)
		{
			var tokenComponents = token.Split(new[] { this.hashDelimiter }, StringSplitOptions.None);
			if (tokenComponents.Length != 2)
			{
				return null;
			}

			byte[] messagebytes;
			byte[] hash;
			try
			{
				messagebytes = Convert.FromBase64String(tokenComponents[0]);
				hash = Convert.FromBase64String(tokenComponents[1]);
			}
			catch
			{
				return null;
			}

			if (!this.validator.IsValid(messagebytes, hash))
			{
				return null;
			}

			var items = this.encoding.GetString(messagebytes).Split(new[] { this.itemDelimiter }, StringSplitOptions.None);

			if (this.additionalItems != null)
			{
				var additionalItemCount = additionalItems.Count();
				for (var i = 0; i < additionalItemCount; i++)
				{
					var tokenizedValue = items[i + 3];
					var currentValue = additionalItems.ElementAt(i)(context);
					if (tokenizedValue != currentValue)
					{
						// todo: may need to log here as this probably indicates hacking
						return null;
					}
				}
			}

			var generatedOn = new DateTime(long.Parse(items[2]));

			if (tokenStamp() - generatedOn > tokenExpiration())
			{
				return null;
			}

			var userName = items[0];
			var claims = items[1].Split(new[] { this.claimsDelimiter }, StringSplitOptions.None);

			return userIdentityResolver.GetUser(userName, claims, context);
		}

		private string CreateToken(string message)
		{
			var messagebytes = this.encoding.GetBytes(message);
			var hash = this.validator.CreateHash(messagebytes);
			return Convert.ToBase64String(messagebytes) + this.hashDelimiter + Convert.ToBase64String(hash);
		}

		/// <summary>
		/// Provides an API for configuring a <see cref="Tokenizer"/> instance.
		/// </summary>
		public class TokenizerConfigurator
		{
			private readonly Tokenizer tokenizer;

			/// <summary>
			/// Initializes a new instance of the <see cref="TokenizerConfigurator"/> class.
			/// </summary>
			/// <param name="tokenizer"></param>
			public TokenizerConfigurator(Tokenizer tokenizer)
			{
				this.tokenizer = tokenizer;
			}

			/// <summary>
			/// Sets the token key store used by the tokenizer
			/// </summary>
			/// <param name="store"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator WithKeyCache(IFarmTokenKeyStore store)
			{
				this.tokenizer.keyStore = store;
				return this;
			}

			/// <summary>
			/// Sets the encoding used by the tokenizer
			/// </summary>
			/// <param name="encoding"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator Encoding(Encoding encoding)
			{
				this.tokenizer.encoding = encoding;
				return this;
			}

			/// <summary>
			/// Sets the delimiter between document and its hash
			/// </summary>
			/// <param name="hashDelimiter"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator HashDelimiter(string hashDelimiter)
			{
				this.tokenizer.hashDelimiter = hashDelimiter;
				return this;
			}

			/// <summary>
			/// Sets the delimiter between each item to be tokenized
			/// </summary>
			/// <param name="itemDelimiter"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator ItemDelimiter(string itemDelimiter)
			{
				this.tokenizer.itemDelimiter = itemDelimiter;
				return this;
			}

			/// <summary>
			/// Sets the delimiter between each claim
			/// </summary>
			/// <param name="claimsDelimiter"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator ClaimsDelimiter(string claimsDelimiter)
			{
				this.tokenizer.claimsDelimiter = claimsDelimiter;
				return this;
			}

			/// <summary>
			/// Sets the token expiration interval. An expired token will cause a user to become unauthorized (logged out). 
			/// Suggested value is 1 day (which is also the default).
			/// </summary>
			/// <param name="expiration"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator TokenExpiration(Func<TimeSpan> expiration)
			{
				this.tokenizer.tokenExpiration = expiration;

				if (this.tokenizer.tokenExpiration() >= this.tokenizer.keyExpiration())
				{
					int t = 0;
					//throw new ArgumentException("Token expiration must be less than key expiration", "expiration");
				}

				return this;
			}

			/// <summary>
			/// Sets the key expiration interval. Must be longer than the <see cref="TokenizerConfigurator.TokenExpiration"/> value. 
			/// When keys expire, they are scheduled to purge once any tokens they have been used to generate have expired.
			/// Suggested range is 2 to 14 days. The default is 7 days.
			/// </summary>
			/// <param name="expiration"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator KeyExpiration(Func<TimeSpan> expiration)
			{
				this.tokenizer.keyExpiration = expiration;

				if (this.tokenizer.tokenExpiration() >= this.tokenizer.keyExpiration())
				{
					int t = 0;
					//throw new ArgumentException("Key expiration must be greater than token expiration", "expiration");
				}

				return this;
			}

			/// <summary>
			/// Sets the token-generated-at timestamp
			/// </summary>
			/// <param name="tokenStamp"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator TokenStamp(Func<DateTime> tokenStamp)
			{
				this.tokenizer.tokenStamp = tokenStamp;
				return this;
			}

			/// <summary>
			/// Sets the current date/time.
			/// </summary>
			/// <param name="now"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator Now(Func<DateTime> now)
			{
				this.tokenizer.now = now;
				return this;
			}

			/// <summary>
			/// Sets any additional items to be included when tokenizing. The default includes Request.Headers.UserAgent.
			/// </summary>
			/// <param name="additionalItems"></param>
			/// <returns>A reference to the current <see cref="TokenizerConfigurator"/></returns>
			public TokenizerConfigurator AdditionalItems(params Func<NancyContext, string>[] additionalItems)
			{
				this.tokenizer.additionalItems = additionalItems;
				return this;
			}
		}

		private class TokenValidator
		{
			private readonly TokenKeyRing keyRing;

			internal TokenValidator(TokenKeyRing keyRing)
			{
				this.keyRing = keyRing;
			}

			public bool IsValid(byte[] message, byte[] hash)
			{
				return this.keyRing
						   .AllKeys()
						   .Select(key => GenerateHash(key, message))
						   .Any(hash.SequenceEqual);
			}

			public byte[] CreateHash(byte[] message)
			{
				var key = this.keyRing
							  .NonExpiredKeys()
							  .First();

				return GenerateHash(key, message);
			}

			private byte[] GenerateHash(byte[] key, byte[] message)
			{
				using (var hmac = new HMACSHA256(key))
				{
					return hmac.ComputeHash(message);
				}
			}
		}

		private class TokenKeyRing
		{
			private readonly Tokenizer tokenizer;

			private IDictionary<DateTime, byte[]> keys;

			internal TokenKeyRing(Tokenizer tokenizer)
			{
				this.tokenizer = tokenizer;
				keys = this.tokenizer.keyStore.Retrieve();
			}

			public IEnumerable<byte[]> AllKeys()
			{
				return this.Keys(true);
			}

			public IEnumerable<byte[]> NonExpiredKeys()
			{
				return this.Keys(false);
			}

			private static object keysLock = new object();
			private IEnumerable<byte[]> Keys(bool includeExpired)
			{
				var now = this.tokenizer.now();
				var validExpiration = now.Subtract(this.tokenizer.keyExpiration());
				var purgeExpiration = validExpiration.Subtract(this.tokenizer.tokenExpiration());
				if (NeedsUpdate(purgeExpiration, validExpiration, includeExpired))
				{
					lock (keysLock)
					{
						// check if it still needs to update
						if (NeedsUpdate(purgeExpiration, validExpiration, includeExpired))
						{
							// update and get keys from store
							this.keys = this.tokenizer.keyStore.Update(purgeExpiration, validExpiration, CreateKey());
						}
					}
				}

				var validKeys = new List<byte[]>();
				foreach (var entry in this.keys.OrderByDescending(x => x.Key))
				{
					if (includeExpired || !(entry.Key < validExpiration))
						validKeys.Add(entry.Value);
				}
				return validKeys;
			}

			private bool NeedsUpdate(DateTime purgeExpiration, DateTime validExpiration, bool includeExpired)
			{
				var hasEntryToPurge = false;
				var hasValidKey = false;

				foreach (var entry in this.keys.OrderByDescending(x => x.Key))
				{
					if (entry.Key < purgeExpiration)
					{
						hasEntryToPurge = true;
						break;
					}
					else if (includeExpired || !(entry.Key < validExpiration))
						hasValidKey = true;
				}

				return (hasEntryToPurge || !hasValidKey);
			}

			private byte[] CreateKey()
			{
				var secretKey = new byte[64];

				using (var rng = new RNGCryptoServiceProvider())
				{
					rng.GetBytes(secretKey);
				}

				return secretKey;
			}
		}
	}
}
