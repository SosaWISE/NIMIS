using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace NXS.Lib.Web
{
	public class WebConfigDictionary : Dictionary<string, string>
	{
		//public WebConfigDictionary() : base(StringComparer.OrdinalIgnoreCase) { }
		//public WebConfigDictionary(int capacity) : base(capacity, StringComparer.OrdinalIgnoreCase) { }

		public WebConfigDictionary() { }
		public WebConfigDictionary(int capacity) : base(capacity) { }
	}

	public class WebConfig
	{
		WebConfigDictionary _dict;
		private WebConfig(WebConfigDictionary dict)
		{
			this._dict = dict;
		}

		public string GetConfig(string key)
		{
			if (_dict.ContainsKey(key))
			{
				return _dict[key];
			}
			else
			{
				return null;
			}
		}

		static Exception _initError;
		static WebConfig _instance;
		public static WebConfig Current
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
		public static void Init(string dir, Func<string, string> tryDecrypt)
		{
			lock (_lockObj)
			{
				if (_instance == null)
				{
					try
					{
						_instance = Create(dir, tryDecrypt);
						_initError = null;
					}
					catch (Exception ex)
					{
						_initError = ex;
					}
				}
			}
		}
		public static WebConfig Create(string dir, Func<string, string> tryDecrypt)
		{
			string path;
			// get environment (default to `debug` if the file is not there)
			path = Path.Combine(dir, "webenv");
			var env = File.Exists(path) ? File.ReadLines(path).First().Trim() : "debug";

			// read default json
			var defaultJson = ReadFile(Path.Combine(dir, "webconfig.json"));
			// read environment json
			var envJson = ReadFile(Path.Combine(dir, "webconfig." + env + ".json"));

			return new WebConfig(Parse(defaultJson, envJson, tryDecrypt));
		}
		private static string ReadFile(string path)
		{
			if (!File.Exists(path))
				throw new Exception("Failed to read webconfig path: " + path);
			return File.ReadAllText(path);
		}
		public static WebConfigDictionary Parse(string defaultJson, string overrideJson, Func<string, string> tryDecrypt)
		{
			// load default
			var defaultDict = JsonConvert.DeserializeObject<WebConfigDictionary>(defaultJson);
			// load for environment
			var overrideDict = JsonConvert.DeserializeObject<WebConfigDictionary>(overrideJson);

			// set overrides
			foreach (var kvp in overrideDict)
			{
				if (!defaultDict.ContainsKey(kvp.Key))
				{
					defaultDict.Add(kvp.Key, kvp.Value);
				}
				else
				{
					defaultDict[kvp.Key] = kvp.Value;
				}
			}

			// decrypt
			var dict = new WebConfigDictionary(defaultDict.Count);
			foreach (var kvp in defaultDict)
			{
				dict[kvp.Key] = tryDecrypt(kvp.Value);
			}
			return dict;
		}
	}
}
