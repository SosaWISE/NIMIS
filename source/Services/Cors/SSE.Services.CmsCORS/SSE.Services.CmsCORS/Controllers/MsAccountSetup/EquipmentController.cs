using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Models.CentralStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv")]
	public class EquipmentController : ApiController
	{
		private IMonitoringStationService Service
		{
			get
			{
				return SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
			}
		}

		[Route("Equipments/{id}")]
		[HttpDelete]
		public Result<bool> Delete(long id)
		{
			return CORSSecurity.AuthorizeAny("Delete", null, null, user =>
			{
				var fnsResult = Service.EquipmentDelete(id, user.GPEmployeeID);
				return new Result<bool>().FromFnsResult(fnsResult);
			});
		}

		[Route("Equipments/")]
		[HttpPost]
		public Result<IFnsMsAccountEquipmentsView> Create([FromBody]FnsMsAccountEquipmentsView equipment)
		{
			return Update((equipment != null) ? equipment.AccountEquipmentID : 0, equipment);
		}
		[Route("Equipments/{id}")]
		[HttpPost]
		public Result<IFnsMsAccountEquipmentsView> Update(long id, [FromBody]FnsMsAccountEquipmentsView equipment)
		{
			return CORSSecurity.AuthorizeAny("Create", null, null, user =>
			{
				var argArray = new List<CORSArg>();
				if (equipment == null)
				{
					argArray.Add(new CORSArg(null, (true), "No values where passed."));
				}
				else
				{
					argArray.Add(new CORSArg("", (equipment.AccountId == 0), "AccountId is required."));
					argArray.Add(new CORSArg("", string.IsNullOrEmpty(equipment.EquipmentId), "EquipmentId is required."));
					argArray.Add(new CORSArg("", string.IsNullOrEmpty(equipment.Comments), "Comments is required."));
					argArray.Add(new CORSArg("", string.IsNullOrEmpty(equipment.Zone), "Zone is required."));
					argArray.Add(new CORSArg("", string.IsNullOrEmpty(equipment.AccountZoneTypeId), "AccountZoneTypeId is required."));
					argArray.Add(new CORSArg("", string.IsNullOrEmpty(equipment.AccountEquipmentUpgradeTypeId), "AccountEquipmentUpgradeTypeId is required."));
					//argArray.Add(new CORSArg("", (equipment.Price == 0), "Price is required."));
				}
				var result = new Result<IFnsMsAccountEquipmentsView>();
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsResult = Service.EquipmentUpdate(equipment, user.GPEmployeeID);
				return result.FromFnsResult(fnsResult);
			});
		}


		[Route("Equipments/{equipmentId}/EquipmentAccountZoneTypes")]
		[HttpGet]
		public Result<object> EquipmentAccountZoneTypes(string equipmentId)
		{
			return CORSSecurity.AuthorizeAny("EquipmentAccountZoneTypes", null, null, user =>
			{
				var fnsResult = Service.EquipmentAccountZoneTypes(equipmentId);
				return new Result<object>().FromFnsResult(fnsResult);
			});
		}
		[Route("Equipments/{equipmentId}/EquipmentAccountZoneTypeEvents")]
		[HttpGet]
		public Result<object> EquipmentAccountZoneTypeEvents(string equipmentId, int equipmentAccountZoneTypeId, string monitoringStationOSId)
		{
			return CORSSecurity.AuthorizeAny("EquipmentAccountZoneTypeEvents", null, null, user =>
			{
				var fnsResult = Service.EquipmentAccountZoneTypeEvents(equipmentId, equipmentAccountZoneTypeId, monitoringStationOSId);
				return new Result<object>().FromFnsResult(fnsResult);
			});
		}


		[Route("Equipments/{id}")]
		[HttpGet]
		public Result<object> ByEquipmentID(string id)
		{
			return CORSSecurity.AuthorizeAny("ByEquipmentID", null, null, user =>
			{
				var fnsResult = Service.EquipmentByEquipmentID(id);
				return new Result<object>().FromFnsResult(fnsResult);
			});
		}
		[Route("Equipments/{id}/ByPartNumber")]
		[HttpGet]
		public Result<object> ByPartNumber(string id)
		{
			return CORSSecurity.AuthorizeAny("ByPartNumber", null, null, user =>
			{
				var fnsResult = Service.EquipmentByPartNumber(id);
				return new Result<object>().FromFnsResult(fnsResult);
			});
		}
		[Route("Equipments/{id}/ByBarcode")]
		[HttpGet]
		public Result<object> ByBarcode(string id)
		{
			return CORSSecurity.AuthorizeAny("ByBarcode", null, null, user =>
			{
				var fnsResult = Service.EquipmentByBarcode(id);
				return new Result<object>().FromFnsResult(fnsResult);
			});
		}
	}
}