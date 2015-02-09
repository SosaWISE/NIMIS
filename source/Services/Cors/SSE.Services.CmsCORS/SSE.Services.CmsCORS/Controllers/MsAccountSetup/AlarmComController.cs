using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.Services.Interfaces.Models.CellStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv/AlarmCom")]
	public class AlarmComController : ApiController
	{
		private ICellStationService Service
		{
			get { return SosServiceEngine.Instance.FunctionalServices.Instance<ICellStationService>(); }
		}

		[HttpPost, Route("{id}/Register")]
		public Result<object> Register(int id, [FromBody]RegisterPost data)
		{
			return CORSSecurity.Authorize("Register", null, null, user =>
			{
				var validateResult = CORSArg.ArgumentValidation<object>(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'AccountId' was not passed."),
                    new CORSArg("", (data==null || string.IsNullOrEmpty(data.SerialNumber)), "'SerialNumber' was not passed."),
                });
				if (validateResult.Failure) return validateResult;
				return Service.Register(id, data.SerialNumber, data.EnableTwoWay);
			});
		}

		[HttpGet, Route("{id}/EquipmentList")]
		public Result<object> EquipmentList(int id)
		{
			return CORSSecurity.Authorize("EquipmentList", null, null, user =>
			{
				var validateResult = CORSArg.ArgumentValidation<object>(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'AccountId' was not passed."),
                });
				if (validateResult.Failure) return validateResult;
				return Service.GetEquipmentList(id);
			});
		}

		[HttpPost, Route("{id}/SwapModem")]
		public Result<bool> SwapModem(int id, [FromBody]SwapModemPost data)
		{
			return CORSSecurity.Authorize("SwapModem", null, null, user =>
			{
				var validateResult = CORSArg.ArgumentValidation<bool>(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'AccountId' was not passed."),
                    new CORSArg("", (data == null || string.IsNullOrWhiteSpace(data.NewSerialNumber)), "Invalid 'NewSerialNumber'"),
                });
				if (validateResult.Failure) return validateResult;
				return Service.SwapModem(id, data.NewSerialNumber, data.SwapReason, data.SpecialRequest, data.RestoreBackedUpSettingsAfterSwap);
			});
		}

		[HttpPost, Route("{id}/Unregister")]
		public Result<bool> Unregister(int id)
		{
			return CORSSecurity.Authorize("Unregister", null, null, user =>
			{
				var validateResult = CORSArg.ArgumentValidation<bool>(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'AccountId' was not passed."),
                });
				if (validateResult.Failure) return validateResult;
				return Service.Unregister(id);
			});
		}

		[HttpGet, Route("{id}/AccountStatus")]
		public Result<object> AccountStatus(int id)
		{
			return CORSSecurity.Authorize("AccountStatus", null, null, user =>
			{
				var validateResult = CORSArg.ArgumentValidation<object>(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'AccountId' was not passed."),
                });
				if (validateResult.Failure) return validateResult;
				return Service.AccountStatus(id);
			});
		}

		[HttpPost, Route("{id}/ChangeServicePackage")]
		public Result<bool> ChangeServicePackage(int id, string cellPackageItemId)
		{
			return CORSSecurity.Authorize("ChangeServicePackage", null, null, user =>
			{
				var validateResult = CORSArg.ArgumentValidation<bool>(new List<CORSArg>() {
                    new CORSArg("", (id == 0), "'AccountId' was not passed."),
                    new CORSArg("", string.IsNullOrEmpty(cellPackageItemId ), "'CellPackageItemId' was not passed."),
                });
				if (validateResult.Failure) return validateResult;
				return Service.ChangeServicePackage(id, user.GPEmployeeID, newCellPackageItemId: cellPackageItemId);
			});
		}
	}
}
