using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using SOS.Lib.Core.ErrorHandling;

namespace NXS.Framework.Wpf.Settings
{
	[DataContract]
	public class UserSettingContainer
	{
		private DataContractSerializer Serializer { get; set; }

		[DataMember]
		public Dictionary<string, object> Settings { get; set; }

		public UserSettingContainer()
			: this(null)
		{
		}

		public UserSettingContainer(IEnumerable<Type> serializedTypes)
		{
			Settings = new Dictionary<string, object>();

			if (serializedTypes != null)
			{
				Serializer = new DataContractSerializer(typeof(UserSettingContainer), "settings", "", serializedTypes);
			}
			else
			{
				Serializer = new DataContractSerializer(typeof(UserSettingContainer), "settings", "");
			}
		}

		public T GetSettingValue<T>(string settingName)
		{
			T result = default(T);

			object value;
			if (Settings.TryGetValue(settingName, out value))
			{
				result = (T)Convert.ChangeType(value, typeof(T));
			}

			return result;
		}

		public void SetSettingValue<T>(string settingName, T value)
		{
			if (Settings.ContainsKey(settingName))
			{
				Settings[settingName] = value;
			}
			else
			{
				Settings.Add(settingName, value);
			}
		}

		public string Serialize(IErrorManager errorManager)
		{
			StringBuilder sob = new StringBuilder();

			try
			{
				using (StringWriter writer = new StringWriter(sob))
				{
					using (XmlWriter xWriter = new XmlTextWriter(writer))
					{
						Serializer.WriteObject(xWriter, this);
					}
				}
			}
			catch (Exception ex)
			{
				if (errorManager != null)
				{
					errorManager.AddCriticalMessage(ex);
				}
				else
				{
					throw ex;
				}
			}

			return sob.ToString();
		}

		public static UserSettingContainer Deserialize(string serializedString, IErrorManager errorManager)
		{
			return Deserialize(serializedString, null, errorManager);
		}

		public static UserSettingContainer Deserialize(string serializedString, IEnumerable<Type> serializedTypes, IErrorManager errorManager)
		{
			UserSettingContainer result = null;

			// Create the serializer
			DataContractSerializer serializer;
			if (serializedTypes != null)
			{
				serializer = new DataContractSerializer(typeof(UserSettingContainer), "settings", "", serializedTypes);
			}
			else
			{
				serializer = new DataContractSerializer(typeof(UserSettingContainer), "settings", "");
			}

			// Deserialize the value
			try
			{
				using (var reader = new StringReader(serializedString))
				{
					using (XmlReader xReader = new XmlTextReader(reader))
					{
						result = serializer.ReadObject(xReader) as UserSettingContainer;
					}
				}
			}
			catch (Exception ex)
			{
				if (errorManager != null)
				{
					errorManager.AddCriticalMessage(ex);
				}
				else
				{
					throw ex;
				}
			}

			if (result != null)
			{
				result.Serializer = serializer;
				if (result.Settings == null)
				{
					result.Settings = new Dictionary<string, object>();
				}
			}

			return result;
		}
	}
}