using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Swing;

namespace SOS.FunctionalServices.Models.Swing
{
	public class FnsCustomerSwingInfo : IFnsCustomerSwingInfo
	{
		#region .ctor

		public FnsCustomerSwingInfo() {}

		public FnsCustomerSwingInfo(Action<IFnsCustomerSwingInfo, object> fxBindData, object value)
		{
			if (fxBindData == null)
				throw new NotImplementedException();

			fxBindData(this, value);
		}
        
        public FnsCustomerSwingInfo(AE_CustomerSWINGView item)
		{
            Salutation = item.Salutation;
			FirstName = item.FirstName;
			MiddleName = item.MiddleName;
			LastName = item.LastName;
            Email = item.Email;
            Suffix = item.Suffix;
            SSN = item.SSN;
            DOB = item.DOB;
		}
        
		#endregion .ctor

		#region Properties
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string SSN { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
	    	
		#endregion Properties
        //#region Methods
        //public MS_EmergencyContact GetMsEMC()
        //{
        //    /** Initialize */
        //    var result = new MS_EmergencyContact();

        //    if (EmergencyContactID > 0)
        //        result = SosCrmDataContext.Instance.MS_EmergencyContacts.LoadByPrimaryKey(EmergencyContactID);

        //    /** Databind. */
        //    //result.EmergencyContactID = EmergencyContactID;
        //    result.CustomerId = CustomerId;
        //    result.AccountId = AccountId;
        //    result.RelationshipId = RelationshipId;
        //    result.OrderNumber = OrderNumber;
        //    result.Allergies = Allergies;
        //    result.MedicalConditions = MedicalConditions;
        //    result.HasKey = HasKey;
        //    result.DOB = DOB;
        //    result.Prefix = Prefix;
        //    result.FirstName = FirstName;
        //    result.MiddleName = MiddleName;
        //    result.LastName = LastName;
        //    result.Postfix = Postfix;
        //    result.Email = Email;
        //    result.Password = Password;
        //    result.Phone1 = Phone1;
        //    result.Phone1TypeId = Phone1TypeId;
        //    result.Phone2 = Phone2;
        //    result.Phone2TypeId = Phone2TypeId;
        //    result.Phone3 = Phone3;
        //    result.Phone3TypeId = Phone3TypeId;
        //    result.Comment1 = Comment1;
        //    result.IsActive = true;
        //    result.IsDeleted = false;

        //    /** Return result. */
        //    return result;
        //}
        //#endregion Methods
	}
}
