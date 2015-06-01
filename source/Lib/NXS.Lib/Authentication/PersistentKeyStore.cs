using Nancy;
using Nancy.Authentication.Token.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NXS.Lib.Authentication
{
	public delegate List<KeyValue> UpdateKeyValuesFunc(DateTime purgeExpiration, DateTime validExpiration, byte[] newKey);
	public delegate List<KeyValue> ReadKeyValuesFunc();
	public delegate void KeyValueAction();

	public class KeyValue
	{
		//public int ID;
		public DateTime CreatedOn;
		public byte[] Value;
	}

	/// <summary>
	/// Stores encryption keys
	/// </summary>
	public class PersistentKeyStore : IFarmTokenKeyStore
	{
		UpdateKeyValuesFunc _update;
		ReadKeyValuesFunc _read;

		public PersistentKeyStore(UpdateKeyValuesFunc update, ReadKeyValuesFunc read)
		{
			_update = update;
			_read = read;
		}

		public IDictionary<DateTime, byte[]> Retrieve()
		{
			return ConvertKeyValues(_read());
		}

		public IDictionary<DateTime, byte[]> Update(DateTime purgeExpiration, DateTime validExpiration, byte[] newKey)
		{
			return ConvertKeyValues(_update(purgeExpiration, validExpiration, newKey));
		}

		public void Purge()
		{
			_update(DateTime.MaxValue, DateTime.MaxValue, null);
		}


		private static IDictionary<DateTime, byte[]> ConvertKeyValues(List<KeyValue> keyValues)
		{
			var keyChain = new Dictionary<DateTime, byte[]>(keyValues.Count);
			foreach (var kv in keyValues)
			{
				keyChain.Add(kv.CreatedOn, kv.Value);
			}
			return keyChain;
		}
	}
}