using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Logic.MonitoringStations;
using NXS.Logic.MonitoringStations.Helpers;
using NXS.Logic.MonitoringStations.Models;

namespace NXS.Logic.MonitoringStationsUT
{
	[TestClass]
	public class OnBoardTests
	{
		#region Properteis

		private const string _USERID = "wsi_828070003";
		private const string _PASWRD = "password";
		private const string _SERVICE_NO = "828070003";
		private const string _CS_NO_DIGITAL_PRIMARY = "768247002";
		private const string _RECEIVER_NUMBER = "8446913961";

		#endregion Properteis

		#region Test Methods

		[TestMethod]
		public void TestSerialization()
		{
			#region Setup Account
			var acct = new Account
			{
				SiteSystems = new List<SiteSystem>
				{
					new SiteSystem
					{
						SiteName = "Andres Home",
						SiteAddr1 = "1184 N"
					}

				},
				Zones = new List<Zone>
				{
					new Zone
					{
						EquipmentLocationId = "FrontDoor",
						EquipmentTypeId = "Motion",
						EventId = "343",
						ZoneId = "002",
						ZoneStateId = "A"
					}
				},
				SiteAgencyPermits = new List<SiteAgencyPermit>
				{
					new SiteAgencyPermit
					{
						AgencyTypeId = "PD",
						Phone1 = "8016549877",
						PermTypeId = "F",
						PermitNo = "12345",
						EffectiveDate = "12/10/2013",
						ExpireDate = "12/10/2020"
					}
				},
				Contacts = new List<Contact>
				{
					new Contact
					{
						FirstName = "Andres",
						LastName = "Sosa",
					}
				},
				SiteOptions = new List<SiteOption>
				{
					new SiteOption
					{
						OptionId = "CMPUR",
						OptionValue = "PUR"
					},
					new SiteOption
					{
						OptionId = "CONTRLEN",
						OptionValue = "12"
					}
				},
				SiteSystemOptions = new List<SiteSystemOption>
				{
					new SiteSystemOption
					{
						OptionId = "DSL-VOIP",
						OptionValue = "VOIP"
					},
					new SiteSystemOption
					{
						OptionId = "INST CODE",
						OptionValue = "4444"
					}
				},
				SiteGeneralDispatches = new List<SiteGeneralDispatches>
				{
					new SiteGeneralDispatches
					{
						Instructions = "This is one instruction"
					},
					new SiteGeneralDispatches
					{
						Instructions = "This is another instruction"
					}
				}
			};
			#endregion Setup Account

			// Convert to XML.
			var xmlizedString = acct.Serialize();

			Assert.IsTrue(!string.IsNullOrEmpty(xmlizedString), "This string is not right.");
		}

