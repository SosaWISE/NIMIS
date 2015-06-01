using System;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SSE.FOS.AddressVerification.Interfaces;
using SSE.FOS.AddressVerification.Models;
using SSE.FOS.AddressVerification.Vendors;
using NXS.Lib;

namespace SSE.FOS.AddressVerification
{
    public class Main
	{
		#region .ctor

		public Main()
		{
			_vendorID = WebConfig.Instance.GetConfig("AddressVerification_Vendor");
			Initialize();
		}

	    public Main(string vendorID)
	    {
		    _vendorID = vendorID;
			Initialize();
	    }

	    #endregion .ctor

		#region Properties

	    private readonly string _vendorID;
	    public string VendorID
	    {
		    get { return _vendorID; }
	    }

	    private IVendor _avVendor;

		public static class Vendors
		{
			public const string STRIKE_IRON = "STRIKEIRON";
			public const string INTELI_SRCH = "INTELSEARCH";
			public const string SMARTY_STRT = "SMARTYSTREET";
		}

	    #endregion Properties

	    #region Methods

	    private void Initialize()
	    {
			switch (VendorID)
			{
				case Vendors.STRIKE_IRON:
					_avVendor = new StrikeIron();
					break;
				case Vendors.INTELI_SRCH:
					_avVendor = new IntelligentSearch();
					break;
				case Vendors.SMARTY_STRT:
					_avVendor = new SmartyStreets();
					break;
				default:
					throw new Exception(string.Format("The vendor set in config file ('{0}') is not supported.", VendorID));
			}
	    }

	    public IFosAVResult<IFosAddressVerified> VerifyAddress(IFosQlAddress address, int nAreaCode, int dealerId, int seasonId, int teamLocationId, string salesRepId, string userId)
	    {
			return _avVendor.VerifyAddress(address, nAreaCode, dealerId, seasonId, teamLocationId, salesRepId, userId);
	    }

		public static IFosAddressVerified BindTimeZone(QL_Address address, int nAreaCode, string userId)
		{
			// ** Calculate the timezone if it is missing
			if (address.TimeZoneId == (int) MC_PoliticalTimeZone.TimeZoneEnum.No_Zone)
			{
				#region Lookup TimeZone by State

				if (address.State != null && !string.IsNullOrEmpty(address.State.StateID))
				{
					MS_TimeZoneLookupCollection oTzLookUp =
						SosCrmDataContext.Instance.MS_TimeZoneLookups.GetByStateAB(address.State.StateAB);
					if (oTzLookUp.Count > 0)
					{
						MS_TimeZoneLookup oRw;
						if (oTzLookUp.Count == 1)
						{
							oRw = oTzLookUp[0];
						}
						else
						{
							oRw = oTzLookUp[0];
							foreach (MS_TimeZoneLookup oR in oTzLookUp)
							{
								if (int.Parse(oR.AreaCode) == nAreaCode)
								{
									oRw = oR;
								}
							}
						}

						// Assigne the correct timezone
						switch (oRw.GreenwichOffset)
						{
							case 0:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.No_Zone;
								break;
							case -10:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.HawaiiAleutian_Standard_Time;
								break;
							case -9:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.Alaska_Standard_Time;
								break;
							case -8:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.Pacific_Standard_Time;
								break;
							case -7:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.Mountain_Standard_Time;
								break;
							case -6:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.Central_Standard_Time;
								break;
							case -5:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.Eastern_Standard_Time;
								break;
							case -4:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.Atlantic_Standard_Time;
								break;
							default:
								address.TimeZoneId = (int) MC_PoliticalTimeZone.TimeZoneEnum.No_Zone;
								break;
						}
					}
				}

				#endregion Lookup TimeZone by State
			}

			// ** Save address
			address.Save(userId);

			// ** Create response object
			var result = new FosAddressVerified(address);

			// ** Return result.
			return result;

		}


	    #endregion Methods
	}
}
