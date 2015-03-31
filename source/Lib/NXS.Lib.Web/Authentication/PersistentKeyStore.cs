using Nancy;
using Nancy.Authentication.Token.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NXS.Lib.Web.Authentication
{
	public delegate void SaveKeyValuesFunc(List<KeyValue> keyValueList);
	public delegate List<KeyValue> ReadKeyValuesFunc();
	public delegate void KeyValueAction();

	public class KeyValue
	{
		//public int ID;
		public DateTime CreatedOn;
		public byte[] Value;
	}

	/// <summary>
	/// Stores encryption keys in the file system
	/// </summary>
	public class PersistentKeyStore : ITokenKeyStore
	{
		SaveKeyValuesFunc _save;
		ReadKeyValuesFunc _read;

		public PersistentKeyStore(SaveKeyValuesFunc save, ReadKeyValuesFunc read)
		{
			_save = save;
			_read = read;
		}

		public IDictionary<DateTime, byte[]> Retrieve()
		{
			var keyValues = _read();
			var keyChain = new Dictionary<DateTime, byte[]>(keyValues.Count);
			foreach (var kv in keyValues)
			{
				keyChain.Add(kv.CreatedOn, kv.Value);
			}
			return keyChain;
		}

		public void Store(IDictionary<DateTime, byte[]> keys)
		{
			var keyValues = new List<KeyValue>();
			foreach (var k in keys)
			{
				var kv = new KeyValue();
				kv.Value = k.Value;
				kv.CreatedOn = k.Key;
				keyValues.Add(kv);
			}
			_save(keyValues);
		}

		public void Purge()
		{
			// save nothin
			_save(new List<KeyValue>());
		}
	}
}