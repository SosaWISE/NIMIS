


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

// ReSharper disable once CheckNamespace
namespace NXS.Data.Funding {
	public partial class NxseFundingDataStoredProcedureManager {
		public static StoredProcedure FE_PacketItemsViewByPacketID(int? PacketID) {
			StoredProcedure sp = new StoredProcedure("custFE_PacketItemsViewByPacketID" ,DataService.GetInstance("NxsFundingProvider"));
			sp.Command.AddParameter("@PacketID", PacketID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure FE_PacketsReadOpen() {
			StoredProcedure sp = new StoredProcedure("custFE_PacketsReadOpen" ,DataService.GetInstance("NxsFundingProvider"));
			return sp;
		}
	}
}
 