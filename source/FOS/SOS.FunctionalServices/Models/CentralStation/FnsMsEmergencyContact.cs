using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsEmergencyContact : IFnsMsEmergencyContact
	{
		#region .ctor

		public FnsMsEmergencyContact() {}

		public FnsMsEmergencyContact(Action<IFnsMsEmergencyContact, object> fxBindData, object value)
		{
			if (fxBindData == null)
				throw new NotImplementedException();

			fxBindData(this, value);
		}

		public FnsMsEmergencyContact(MS_EmergencyContact item)
		{
			EmergencyContactID = item.EmergencyContactID;
			EmergencyContactTypeId = item.EmergencyContactTypeId;
			CustomerId = item.CustomerId;
			AccountId = item.AccountId;
			RelationshipId = item.RelationshipId;
			AuthorityId = item.AuthorityId;
			OrderNumber = item.OrderNumber;
			Allergies = item.Allergies;
			MedicalConditions = item.MedicalConditions;
			HasKey = item.HasKey;
			DOB = item.DOB;
			Prefix = item.Prefix;
			FirstName = item.FirstName;
			MiddleName = item.MiddleName;
			LastName = item.LastName;
			Postfix = item.Postfix;
			Email = item.Email;
			Password = item.Password;
			Phone1 = item.Phone1;
			Phone1TypeId = item.Phone1TypeId;
			Phone2 = item.Phone2;
			Phone2TypeId = item.Phone2TypeId;
			Phone3 = item.Phone3;
			Phone3TypeId = item.Phone3TypeId;
			Comment1 = item.Comment1;
		}

		#endregion .ctor

		#region Properties
		public long EmergencyContactID { get; set; }
		public int EmergencyContactTypeId { get; set; }
		public long? CustomerId { get; set; }
		public long AccountId { get; set; }
		public int RelationshipId { get; set; }
		public int AuthorityId { get; set; }
		public short OrderNumber { get; set; }
		public string Allergies { get; set; }
		public string MedicalConditions { get; set; }
		public bool HasKey { get; set; }
		public DateTime? DOB { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Postfix { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone1 { get; set; }
		public int Phone1TypeId { get; set; }
		public string Phone2 { get; set; }
		public int? Phone2TypeId { get; set; }
		public string Phone3 { get; set; }
		public int? Phone3TypeId { get; set; }
		public string Comment1 { get; set; }
		
		#endregion Properties

		#region Methods
		public MS_EmergencyContact GetMsEMC()
		{
			/** Initialize */
			var result = new MS_EmergencyContact();

			if (EmergencyContactID > 0)
				result = SosCrmDataContext.Instance.MS_EmergencyContacts.LoadByPrimaryKey(EmergencyContactID);

			/** Databind. */
			//result.EmergencyContactID = EmergencyContactID;
			result.EmergencyContactTypeId = EmergencyContactTypeId;
			result.AuthorityId = AuthorityId;
			result.CustomerId = CustomerId;
			result.AccountId = AccountId;
			result.RelationshipId = RelationshipId;
			result.OrderNumber = OrderNumber;
			result.Allergies = Allergies;
			result.MedicalConditions = MedicalConditions;
			result.HasKey = HasKey;
			result.DOB = DOB;
			result.Prefix = Prefix;
			result.FirstName = FirstName;
			result.MiddleName = MiddleName;
			result.LastName = LastName;
			result.Postfix = Postfix;
			result.Email = Email;
			result.Password = Password;
			result.Phone1 = Phone1;
			result.Phone1TypeId = Phone1TypeId;
			result.Phone2 = Phone2;
			result.Phone2TypeId = Phone2TypeId;
			result.Phone3 = Phone3;
			result.Phone3TypeId = Phone3TypeId;
			result.Comment1 = Comment1;
			result.IsActive = true;
			result.IsDeleted = false;

			/** Return result. */
			return result;
		}
		#endregion Methods
	}
}
