


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

// ReSharper disable once CheckNamespace
namespace NXS.Data.Funding {
	public partial class NxseFundingDataStoredProcedureManager {
		public static StoredProcedure FE_BundleItemsAdd(int? BundleId,int? PacketId,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custFE_BundleItemsAdd" ,DataService.GetInstance("NxsFundingProvider"));
			sp.Command.AddParameter("@BundleId", BundleId, DbType.Int32);
			sp.Command.AddParameter("@PacketId", PacketId, DbType.Int32);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure FE_BundleItemsViewByBundleID(int? BundleID) {
			StoredProcedure sp = new StoredProcedure("custFE_BundleItemsViewByBundleID" ,DataService.GetInstance("NxsFundingProvider"));
			sp.Command.AddParameter("@BundleID", BundleID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure FE_BundlesReadOpen() {
			StoredProcedure sp = new StoredProcedure("custFE_BundlesReadOpen" ,DataService.GetInstance("NxsFundingProvider"));
			return sp;
		}
		public static StoredProcedure FE_PacketItemsAdd(int? PacketId,int? AccountId,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custFE_PacketItemsAdd" ,DataService.GetInstance("NxsFundingProvider"));
			sp.Command.AddParameter("@PacketId", PacketId, DbType.Int32);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int32);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure FE_PacketItemsViewByPacketID(int? PacketID) {
			StoredProcedure sp = new StoredProcedure("custFE_PacketItemsViewByPacketID" ,DataService.GetInstance("NxsFundingProvider"));
			sp.Command.AddParameter("@PacketID", PacketID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure FE_PacketsReadOpen() {
			StoredProcedure sp = new StoredProcedure("custFE_PacketsReadOpen" ,DataService.GetInstance("NxsFundingProvider"));
			return sp;
		}
		public static StoredProcedure FE_PurchasedAccountAdd(int? PurchaseContractID,long? CMFID,string CSID,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custFE_PurchasedAccountAdd" ,DataService.GetInstance("NxsFundingProvider"));
			sp.Command.AddParameter("@PurchaseContractID", PurchaseContractID, DbType.Int32);
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			sp.Command.AddParameter("@CSID", CSID, DbType.AnsiString);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure FE_RejectedAccountsViewAddAccount(int? BundleID,long? CMFID,string CSID,int? AccountFundingStatusTypeId,int? RejectionTypeId,string AccountStatusNote,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custFE_RejectedAccountsViewAddAccount" ,DataService.GetInstance("NxsFundingProvider"));
			sp.Command.AddParameter("@BundleID", BundleID, DbType.Int32);
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			sp.Command.AddParameter("@CSID", CSID, DbType.AnsiString);
			sp.Command.AddParameter("@AccountFundingStatusTypeId", AccountFundingStatusTypeId, DbType.Int32);
			sp.Command.AddParameter("@RejectionTypeId", RejectionTypeId, DbType.Int32);
			sp.Command.AddParameter("@AccountStatusNote", AccountStatusNote, DbType.String);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure FE_SubmittedToPurchaserAccountSubmitABundle(int? BundleId,string CreatedBy) {
			StoredProcedure sp = new StoredProcedure("custFE_SubmittedToPurchaserAccountSubmitABundle" ,DataService.GetInstance("NxsFundingProvider"));
			sp.Command.AddParameter("@BundleId", BundleId, DbType.Int32);
			sp.Command.AddParameter("@CreatedBy", CreatedBy, DbType.AnsiString);
			return sp;
		}
	}
}
 