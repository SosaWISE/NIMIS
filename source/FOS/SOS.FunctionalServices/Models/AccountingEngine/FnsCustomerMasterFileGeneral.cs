using System.Collections.Generic;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsCustomerMasterFileGeneral : IFnsCustomerMasterFileGeneral
	{
		#region .ctor

		public FnsCustomerMasterFileGeneral(AE_CustomerMasterFileGeneralView viewItem)
		{
			CustomerMasterFileID = viewItem.CustomerMasterFileID;
			FkId = viewItem.FkId;
			ResultType = viewItem.ResultType;
			Fullname = viewItem.Fullname;
			City = viewItem.City;
			Phone = viewItem.Phone;
			Email = viewItem.Email;

			/** Build list of Account Types. */
			AccountTypes = new List<string>();
			var aList = viewItem.AccountTypes.Substring(1).Split(';');  // This removes the first ';'.
			foreach (var atype in aList)
			{
				AccountTypes.Add(atype);
			}

		}

		#endregion .ctor

		#region Properties
		public long CustomerMasterFileID { get; private set; }
		public long? FkId { get; private set; }
		public string ResultType { get; private set; }
		public string Fullname { get; private set; }
		public string City { get; private set; }
		public string Phone { get; private set; }
		public string Email { get; private set; }
		public List<string> AccountTypes { get; private set; } 
		#endregion Properties
	}
}
