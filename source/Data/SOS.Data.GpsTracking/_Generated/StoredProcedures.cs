


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace SOS.Data.GpsTracking {
	public partial class GpsTrackingDataStoredProcedureManager {
		public static StoredProcedure GS_AccountGeoFenceCircleSave(long? GeoFenceID,long? AccountId,double? CenterLattitude,double? CenterLongitude,double? Radius,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFenceCircleSave" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@GeoFenceID", GeoFenceID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@CenterLattitude", CenterLattitude, DbType.Double);
			sp.Command.AddParameter("@CenterLongitude", CenterLongitude, DbType.Double);
			sp.Command.AddParameter("@Radius", Radius, DbType.Double);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFencePointDelete(long? GeoFenceID,long? AccountId,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFencePointDelete" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@GeoFenceID", GeoFenceID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFencePointRead(long? GeoFenceID,long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFencePointRead" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@GeoFenceID", GeoFenceID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFencePointSave(long? GeoFenceID,long? AccountId,string PlaceName,string PlaceDescription,double? Lattitude,double? Longitude,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFencePointSave" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@GeoFenceID", GeoFenceID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@PlaceName", PlaceName, DbType.String);
			sp.Command.AddParameter("@PlaceDescription", PlaceDescription, DbType.String);
			sp.Command.AddParameter("@Lattitude", Lattitude, DbType.Double);
			sp.Command.AddParameter("@Longitude", Longitude, DbType.Double);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFencePolygonSave(long? GeoFenceID,string GeoFenceTypeId,long? AccountId,string GeogCol1,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFencePolygonSave" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@GeoFenceID", GeoFenceID, DbType.Int64);
			sp.Command.AddParameter("@GeoFenceTypeId", GeoFenceTypeId, DbType.AnsiString);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@GeogCol1", GeogCol1, DbType.AnsiString);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFenceRectangleDelete(long? GeoFenceID,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFenceRectangleDelete" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@GeoFenceID", GeoFenceID, DbType.Int64);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFenceRectangleSave(long? GeoFenceID,long? AccountId,string ReportModeId,string GeoFenceName,string GeoFenceDescription,double? MaxLattitude,double? MinLongitude,double? MinLattitude,double? MaxLongitude,short? GoogleMapZoomLevel,string ModifiedBy) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFenceRectangleSave" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@GeoFenceID", GeoFenceID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@ReportModeId", ReportModeId, DbType.AnsiString);
			sp.Command.AddParameter("@GeoFenceName", GeoFenceName, DbType.String);
			sp.Command.AddParameter("@GeoFenceDescription", GeoFenceDescription, DbType.String);
			sp.Command.AddParameter("@MaxLattitude", MaxLattitude, DbType.Double);
			sp.Command.AddParameter("@MinLongitude", MinLongitude, DbType.Double);
			sp.Command.AddParameter("@MinLattitude", MinLattitude, DbType.Double);
			sp.Command.AddParameter("@MaxLongitude", MaxLongitude, DbType.Double);
			sp.Command.AddParameter("@GoogleMapZoomLevel", GoogleMapZoomLevel, DbType.Int16);
			sp.Command.AddParameter("@ModifiedBy", ModifiedBy, DbType.String);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFencesBindLaipacFence(long? CommandMessageID,long? AccountId) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFencesBindLaipacFence" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@CommandMessageID", CommandMessageID, DbType.Int64);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFencesByAccountID(long? AccountID) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFencesByAccountID" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure GS_AccountGeoFencesByCMFID(long? CMFID,long? CustomerID) {
			StoredProcedure sp = new StoredProcedure("custGS_AccountGeoFencesByCMFID" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@CMFID", CMFID, DbType.Int64);
			sp.Command.AddParameter("@CustomerID", CustomerID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure GS_EventsPagging(long? AccountID,DateTime? StartDate,DateTime? EndDate,int? PageSize,int? PageNumber) {
			StoredProcedure sp = new StoredProcedure("custGS_EventsPagging" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@PageSize", PageSize, DbType.Int32);
			sp.Command.AddParameter("@PageNumber", PageNumber, DbType.Int32);
			return sp;
		}
		public static StoredProcedure GS_EventsPaggingMaster(long? CustomerMasterFileID,long? CustomerId,DateTime? StartDate,DateTime? EndDate,int? PageSize,int? PageNumber) {
			StoredProcedure sp = new StoredProcedure("custGS_EventsPaggingMaster" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@CustomerMasterFileID", CustomerMasterFileID, DbType.Int64);
			sp.Command.AddParameter("@CustomerId", CustomerId, DbType.Int64);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@PageSize", PageSize, DbType.Int32);
			sp.Command.AddParameter("@PageNumber", PageNumber, DbType.Int32);
			return sp;
		}
		public static StoredProcedure GS_EventsReporting(long? CustomerMasterFileID,long? AccountID,string EventTypeID,long? GeoFenceID,DateTime? StartDate,DateTime? EndDate,int? PageSize,int? PageNumber) {
			StoredProcedure sp = new StoredProcedure("custGS_EventsReporting" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@CustomerMasterFileID", CustomerMasterFileID, DbType.Int64);
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			sp.Command.AddParameter("@EventTypeID", EventTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@GeoFenceID", GeoFenceID, DbType.Int64);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@PageSize", PageSize, DbType.Int32);
			sp.Command.AddParameter("@PageNumber", PageNumber, DbType.Int32);
			return sp;
		}
		public static StoredProcedure GS_EventTypeViewReadAll(string EventTypeID,string EventType) {
			StoredProcedure sp = new StoredProcedure("custGS_EventTypeViewReadAll" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@EventTypeID", EventTypeID, DbType.AnsiString);
			sp.Command.AddParameter("@EventType", EventType, DbType.String);
			return sp;
		}
		public static StoredProcedure KW_RequestGetQueue(int? AttemptNumberPerCmd) {
			StoredProcedure sp = new StoredProcedure("custKW_RequestGetQueue" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@AttemptNumberPerCmd", AttemptNumberPerCmd, DbType.Int32);
			return sp;
		}
		public static StoredProcedure KW_RequestIncrementAttempt(long? RequestID,int? IncrementBy) {
			StoredProcedure sp = new StoredProcedure("custKW_RequestIncrementAttempt" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@RequestID", RequestID, DbType.Int64);
			sp.Command.AddParameter("@IncrementBy", IncrementBy, DbType.Int32);
			return sp;
		}
		public static StoredProcedure KW_RequestProcess(long? RequestID) {
			StoredProcedure sp = new StoredProcedure("custKW_RequestProcess" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@RequestID", RequestID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure LP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID(long? UnitID,long? ReqCommandMessageId,string EventCodeId) {
			StoredProcedure sp = new StoredProcedure("custLP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@UnitID", UnitID, DbType.Int64);
			sp.Command.AddParameter("@ReqCommandMessageId", ReqCommandMessageId, DbType.Int64);
			sp.Command.AddParameter("@EventCodeId", EventCodeId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure LP_CommandMessageEAVRSP3ProcessByUnitID(long? UnitID) {
			StoredProcedure sp = new StoredProcedure("custLP_CommandMessageEAVRSP3ProcessByUnitID" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@UnitID", UnitID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure LP_CommandMessageEAVRSP4ProcessByUnitID(long? UnitID) {
			StoredProcedure sp = new StoredProcedure("custLP_CommandMessageEAVRSP4ProcessByUnitID" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@UnitID", UnitID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure LP_GsGeoFencesGetByGeoFenceId(long? GsGeoFenceId) {
			StoredProcedure sp = new StoredProcedure("custLP_GsGeoFencesGetByGeoFenceId" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@GsGeoFenceId", GsGeoFenceId, DbType.Int64);
			return sp;
		}
		public static StoredProcedure LP_GsGeoFencesGetNextAvailable(long? AccountID,long? UnitID,int? NumberOfFences) {
			StoredProcedure sp = new StoredProcedure("custLP_GsGeoFencesGetNextAvailable" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int64);
			sp.Command.AddParameter("@UnitID", UnitID, DbType.Int64);
			sp.Command.AddParameter("@NumberOfFences", NumberOfFences, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LP_RequestGetQueue(int? AttemptNumberPerCmd) {
			StoredProcedure sp = new StoredProcedure("custLP_RequestGetQueue" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@AttemptNumberPerCmd", AttemptNumberPerCmd, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LP_RequestIncrementAttempt(long? RequestID,int? IncrementBy) {
			StoredProcedure sp = new StoredProcedure("custLP_RequestIncrementAttempt" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@RequestID", RequestID, DbType.Int64);
			sp.Command.AddParameter("@IncrementBy", IncrementBy, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LP_RequestProcess(long? RequestID) {
			StoredProcedure sp = new StoredProcedure("custLP_RequestProcess" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@RequestID", RequestID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure SS_DeviceRequestGetQueue(int? AttemptNumberPerCmd) {
			StoredProcedure sp = new StoredProcedure("custSS_DeviceRequestGetQueue" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@AttemptNumberPerCmd", AttemptNumberPerCmd, DbType.Int32);
			return sp;
		}
		public static StoredProcedure SS_DeviceRequestIncrementAttempt(long? DeviceRequestID,int? IncrementBy) {
			StoredProcedure sp = new StoredProcedure("custSS_DeviceRequestIncrementAttempt" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@DeviceRequestID", DeviceRequestID, DbType.Int64);
			sp.Command.AddParameter("@IncrementBy", IncrementBy, DbType.Int32);
			return sp;
		}
		public static StoredProcedure SS_DeviceRequestProcess(long? DeviceRequestID) {
			StoredProcedure sp = new StoredProcedure("custSS_DeviceRequestProcess" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@DeviceRequestID", DeviceRequestID, DbType.Int64);
			return sp;
		}
		public static StoredProcedure Tbl_ObjectsGetByUserID(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custTbl_ObjectsGetByUserID" ,DataService.GetInstance("SosGpsTrackingProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
	}
}
 
