using System;

namespace SOS.Lib.Core
{
	public interface IBusinessEntity
	{
		object PrimaryKeyValue { get; }
		event EventHandler Saved;
	}
}
