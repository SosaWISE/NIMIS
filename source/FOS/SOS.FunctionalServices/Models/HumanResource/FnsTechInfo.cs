using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using System;
using System.Collections.Generic;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsTechInfo : IFnsTechInfo
	{
		#region .ctor

		public FnsTechInfo(RU_User user, IEnumerable<RU_Season> seasons) : this(user)
		{
			// ** Iniitalize
			Seasons = new List<IFnsSeason>();

			foreach (var season in seasons)
			{
				Seasons.Add(new FnsSeason(season));	
			}
		}

		public FnsTechInfo(RU_User user)
		{
			UserID = user.UserID;
			CompanyID = user.GPEmployeeId;
			DefaultOfficeName = "[Not Yet Implemented]";
			PreferredName = user.PreferredName;
			FirstName = user.FirstName;
			LastName = user.LastName;
			CompanyName = user.CompanyName;
			UserName = user.UserName;
			BirthDate = user.BirthDate;
			HomeTown = user.HomeTown;
			Sex = user.StringSex;
			ShirtSize = user.StringShirtSize;
			HatSize = user.StringHatSize;
			PhoneHome = user.PhoneHome;
			PhoneCell = user.PhoneCell;
			Email = user.Email;
			SSN = user.SSNMask;

		}
		#endregion .ctor

		#region Properties

		public int UserID { get; set; }
		public string CompanyID { get; set; }
		public int TeamLocationId { get; set; }
		public string DefaultOfficeName { get; set; }
		public string PreferredName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CompanyName { get; set; }
		public string UserName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string HomeTown { get; set; }
		public string Sex { get; set; }
		public string ShirtSize { get; set; }
		public string HatSize { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneCell { get; set; }
		public string Email { get; set; }
		public string SSN { get; set; }
		public List<IFnsSeason> Seasons { get; set; }

		#endregion Properties
	}
}