		[TestMethod]
		public void TestAccountOnboarding()
		{
			// ** Inititalize.
			#region Setup Account

			var rand1 = new Random();
			var rand2 = new Random();
		    var acct = new Account
		    {
		        #region SiteSystem
		        SiteSystems = new List<SiteSystem>
		        {

		            new SiteSystem
		            {
		                SiteName = "WANDA/JOHN SMITH",
		                SiteAddr1 = "25 second ave",
		                CityName = "OREM",
		                StateId = "UT",
		                CountyName = "UTAH",
		                ZipCode = "840973446",
		                Phone1 = "9722437440",
		                SiteTypeId = "RBFM",
		                SiteStateId = "C",
		                CodeWord1 = "JUMANJI",
		                CsPartNo = "300",
		                CrossStreet = "Cross Ave",
		                PanelLocation = "Master Closet",
		                InstallDate = "8/1/2012",
		                PanelCode = "ABCD1234",
		                PanelPhone = "9722437443",
		                ReceiverPhone = _RECEIVER_NUMBER,
		                SysTypeId = "A1S001",
		                ServiceNo = _SERVICE_NO,
		                InstallServiceNo = _SERVICE_NO

		            }

		        },

		        #endregion SiteSystem

		        #region Contacts
		        Contacts = new List<Contact>
		        {
		            #region Contact Wanda
		            new Contact //Wanda Smith

		            {


		                FirstName = "WaNdA",
		                LastName = "Smith",
		                ContactTypeId = "MON",
		                RelationId = "Own",
		                AuthId = "full",
		                ContractSignerFlag = "Y",
		                HasKeyFlag = "Y",
		                Phone1 = "9722437441",
		                PhoneTypeId1 = "WK",
		                ContlTypeNo = "5000"
		            },

		            #endregion Contact Wanda

		            #region Contact John
		            new Contact //John Smith

		            {
		                FirstName = "John",
		                LastName = "Smith",
		                ContactTypeId = "MON",
		                RelationId = "OWN",
		                AuthId = "FULL",
		                HasKeyFlag = "Y",
		                Phone1 = "9722437442",
		                PhoneTypeId1 = "WK"

		            },

		            #endregion Contact John

		            #region Contact Dispatch
		            new Contact // DISPATCH IMMEDIATELY
		            {
		                FirstName = "DISPATCH",
		                LastName = "IMMEDIATELY",
		                ContactTypeId = "DURS",
		                RelationId = "OWN",
		                AuthId = "DURS",
		                Pin = "18913"

		            },

		            #endregion Contact Dispatch

		            #region Contact First
		            new Contact // First Contact
		            {
		                FirstName = "First",
		                LastName = "Contact",
		                ContactTypeId = "MON",
		                RelationId = "OWN",
		                AuthId = "FULL",
		                Pin = "12345",
		                ContractSignerFlag = "Y",
		                HasKeyFlag = "Y",
		                Phone1 = "9722203333",
		                PhoneTypeId1 = "CL",
		                EmailAddress = "fcontact@contact.com",
		            },

		            #endregion Contact First

		            #region Contact Second
		            new Contact // Second Contact
		            {
		                FirstName = "Second",
		                LastName = "Contact",
		                ContactTypeId = "MON",
		                RelationId = "OWN",
		                AuthId = "FULL",
		                Phone1 = "9722437443",
		                PhoneTypeId1 = "CL",

		            },

		            #endregion Contact Second

		            #region Contact Third
		            new Contact // Third Contact
		            {
		                FirstName = "third",
		                LastName = "Contact",
		                ContactTypeId = "MON",
		                RelationId = "NGH",
		                AuthId = "FULL",
		                Phone1 = "9722437444",
		                PhoneTypeId1 = "HM"
		            },

		            #endregion Contact Third

		            #region Contact Fourth
		            new Contact // Fourth Contact

		            {
		                FirstName = "Fourth",
		                LastName = "Contact",
		                ContactTypeId = "MON",
		                RelationId = "REL",
		                AuthId = "Full",
		                Phone1 = "9722437445",
		                PhoneTypeId1 = "WK",
		                Phone2 = "8176542444",
		                PhoneTypeId2 = "WK",
		                Phone3 = "6434322333",
		                PhoneTypeId3 = "CL"
		            }
		            #endregion Contact Fourth



		        },

		        #endregion Contacts

		        #region SiteAgencyPermits
		        SiteAgencyPermits = new List<SiteAgencyPermit>
		        {
		            #region PD
		            new SiteAgencyPermit
		            {
		                AgencyTypeId = "PD",
		                Phone1 = "8016549877",
		                PermTypeId = "F",
		                PermitNo = rand1.Next(100000, 999999).ToString(CultureInfo.InvariantCulture),
		                EffectiveDate = "2/15/2013",
		                ExpireDate = "2/14/2015",
		                AgencyNo = "200013216"

		            },

		            #endregion PD

		            #region FD
		            new SiteAgencyPermit
		            {
		                AgencyTypeId = "FD",
		                Phone1 = "8012297070",
		                PermitNo = rand2.Next(100000, 999999).ToString(CultureInfo.InvariantCulture)

		            },

		            #endregion FD

		            #region MD
		            new SiteAgencyPermit
		            {
		                AgencyTypeId = "MD",
		                Phone1 = "8012297070",

		            }
		            #endregion MD

		        },

		        #endregion SiteAgencyPermits

		        #region Zones
		        Zones = new List<Zone>
		        {

					#region 1
					new Zone
					{
						ZoneId = "1",
						ZoneStateId = "A",
						EventId = "1400",
						EquipmentLocationId = "Bed",
						EquipmentTypeId = "WSR"

					},

					#endregion 1
                    
		            #region 2
		            new Zone
		            {
		                //EquipmentLocationId = "FrontDoor", //-- This generates an error
		                EquipmentLocationId = "FRNT",
		                // EquipmentTypeId = "Motion", //-- This also generates an error
		                EquipmentTypeId = "PIR",
		                // EventId = "343", //-- This also generates an error
		                EventId = "1400",
		                ZoneId = "2",
		                ZoneStateId = "A",
		                ZoneComment = "Living Room"

		            },

		            #endregion 2

		            #region 3
		            new Zone
		            {
		                ZoneId = "3",
		                EventId = "1400",
		                EquipmentLocationId = "KIT",
		                EquipmentTypeId = "WSR",
		                ZoneComment = "Kitchen/Dining"
		            },

		            #endregion 3

					//#region 4
					//new Zone
					//{
					//	ZoneId = "4",
					//	ZoneStateId = "A",
					//	EventId = "1400",
					//	EquipmentLocationId = "Bed",
					//	EquipmentTypeId = "WSR"

					//},

					//#endregion 4
                    
					//#region 95
					//new Zone
					//{
					//	ZoneId = "95",
					//	EquipmentLocationId = "KYPD",
					//	EquipmentTypeId = "KEYPAD",

					//},

					//#endregion 95

					//#region 96
					//new Zone
					//{
					//	ZoneId = "96",
					//	EquipmentLocationId = "KYPD",
					//	EquipmentTypeId = "KEYPAD"
					//},

					//#endregion 96
                    
					//#region 99
					//new Zone
					//{
					//	ZoneId = "99",
					//	EquipmentLocationId = "KYPD",
					//	EquipmentTypeId = "KEYPAD"
					//}

					//#endregion 99


		        },

		        #endregion Zones

		        #region SiteGeneralDispatches
		        SiteGeneralDispatches = new List<SiteGeneralDispatches>
		        {
		            new SiteGeneralDispatches
		            {
		                Instructions = "This is one instruction"
		            },
		            new SiteGeneralDispatches
		            {
		                Instructions = "This is another instruction"
		            }
		        },

		        #endregion SiteGeneralDispatches

		        #region SiteOptions
		        SiteOptions = new List<SiteOption>
		        {
		            new SiteOption
		            {
		                OptionId = "CMPUR",
		                OptionValue = "PUR"
		            },
		            new SiteOption
		            {
		                OptionId = "CONTRLEN",
		                OptionValue = "60"
		            },
                    new SiteOption
                    {
                        OptionId = "BARRIO",
                        OptionValue = "",
                    }
		        },

		        #endregion SiteOptions

		        #region SiteSytemOptions
		        SiteSystemOptions = new List<SiteSystemOption>
		        {
		            new SiteSystemOption
		            {
		                OptionId = "DSL-VOIP",
		                OptionValue = "VOIP"
		            },
		           
                    new SiteSystemOption
		            {
		                OptionId = "INST CODE",
		                OptionValue = "4444"
		            },
                   
                    new SiteSystemOption
                    {
                        OptionId = "PRIVACY",
                        OptionValue = "N"
                    },
                   
                    new SiteSystemOption
                    {
                        OptionId = "WIRELESS",
                        OptionValue = "W"   
                    },
                    
                    new SiteSystemOption
                    {
                        OptionId = "SIGFMT",
                        OptionValue = "CID" 
                    },
                    
                    new SiteSystemOption
                    {
                        OptionId = "TRANSFORMER",
                        OptionValue = "master closet"
                    }
		        }
		    };
            #endregion SiteSytemOptions


            #endregion Setup Account

            #region Setup CreditRequestXml

			var credReq = new CreditRequest
			{
				CS = _CS_NO_DIGITAL_PRIMARY,
				SSN = "234567890",
				FirstName = "Sammy",
				LastName = "SOSA",
				StreetNumber = "111",
				City = "City",
				State = "TX",
				Zip = "22222",
				FICO = "777",
				TransactionID = "123445",
				Token = "abc123def456",
				DealerId = "423432",
				UserId = _USERID,
				RequestDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)
			};

            #endregion Setup CreditRequestXml

            // Convert to XML.
			var xmlizedString = acct.Serialize();
			var moniService = new Monitronics(_USERID, _PASWRD);
			var creditRequestXml = credReq.Serialize();
			var purchaseInfoXml = string.Empty;
			DataSet dsRaw;
			string confirmationNumber;
			string firstErrorMsg;
			Assert.IsTrue(moniService.AccountOnlineTry(_CS_NO_DIGITAL_PRIMARY, xmlizedString, out dsRaw, out confirmationNumber, out firstErrorMsg, creditRequestXml, purchaseInfoXml), string.Format("The following error was generated"));

			Assert.IsTrue(!string.IsNullOrEmpty(confirmationNumber), "Confirmation # should not be empty.");
		}

		[TestMethod]
		public void TestConfirmationExtration()
		{
			// ** Initialize. 
			const string CONFIRMATION_MESSAGE = "Online confirmation #, 08528269; Digital";

			var confirmationNumber = Utils.GetOnlineConfirmationNumber(CONFIRMATION_MESSAGE);

			Assert.IsTrue(confirmationNumber.Equals("08528269"), "Confirmation Number did not pull correctly.");
		}

		#endregion Test Methods
	}
}