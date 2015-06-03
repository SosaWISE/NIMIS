using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace NXS.Lib
{
	public class WebConfig
	{
		public static readonly string InheritEnv = "InheritEnv";
		public static readonly string Env = "Env";

		WebConfigDictionary _dict;
		private WebConfig(WebConfigDictionary dict)
		{
			this._dict = dict;
			// remove base env
			dict.Remove(InheritEnv);
		}

		public bool HasConfigValue(string key)
		{
			return this._dict.ContainsKey(key) && GetConfig(key) != null;
		}
		public string GetConfig(string key)
		{
			return _dict.GetConfig(key);
		}
		public bool GetBool(string key)
		{
			bool result;
			bool.TryParse(GetConfig(key), out result);
			return result;
		}
		public int GetInt(string key)
		{
			int result;
			int.TryParse(GetConfig(key), out result);
			return result;
		}

		static Exception _initError;
		static WebConfig _instance;
		public static WebConfig Instance
		{
			get
			{
				if (_initError != null)
					throw new Exception("Error initializing WebConfig", _initError);
				if (_instance == null)
					throw new Exception("WebConfig not initialized");
				return _instance;
			}
		}

		static object _lockObj = new object();
		public static void Init(string dir, Func<string, string> tryDecrypt = null, string filePrefix = "web")
		{
			lock (_lockObj)
			{
				_instance = null;
				_initError = null;
				try
				{
					_instance = Create(dir, filePrefix, tryDecrypt);
					_initError = null;
				}
				catch (Exception ex)
				{
					_initError = ex;
				}
			}
		}
		private static WebConfig Create(string dir, string filePrefix, Func<string, string> tryDecrypt = null)
		{
			string path;
			// get environment
			path = Path.Combine(dir, filePrefix + "env");
			var webenv = File.Exists(path) ? (File.ReadLines(path).FirstOrDefault() ?? "").Trim() : "";
			// default to `dbg`
			if (webenv == "") webenv = "dbg";

			var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			visited.Add(webenv);
			var configs = new Stack<WebConfigDictionary>();
			var topConfig = Parse(dir, filePrefix, webenv);
			while (!string.IsNullOrEmpty(webenv = topConfig.GetConfig(InheritEnv)))
			{
				if (visited.Contains(webenv))
					throw new Exception("Circular " + InheritEnv + " reference: " + webenv);
				visited.Add(webenv);
				// add to stack and parse next top config
				configs.Push(topConfig);
				topConfig = Parse(dir, filePrefix, webenv);
			}
			// apply overrides
			while (configs.Count > 0)
				topConfig.ApplyOverrides(configs.Pop());

			// decrypt
			if (tryDecrypt != null)
				topConfig = Decrypt(topConfig, tryDecrypt);

			return new WebConfig(topConfig);
		}
		private static WebConfigDictionary Parse(string dir, string filePrefix, string webenv)
		{
			webenv = webenv.ToLower();
			var path = Path.Combine(dir, filePrefix + "config." + webenv + ".json");
			if (!File.Exists(path))
				throw new Exception("No config at path: " + path);
			var json = File.ReadAllText(path);
			// parse
			var config = JsonConvert.DeserializeObject<WebConfigDictionary>(json);
			// set/overwrite environment variable
			if (!config.ContainsKey(Env))
				config.Add("Env", webenv);
			else
				config[Env] = webenv;
			return config;
		}
		private static WebConfigDictionary Decrypt(WebConfigDictionary configDict, Func<string, string> tryDecrypt)
		{
			var dict = new WebConfigDictionary(configDict.Count);
			foreach (var kvp in configDict)
			{
				dict[kvp.Key] = tryDecrypt(kvp.Value);
			}
			return dict;
		}
	}

	public class WebConfigDictionary : Dictionary<string, string>
	{
		//public WebConfigDictionary() : base(StringComparer.OrdinalIgnoreCase) { }
		//public WebConfigDictionary(int capacity) : base(capacity, StringComparer.OrdinalIgnoreCase) { }

		public WebConfigDictionary() { }
		public WebConfigDictionary(int capacity) : base(capacity) { }

		public void ApplyOverrides(WebConfigDictionary overrides)
		{
			// set overrides
			foreach (var kvp in overrides)
			{
				if (!this.ContainsKey(kvp.Key))
					this.Add(kvp.Key, kvp.Value);
				else
					this[kvp.Key] = kvp.Value;
			}
		}

		public string GetConfig(string key)
		{
			return (this.ContainsKey(key)) ? this[key] : null;
		}
	}
}
