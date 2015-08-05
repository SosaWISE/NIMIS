


using System;
using SubSonic;
using SOS.Data;

namespace SOS.Data.HumanResource
{
	public partial class HumanResourceDataContext
	{
		#region Internal Instance

		private static HumanResourceDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();

		public static HumanResourceDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new HumanResourceDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}

		#endregion // Internal Instance

		#region Controllers Properties

		APT_AddressController _APT_Addresses;
		public APT_AddressController APT_Addresses
		{
			get
			{
				if (_APT_Addresses == null) _APT_Addresses = new APT_AddressController();
				return _APT_Addresses;
			}
		}

		APT_ApartmentController _APT_Apartments;
		public APT_ApartmentController APT_Apartments
		{
			get
			{
				if (_APT_Apartments == null) _APT_Apartments = new APT_ApartmentController();
				return _APT_Apartments;
			}
		}

		APT_ApartmentBySeasonController _APT_ApartmentBySeasons;
		public APT_ApartmentBySeasonController APT_ApartmentBySeasons
		{
			get
			{
				if (_APT_ApartmentBySeasons == null) _APT_ApartmentBySeasons = new APT_ApartmentBySeasonController();
				return _APT_ApartmentBySeasons;
			}
		}

		APT_ApartmentFurnitureItemController _APT_ApartmentFurnitureItems;
		public APT_ApartmentFurnitureItemController APT_ApartmentFurnitureItems
		{
			get
			{
				if (_APT_ApartmentFurnitureItems == null) _APT_ApartmentFurnitureItems = new APT_ApartmentFurnitureItemController();
				return _APT_ApartmentFurnitureItems;
			}
		}

		APT_ApartmentNoteController _APT_ApartmentNotes;
		public APT_ApartmentNoteController APT_ApartmentNotes
		{
			get
			{
				if (_APT_ApartmentNotes == null) _APT_ApartmentNotes = new APT_ApartmentNoteController();
				return _APT_ApartmentNotes;
			}
		}

		APT_ApartmentTypeController _APT_ApartmentTypes;
		public APT_ApartmentTypeController APT_ApartmentTypes
		{
			get
			{
				if (_APT_ApartmentTypes == null) _APT_ApartmentTypes = new APT_ApartmentTypeController();
				return _APT_ApartmentTypes;
			}
		}

		APT_BedController _APT_Beds;
		public APT_BedController APT_Beds
		{
			get
			{
				if (_APT_Beds == null) _APT_Beds = new APT_BedController();
				return _APT_Beds;
			}
		}

		APT_BedroomController _APT_Bedrooms;
		public APT_BedroomController APT_Bedrooms
		{
			get
			{
				if (_APT_Bedrooms == null) _APT_Bedrooms = new APT_BedroomController();
				return _APT_Bedrooms;
			}
		}

		APT_BedTypeController _APT_BedTypes;
		public APT_BedTypeController APT_BedTypes
		{
			get
			{
				if (_APT_BedTypes == null) _APT_BedTypes = new APT_BedTypeController();
				return _APT_BedTypes;
			}
		}

		APT_ComplaintController _APT_Complaints;
		public APT_ComplaintController APT_Complaints
		{
			get
			{
				if (_APT_Complaints == null) _APT_Complaints = new APT_ComplaintController();
				return _APT_Complaints;
			}
		}

		APT_ComplexController _APT_Complices;
		public APT_ComplexController APT_Complices
		{
			get
			{
				if (_APT_Complices == null) _APT_Complices = new APT_ComplexController();
				return _APT_Complices;
			}
		}

		APT_ContactController _APT_Contacts;
		public APT_ContactController APT_Contacts
		{
			get
			{
				if (_APT_Contacts == null) _APT_Contacts = new APT_ContactController();
				return _APT_Contacts;
			}
		}

		APT_ContactTypeController _APT_ContactTypes;
		public APT_ContactTypeController APT_ContactTypes
		{
			get
			{
				if (_APT_ContactTypes == null) _APT_ContactTypes = new APT_ContactTypeController();
				return _APT_ContactTypes;
			}
		}

		APT_DefaultFurniturePackageController _APT_DefaultFurniturePackages;
		public APT_DefaultFurniturePackageController APT_DefaultFurniturePackages
		{
			get
			{
				if (_APT_DefaultFurniturePackages == null) _APT_DefaultFurniturePackages = new APT_DefaultFurniturePackageController();
				return _APT_DefaultFurniturePackages;
			}
		}

		APT_DefaultFurniturePackageContentController _APT_DefaultFurniturePackageContents;
		public APT_DefaultFurniturePackageContentController APT_DefaultFurniturePackageContents
		{
			get
			{
				if (_APT_DefaultFurniturePackageContents == null) _APT_DefaultFurniturePackageContents = new APT_DefaultFurniturePackageContentController();
				return _APT_DefaultFurniturePackageContents;
			}
		}

		APT_DocumentController _APT_Documents;
		public APT_DocumentController APT_Documents
		{
			get
			{
				if (_APT_Documents == null) _APT_Documents = new APT_DocumentController();
				return _APT_Documents;
			}
		}

		APT_DocumentTypeController _APT_DocumentTypes;
		public APT_DocumentTypeController APT_DocumentTypes
		{
			get
			{
				if (_APT_DocumentTypes == null) _APT_DocumentTypes = new APT_DocumentTypeController();
				return _APT_DocumentTypes;
			}
		}

		APT_ExpenseController _APT_Expenses;
		public APT_ExpenseController APT_Expenses
		{
			get
			{
				if (_APT_Expenses == null) _APT_Expenses = new APT_ExpenseController();
				return _APT_Expenses;
			}
		}

		APT_ExpenseBillController _APT_ExpenseBills;
		public APT_ExpenseBillController APT_ExpenseBills
		{
			get
			{
				if (_APT_ExpenseBills == null) _APT_ExpenseBills = new APT_ExpenseBillController();
				return _APT_ExpenseBills;
			}
		}

		APT_ExpensePaymentController _APT_ExpensePayments;
		public APT_ExpensePaymentController APT_ExpensePayments
		{
			get
			{
				if (_APT_ExpensePayments == null) _APT_ExpensePayments = new APT_ExpensePaymentController();
				return _APT_ExpensePayments;
			}
		}

		APT_ExpenseTypeController _APT_ExpenseTypes;
		public APT_ExpenseTypeController APT_ExpenseTypes
		{
			get
			{
				if (_APT_ExpenseTypes == null) _APT_ExpenseTypes = new APT_ExpenseTypeController();
				return _APT_ExpenseTypes;
			}
		}

		APT_FurniturePackageController _APT_FurniturePackages;
		public APT_FurniturePackageController APT_FurniturePackages
		{
			get
			{
				if (_APT_FurniturePackages == null) _APT_FurniturePackages = new APT_FurniturePackageController();
				return _APT_FurniturePackages;
			}
		}

		APT_FurniturePieceController _APT_FurniturePieces;
		public APT_FurniturePieceController APT_FurniturePieces
		{
			get
			{
				if (_APT_FurniturePieces == null) _APT_FurniturePieces = new APT_FurniturePieceController();
				return _APT_FurniturePieces;
			}
		}

		APT_ManagementCompanyController _APT_ManagementCompanies;
		public APT_ManagementCompanyController APT_ManagementCompanies
		{
			get
			{
				if (_APT_ManagementCompanies == null) _APT_ManagementCompanies = new APT_ManagementCompanyController();
				return _APT_ManagementCompanies;
			}
		}

		APT_OccupantController _APT_Occupants;
		public APT_OccupantController APT_Occupants
		{
			get
			{
				if (_APT_Occupants == null) _APT_Occupants = new APT_OccupantController();
				return _APT_Occupants;
			}
		}

		APT_RecruitChargeController _APT_RecruitCharges;
		public APT_RecruitChargeController APT_RecruitCharges
		{
			get
			{
				if (_APT_RecruitCharges == null) _APT_RecruitCharges = new APT_RecruitChargeController();
				return _APT_RecruitCharges;
			}
		}

		dtpropertyController _dtproperties;
		public dtpropertyController dtproperties
		{
			get
			{
				if (_dtproperties == null) _dtproperties = new dtpropertyController();
				return _dtproperties;
			}
		}

		ES_MessageAttachmentController _ES_MessageAttachments;
		public ES_MessageAttachmentController ES_MessageAttachments
		{
			get
			{
				if (_ES_MessageAttachments == null) _ES_MessageAttachments = new ES_MessageAttachmentController();
				return _ES_MessageAttachments;
			}
		}

		ES_MessageRecipientController _ES_MessageRecipients;
		public ES_MessageRecipientController ES_MessageRecipients
		{
			get
			{
				if (_ES_MessageRecipients == null) _ES_MessageRecipients = new ES_MessageRecipientController();
				return _ES_MessageRecipients;
			}
		}

		ES_MessageController _ES_Messages;
		public ES_MessageController ES_Messages
		{
			get
			{
				if (_ES_Messages == null) _ES_Messages = new ES_MessageController();
				return _ES_Messages;
			}
		}

		GP_CustAcctSummaryController _GP_CustAcctSummaries;
		public GP_CustAcctSummaryController GP_CustAcctSummaries
		{
			get
			{
				if (_GP_CustAcctSummaries == null) _GP_CustAcctSummaries = new GP_CustAcctSummaryController();
				return _GP_CustAcctSummaries;
			}
		}

		HS_ComplexController _HS_Complices;
		public HS_ComplexController HS_Complices
		{
			get
			{
				if (_HS_Complices == null) _HS_Complices = new HS_ComplexController();
				return _HS_Complices;
			}
		}

		HS_ComplexCheckTypeController _HS_ComplexCheckTypes;
		public HS_ComplexCheckTypeController HS_ComplexCheckTypes
		{
			get
			{
				if (_HS_ComplexCheckTypes == null) _HS_ComplexCheckTypes = new HS_ComplexCheckTypeController();
				return _HS_ComplexCheckTypes;
			}
		}

		HS_ComplexNoteController _HS_ComplexNotes;
		public HS_ComplexNoteController HS_ComplexNotes
		{
			get
			{
				if (_HS_ComplexNotes == null) _HS_ComplexNotes = new HS_ComplexNoteController();
				return _HS_ComplexNotes;
			}
		}

		HS_ContactInformationController _HS_ContactInformations;
		public HS_ContactInformationController HS_ContactInformations
		{
			get
			{
				if (_HS_ContactInformations == null) _HS_ContactInformations = new HS_ContactInformationController();
				return _HS_ContactInformations;
			}
		}

		HS_ContactInformationAddressController _HS_ContactInformationAddresses;
		public HS_ContactInformationAddressController HS_ContactInformationAddresses
		{
			get
			{
				if (_HS_ContactInformationAddresses == null) _HS_ContactInformationAddresses = new HS_ContactInformationAddressController();
				return _HS_ContactInformationAddresses;
			}
		}

		HS_ContactInformationAddressTypeController _HS_ContactInformationAddressTypes;
		public HS_ContactInformationAddressTypeController HS_ContactInformationAddressTypes
		{
			get
			{
				if (_HS_ContactInformationAddressTypes == null) _HS_ContactInformationAddressTypes = new HS_ContactInformationAddressTypeController();
				return _HS_ContactInformationAddressTypes;
			}
		}

		HS_FurnitureCompanyController _HS_FurnitureCompanies;
		public HS_FurnitureCompanyController HS_FurnitureCompanies
		{
			get
			{
				if (_HS_FurnitureCompanies == null) _HS_FurnitureCompanies = new HS_FurnitureCompanyController();
				return _HS_FurnitureCompanies;
			}
		}

		HS_FurnitureItemController _HS_FurnitureItems;
		public HS_FurnitureItemController HS_FurnitureItems
		{
			get
			{
				if (_HS_FurnitureItems == null) _HS_FurnitureItems = new HS_FurnitureItemController();
				return _HS_FurnitureItems;
			}
		}

		HS_FurniturePackageController _HS_FurniturePackages;
		public HS_FurniturePackageController HS_FurniturePackages
		{
			get
			{
				if (_HS_FurniturePackages == null) _HS_FurniturePackages = new HS_FurniturePackageController();
				return _HS_FurniturePackages;
			}
		}

		HS_FurniturePackageContentController _HS_FurniturePackageContents;
		public HS_FurniturePackageContentController HS_FurniturePackageContents
		{
			get
			{
				if (_HS_FurniturePackageContents == null) _HS_FurniturePackageContents = new HS_FurniturePackageContentController();
				return _HS_FurniturePackageContents;
			}
		}

		HS_HousingUserController _HS_HousingUsers;
		public HS_HousingUserController HS_HousingUsers
		{
			get
			{
				if (_HS_HousingUsers == null) _HS_HousingUsers = new HS_HousingUserController();
				return _HS_HousingUsers;
			}
		}

		HS_OfficeComplexController _HS_OfficeComplices;
		public HS_OfficeComplexController HS_OfficeComplices
		{
			get
			{
				if (_HS_OfficeComplices == null) _HS_OfficeComplices = new HS_OfficeComplexController();
				return _HS_OfficeComplices;
			}
		}

		HS_OfficeComplexUnitController _HS_OfficeComplexUnits;
		public HS_OfficeComplexUnitController HS_OfficeComplexUnits
		{
			get
			{
				if (_HS_OfficeComplexUnits == null) _HS_OfficeComplexUnits = new HS_OfficeComplexUnitController();
				return _HS_OfficeComplexUnits;
			}
		}

		HS_UnitController _HS_Units;
		public HS_UnitController HS_Units
		{
			get
			{
				if (_HS_Units == null) _HS_Units = new HS_UnitController();
				return _HS_Units;
			}
		}

		HS_UnitAddressController _HS_UnitAddresses;
		public HS_UnitAddressController HS_UnitAddresses
		{
			get
			{
				if (_HS_UnitAddresses == null) _HS_UnitAddresses = new HS_UnitAddressController();
				return _HS_UnitAddresses;
			}
		}

		HS_UnitDamageController _HS_UnitDamages;
		public HS_UnitDamageController HS_UnitDamages
		{
			get
			{
				if (_HS_UnitDamages == null) _HS_UnitDamages = new HS_UnitDamageController();
				return _HS_UnitDamages;
			}
		}

		HS_UnitDamageUserController _HS_UnitDamageUsers;
		public HS_UnitDamageUserController HS_UnitDamageUsers
		{
			get
			{
				if (_HS_UnitDamageUsers == null) _HS_UnitDamageUsers = new HS_UnitDamageUserController();
				return _HS_UnitDamageUsers;
			}
		}

		HS_UnitFurniturePackageController _HS_UnitFurniturePackages;
		public HS_UnitFurniturePackageController HS_UnitFurniturePackages
		{
			get
			{
				if (_HS_UnitFurniturePackages == null) _HS_UnitFurniturePackages = new HS_UnitFurniturePackageController();
				return _HS_UnitFurniturePackages;
			}
		}

		HS_UnitNoteController _HS_UnitNotes;
		public HS_UnitNoteController HS_UnitNotes
		{
			get
			{
				if (_HS_UnitNotes == null) _HS_UnitNotes = new HS_UnitNoteController();
				return _HS_UnitNotes;
			}
		}

		HS_UnitOccupantController _HS_UnitOccupants;
		public HS_UnitOccupantController HS_UnitOccupants
		{
			get
			{
				if (_HS_UnitOccupants == null) _HS_UnitOccupants = new HS_UnitOccupantController();
				return _HS_UnitOccupants;
			}
		}

		HS_UnitTypeController _HS_UnitTypes;
		public HS_UnitTypeController HS_UnitTypes
		{
			get
			{
				if (_HS_UnitTypes == null) _HS_UnitTypes = new HS_UnitTypeController();
				return _HS_UnitTypes;
			}
		}

		HS_UnitVacateNoticeSentByTypeController _HS_UnitVacateNoticeSentByTypes;
		public HS_UnitVacateNoticeSentByTypeController HS_UnitVacateNoticeSentByTypes
		{
			get
			{
				if (_HS_UnitVacateNoticeSentByTypes == null) _HS_UnitVacateNoticeSentByTypes = new HS_UnitVacateNoticeSentByTypeController();
				return _HS_UnitVacateNoticeSentByTypes;
			}
		}

		MC_PoliticalCountryController _MC_PoliticalCountries;
		public MC_PoliticalCountryController MC_PoliticalCountries
		{
			get
			{
				if (_MC_PoliticalCountries == null) _MC_PoliticalCountries = new MC_PoliticalCountryController();
				return _MC_PoliticalCountries;
			}
		}

		MC_PoliticalStateController _MC_PoliticalStates;
		public MC_PoliticalStateController MC_PoliticalStates
		{
			get
			{
				if (_MC_PoliticalStates == null) _MC_PoliticalStates = new MC_PoliticalStateController();
				return _MC_PoliticalStates;
			}
		}

		MC_PoliticalTimeZoneController _MC_PoliticalTimeZones;
		public MC_PoliticalTimeZoneController MC_PoliticalTimeZones
		{
			get
			{
				if (_MC_PoliticalTimeZones == null) _MC_PoliticalTimeZones = new MC_PoliticalTimeZoneController();
				return _MC_PoliticalTimeZones;
			}
		}

		PR_AccountCalculationController _PR_AccountCalculations;
		public PR_AccountCalculationController PR_AccountCalculations
		{
			get
			{
				if (_PR_AccountCalculations == null) _PR_AccountCalculations = new PR_AccountCalculationController();
				return _PR_AccountCalculations;
			}
		}

		PR_AccountStateExceptionController _PR_AccountStateExceptions;
		public PR_AccountStateExceptionController PR_AccountStateExceptions
		{
			get
			{
				if (_PR_AccountStateExceptions == null) _PR_AccountStateExceptions = new PR_AccountStateExceptionController();
				return _PR_AccountStateExceptions;
			}
		}

		PR_AccountStateController _PR_AccountStates;
		public PR_AccountStateController PR_AccountStates
		{
			get
			{
				if (_PR_AccountStates == null) _PR_AccountStates = new PR_AccountStateController();
				return _PR_AccountStates;
			}
		}

		PR_AccountsToIgnoreController _PR_AccountsToIgnores;
		public PR_AccountsToIgnoreController PR_AccountsToIgnores
		{
			get
			{
				if (_PR_AccountsToIgnores == null) _PR_AccountsToIgnores = new PR_AccountsToIgnoreController();
				return _PR_AccountsToIgnores;
			}
		}

		PR_AutomaticTransactionController _PR_AutomaticTransactions;
		public PR_AutomaticTransactionController PR_AutomaticTransactions
		{
			get
			{
				if (_PR_AutomaticTransactions == null) _PR_AutomaticTransactions = new PR_AutomaticTransactionController();
				return _PR_AutomaticTransactions;
			}
		}

		PR_BackendAccountCalculationController _PR_BackendAccountCalculations;
		public PR_BackendAccountCalculationController PR_BackendAccountCalculations
		{
			get
			{
				if (_PR_BackendAccountCalculations == null) _PR_BackendAccountCalculations = new PR_BackendAccountCalculationController();
				return _PR_BackendAccountCalculations;
			}
		}

		PR_BackendAccountExceptionController _PR_BackendAccountExceptions;
		public PR_BackendAccountExceptionController PR_BackendAccountExceptions
		{
			get
			{
				if (_PR_BackendAccountExceptions == null) _PR_BackendAccountExceptions = new PR_BackendAccountExceptionController();
				return _PR_BackendAccountExceptions;
			}
		}

		PR_BackendAccountMappingController _PR_BackendAccountMappings;
		public PR_BackendAccountMappingController PR_BackendAccountMappings
		{
			get
			{
				if (_PR_BackendAccountMappings == null) _PR_BackendAccountMappings = new PR_BackendAccountMappingController();
				return _PR_BackendAccountMappings;
			}
		}

		PR_BackendAccountMappingTypeController _PR_BackendAccountMappingTypes;
		public PR_BackendAccountMappingTypeController PR_BackendAccountMappingTypes
		{
			get
			{
				if (_PR_BackendAccountMappingTypes == null) _PR_BackendAccountMappingTypes = new PR_BackendAccountMappingTypeController();
				return _PR_BackendAccountMappingTypes;
			}
		}

		PR_BackendAccountNumbersExceptionController _PR_BackendAccountNumbersExceptions;
		public PR_BackendAccountNumbersExceptionController PR_BackendAccountNumbersExceptions
		{
			get
			{
				if (_PR_BackendAccountNumbersExceptions == null) _PR_BackendAccountNumbersExceptions = new PR_BackendAccountNumbersExceptionController();
				return _PR_BackendAccountNumbersExceptions;
			}
		}

		PR_BackendAdjustmentController _PR_BackendAdjustments;
		public PR_BackendAdjustmentController PR_BackendAdjustments
		{
			get
			{
				if (_PR_BackendAdjustments == null) _PR_BackendAdjustments = new PR_BackendAdjustmentController();
				return _PR_BackendAdjustments;
			}
		}

		PR_BackendAggregateTransactionController _PR_BackendAggregateTransactions;
		public PR_BackendAggregateTransactionController PR_BackendAggregateTransactions
		{
			get
			{
				if (_PR_BackendAggregateTransactions == null) _PR_BackendAggregateTransactions = new PR_BackendAggregateTransactionController();
				return _PR_BackendAggregateTransactions;
			}
		}

		PR_BackendAggregateTransactionTypeController _PR_BackendAggregateTransactionTypes;
		public PR_BackendAggregateTransactionTypeController PR_BackendAggregateTransactionTypes
		{
			get
			{
				if (_PR_BackendAggregateTransactionTypes == null) _PR_BackendAggregateTransactionTypes = new PR_BackendAggregateTransactionTypeController();
				return _PR_BackendAggregateTransactionTypes;
			}
		}

		PR_BackendExceptionController _PR_BackendExceptions;
		public PR_BackendExceptionController PR_BackendExceptions
		{
			get
			{
				if (_PR_BackendExceptions == null) _PR_BackendExceptions = new PR_BackendExceptionController();
				return _PR_BackendExceptions;
			}
		}

		PR_BackendLegionController _PR_BackendLegions;
		public PR_BackendLegionController PR_BackendLegions
		{
			get
			{
				if (_PR_BackendLegions == null) _PR_BackendLegions = new PR_BackendLegionController();
				return _PR_BackendLegions;
			}
		}

		PR_BackendManagerPayscaleController _PR_BackendManagerPayscales;
		public PR_BackendManagerPayscaleController PR_BackendManagerPayscales
		{
			get
			{
				if (_PR_BackendManagerPayscales == null) _PR_BackendManagerPayscales = new PR_BackendManagerPayscaleController();
				return _PR_BackendManagerPayscales;
			}
		}

		PR_BackendManagerPayScheduleController _PR_BackendManagerPaySchedules;
		public PR_BackendManagerPayScheduleController PR_BackendManagerPaySchedules
		{
			get
			{
				if (_PR_BackendManagerPaySchedules == null) _PR_BackendManagerPaySchedules = new PR_BackendManagerPayScheduleController();
				return _PR_BackendManagerPaySchedules;
			}
		}

		PR_BackendPayscaleController _PR_BackendPayscales;
		public PR_BackendPayscaleController PR_BackendPayscales
		{
			get
			{
				if (_PR_BackendPayscales == null) _PR_BackendPayscales = new PR_BackendPayscaleController();
				return _PR_BackendPayscales;
			}
		}

		PR_BackendPayScheduleController _PR_BackendPaySchedules;
		public PR_BackendPayScheduleController PR_BackendPaySchedules
		{
			get
			{
				if (_PR_BackendPaySchedules == null) _PR_BackendPaySchedules = new PR_BackendPayScheduleController();
				return _PR_BackendPaySchedules;
			}
		}

		PR_BackendPeriodController _PR_BackendPeriods;
		public PR_BackendPeriodController PR_BackendPeriods
		{
			get
			{
				if (_PR_BackendPeriods == null) _PR_BackendPeriods = new PR_BackendPeriodController();
				return _PR_BackendPeriods;
			}
		}

		PR_BackendPeriodSeasonMappingController _PR_BackendPeriodSeasonMappings;
		public PR_BackendPeriodSeasonMappingController PR_BackendPeriodSeasonMappings
		{
			get
			{
				if (_PR_BackendPeriodSeasonMappings == null) _PR_BackendPeriodSeasonMappings = new PR_BackendPeriodSeasonMappingController();
				return _PR_BackendPeriodSeasonMappings;
			}
		}

		PR_BackendPeriodUserTypeMappingController _PR_BackendPeriodUserTypeMappings;
		public PR_BackendPeriodUserTypeMappingController PR_BackendPeriodUserTypeMappings
		{
			get
			{
				if (_PR_BackendPeriodUserTypeMappings == null) _PR_BackendPeriodUserTypeMappings = new PR_BackendPeriodUserTypeMappingController();
				return _PR_BackendPeriodUserTypeMappings;
			}
		}

		PR_BackendQualificationExceptionController _PR_BackendQualificationExceptions;
		public PR_BackendQualificationExceptionController PR_BackendQualificationExceptions
		{
			get
			{
				if (_PR_BackendQualificationExceptions == null) _PR_BackendQualificationExceptions = new PR_BackendQualificationExceptionController();
				return _PR_BackendQualificationExceptions;
			}
		}

		PR_BackendRentExceptionController _PR_BackendRentExceptions;
		public PR_BackendRentExceptionController PR_BackendRentExceptions
		{
			get
			{
				if (_PR_BackendRentExceptions == null) _PR_BackendRentExceptions = new PR_BackendRentExceptionController();
				return _PR_BackendRentExceptions;
			}
		}

		PR_BackendController _PR_Backends;
		public PR_BackendController PR_Backends
		{
			get
			{
				if (_PR_Backends == null) _PR_Backends = new PR_BackendController();
				return _PR_Backends;
			}
		}

		PR_BackendTransactionCategoryController _PR_BackendTransactionCategories;
		public PR_BackendTransactionCategoryController PR_BackendTransactionCategories
		{
			get
			{
				if (_PR_BackendTransactionCategories == null) _PR_BackendTransactionCategories = new PR_BackendTransactionCategoryController();
				return _PR_BackendTransactionCategories;
			}
		}

		PR_BackendTransactionController _PR_BackendTransactions;
		public PR_BackendTransactionController PR_BackendTransactions
		{
			get
			{
				if (_PR_BackendTransactions == null) _PR_BackendTransactions = new PR_BackendTransactionController();
				return _PR_BackendTransactions;
			}
		}

		PR_BackendTransactionTypeController _PR_BackendTransactionTypes;
		public PR_BackendTransactionTypeController PR_BackendTransactionTypes
		{
			get
			{
				if (_PR_BackendTransactionTypes == null) _PR_BackendTransactionTypes = new PR_BackendTransactionTypeController();
				return _PR_BackendTransactionTypes;
			}
		}

		PR_BarcodesToIgnoreController _PR_BarcodesToIgnores;
		public PR_BarcodesToIgnoreController PR_BarcodesToIgnores
		{
			get
			{
				if (_PR_BarcodesToIgnores == null) _PR_BarcodesToIgnores = new PR_BarcodesToIgnoreController();
				return _PR_BarcodesToIgnores;
			}
		}

		PR_BatchController _PR_Batches;
		public PR_BatchController PR_Batches
		{
			get
			{
				if (_PR_Batches == null) _PR_Batches = new PR_BatchController();
				return _PR_Batches;
			}
		}

		PR_HistoricalPaycheckController _PR_HistoricalPaychecks;
		public PR_HistoricalPaycheckController PR_HistoricalPaychecks
		{
			get
			{
				if (_PR_HistoricalPaychecks == null) _PR_HistoricalPaychecks = new PR_HistoricalPaycheckController();
				return _PR_HistoricalPaychecks;
			}
		}

		PR_HistoricalTransactionController _PR_HistoricalTransactions;
		public PR_HistoricalTransactionController PR_HistoricalTransactions
		{
			get
			{
				if (_PR_HistoricalTransactions == null) _PR_HistoricalTransactions = new PR_HistoricalTransactionController();
				return _PR_HistoricalTransactions;
			}
		}

		PR_LeadTechBackendController _PR_LeadTechBackends;
		public PR_LeadTechBackendController PR_LeadTechBackends
		{
			get
			{
				if (_PR_LeadTechBackends == null) _PR_LeadTechBackends = new PR_LeadTechBackendController();
				return _PR_LeadTechBackends;
			}
		}

		PR_MiscBackendAmountController _PR_MiscBackendAmounts;
		public PR_MiscBackendAmountController PR_MiscBackendAmounts
		{
			get
			{
				if (_PR_MiscBackendAmounts == null) _PR_MiscBackendAmounts = new PR_MiscBackendAmountController();
				return _PR_MiscBackendAmounts;
			}
		}

		PR_MiscBackendTransactionController _PR_MiscBackendTransactions;
		public PR_MiscBackendTransactionController PR_MiscBackendTransactions
		{
			get
			{
				if (_PR_MiscBackendTransactions == null) _PR_MiscBackendTransactions = new PR_MiscBackendTransactionController();
				return _PR_MiscBackendTransactions;
			}
		}

		PR_OfficeCheckRecipientController _PR_OfficeCheckRecipients;
		public PR_OfficeCheckRecipientController PR_OfficeCheckRecipients
		{
			get
			{
				if (_PR_OfficeCheckRecipients == null) _PR_OfficeCheckRecipients = new PR_OfficeCheckRecipientController();
				return _PR_OfficeCheckRecipients;
			}
		}

		PR_OfficeCheckController _PR_OfficeChecks;
		public PR_OfficeCheckController PR_OfficeChecks
		{
			get
			{
				if (_PR_OfficeChecks == null) _PR_OfficeChecks = new PR_OfficeCheckController();
				return _PR_OfficeChecks;
			}
		}

		PR_OfficeTransactionController _PR_OfficeTransactions;
		public PR_OfficeTransactionController PR_OfficeTransactions
		{
			get
			{
				if (_PR_OfficeTransactions == null) _PR_OfficeTransactions = new PR_OfficeTransactionController();
				return _PR_OfficeTransactions;
			}
		}

		PR_OfficeTransactionTypeController _PR_OfficeTransactionTypes;
		public PR_OfficeTransactionTypeController PR_OfficeTransactionTypes
		{
			get
			{
				if (_PR_OfficeTransactionTypes == null) _PR_OfficeTransactionTypes = new PR_OfficeTransactionTypeController();
				return _PR_OfficeTransactionTypes;
			}
		}

		PR_PaycheckController _PR_Paychecks;
		public PR_PaycheckController PR_Paychecks
		{
			get
			{
				if (_PR_Paychecks == null) _PR_Paychecks = new PR_PaycheckController();
				return _PR_Paychecks;
			}
		}

		PR_PayPeriodController _PR_PayPeriods;
		public PR_PayPeriodController PR_PayPeriods
		{
			get
			{
				if (_PR_PayPeriods == null) _PR_PayPeriods = new PR_PayPeriodController();
				return _PR_PayPeriods;
			}
		}

		PR_PayPeriodSeasonMappingController _PR_PayPeriodSeasonMappings;
		public PR_PayPeriodSeasonMappingController PR_PayPeriodSeasonMappings
		{
			get
			{
				if (_PR_PayPeriodSeasonMappings == null) _PR_PayPeriodSeasonMappings = new PR_PayPeriodSeasonMappingController();
				return _PR_PayPeriodSeasonMappings;
			}
		}

		PR_PayPeriodValidAccountSeasonController _PR_PayPeriodValidAccountSeasons;
		public PR_PayPeriodValidAccountSeasonController PR_PayPeriodValidAccountSeasons
		{
			get
			{
				if (_PR_PayPeriodValidAccountSeasons == null) _PR_PayPeriodValidAccountSeasons = new PR_PayPeriodValidAccountSeasonController();
				return _PR_PayPeriodValidAccountSeasons;
			}
		}

		PR_PayScheduleController _PR_PaySchedules;
		public PR_PayScheduleController PR_PaySchedules
		{
			get
			{
				if (_PR_PaySchedules == null) _PR_PaySchedules = new PR_PayScheduleController();
				return _PR_PaySchedules;
			}
		}

		PR_RegionalResidualAccountMappingController _PR_RegionalResidualAccountMappings;
		public PR_RegionalResidualAccountMappingController PR_RegionalResidualAccountMappings
		{
			get
			{
				if (_PR_RegionalResidualAccountMappings == null) _PR_RegionalResidualAccountMappings = new PR_RegionalResidualAccountMappingController();
				return _PR_RegionalResidualAccountMappings;
			}
		}

		PR_RegionalResidualPayeeController _PR_RegionalResidualPayees;
		public PR_RegionalResidualPayeeController PR_RegionalResidualPayees
		{
			get
			{
				if (_PR_RegionalResidualPayees == null) _PR_RegionalResidualPayees = new PR_RegionalResidualPayeeController();
				return _PR_RegionalResidualPayees;
			}
		}

		PR_RegionalResidualPeriodController _PR_RegionalResidualPeriods;
		public PR_RegionalResidualPeriodController PR_RegionalResidualPeriods
		{
			get
			{
				if (_PR_RegionalResidualPeriods == null) _PR_RegionalResidualPeriods = new PR_RegionalResidualPeriodController();
				return _PR_RegionalResidualPeriods;
			}
		}

		PR_RegionalResidualController _PR_RegionalResiduals;
		public PR_RegionalResidualController PR_RegionalResiduals
		{
			get
			{
				if (_PR_RegionalResiduals == null) _PR_RegionalResiduals = new PR_RegionalResidualController();
				return _PR_RegionalResiduals;
			}
		}

		PR_RollingTransactionController _PR_RollingTransactions;
		public PR_RollingTransactionController PR_RollingTransactions
		{
			get
			{
				if (_PR_RollingTransactions == null) _PR_RollingTransactions = new PR_RollingTransactionController();
				return _PR_RollingTransactions;
			}
		}

		PR_RollingTransactionTypeController _PR_RollingTransactionTypes;
		public PR_RollingTransactionTypeController PR_RollingTransactionTypes
		{
			get
			{
				if (_PR_RollingTransactionTypes == null) _PR_RollingTransactionTypes = new PR_RollingTransactionTypeController();
				return _PR_RollingTransactionTypes;
			}
		}

		PR_SalesManagerBackendController _PR_SalesManagerBackends;
		public PR_SalesManagerBackendController PR_SalesManagerBackends
		{
			get
			{
				if (_PR_SalesManagerBackends == null) _PR_SalesManagerBackends = new PR_SalesManagerBackendController();
				return _PR_SalesManagerBackends;
			}
		}

		PR_SalesRegionalBackendController _PR_SalesRegionalBackends;
		public PR_SalesRegionalBackendController PR_SalesRegionalBackends
		{
			get
			{
				if (_PR_SalesRegionalBackends == null) _PR_SalesRegionalBackends = new PR_SalesRegionalBackendController();
				return _PR_SalesRegionalBackends;
			}
		}

		PR_SalesRegionalBackendTotalController _PR_SalesRegionalBackendTotals;
		public PR_SalesRegionalBackendTotalController PR_SalesRegionalBackendTotals
		{
			get
			{
				if (_PR_SalesRegionalBackendTotals == null) _PR_SalesRegionalBackendTotals = new PR_SalesRegionalBackendTotalController();
				return _PR_SalesRegionalBackendTotals;
			}
		}

		PR_SalesRepBackendController _PR_SalesRepBackends;
		public PR_SalesRepBackendController PR_SalesRepBackends
		{
			get
			{
				if (_PR_SalesRepBackends == null) _PR_SalesRepBackends = new PR_SalesRepBackendController();
				return _PR_SalesRepBackends;
			}
		}

		PR_SigningBonusAmountController _PR_SigningBonusAmounts;
		public PR_SigningBonusAmountController PR_SigningBonusAmounts
		{
			get
			{
				if (_PR_SigningBonusAmounts == null) _PR_SigningBonusAmounts = new PR_SigningBonusAmountController();
				return _PR_SigningBonusAmounts;
			}
		}

		PR_TechBackendController _PR_TechBackends;
		public PR_TechBackendController PR_TechBackends
		{
			get
			{
				if (_PR_TechBackends == null) _PR_TechBackends = new PR_TechBackendController();
				return _PR_TechBackends;
			}
		}

		PR_TechRecruitingBonuseController _PR_TechRecruitingBonuses;
		public PR_TechRecruitingBonuseController PR_TechRecruitingBonuses
		{
			get
			{
				if (_PR_TechRecruitingBonuses == null) _PR_TechRecruitingBonuses = new PR_TechRecruitingBonuseController();
				return _PR_TechRecruitingBonuses;
			}
		}

		PR_TechRecruitingBonusTreeController _PR_TechRecruitingBonusTrees;
		public PR_TechRecruitingBonusTreeController PR_TechRecruitingBonusTrees
		{
			get
			{
				if (_PR_TechRecruitingBonusTrees == null) _PR_TechRecruitingBonusTrees = new PR_TechRecruitingBonusTreeController();
				return _PR_TechRecruitingBonusTrees;
			}
		}

		PR_TransactionCategoryController _PR_TransactionCategories;
		public PR_TransactionCategoryController PR_TransactionCategories
		{
			get
			{
				if (_PR_TransactionCategories == null) _PR_TransactionCategories = new PR_TransactionCategoryController();
				return _PR_TransactionCategories;
			}
		}

		PR_TransactionCodeController _PR_TransactionCodes;
		public PR_TransactionCodeController PR_TransactionCodes
		{
			get
			{
				if (_PR_TransactionCodes == null) _PR_TransactionCodes = new PR_TransactionCodeController();
				return _PR_TransactionCodes;
			}
		}

		PR_TransactionController _PR_Transactions;
		public PR_TransactionController PR_Transactions
		{
			get
			{
				if (_PR_Transactions == null) _PR_Transactions = new PR_TransactionController();
				return _PR_Transactions;
			}
		}

		PR_TransactionTypeController _PR_TransactionTypes;
		public PR_TransactionTypeController PR_TransactionTypes
		{
			get
			{
				if (_PR_TransactionTypes == null) _PR_TransactionTypes = new PR_TransactionTypeController();
				return _PR_TransactionTypes;
			}
		}

		PR_WeeklyTransactionController _PR_WeeklyTransactions;
		public PR_WeeklyTransactionController PR_WeeklyTransactions
		{
			get
			{
				if (_PR_WeeklyTransactions == null) _PR_WeeklyTransactions = new PR_WeeklyTransactionController();
				return _PR_WeeklyTransactions;
			}
		}

		PS_ArticleController _PS_Articles;
		public PS_ArticleController PS_Articles
		{
			get
			{
				if (_PS_Articles == null) _PS_Articles = new PS_ArticleController();
				return _PS_Articles;
			}
		}

		PS_ArticleTypeController _PS_ArticleTypes;
		public PS_ArticleTypeController PS_ArticleTypes
		{
			get
			{
				if (_PS_ArticleTypes == null) _PS_ArticleTypes = new PS_ArticleTypeController();
				return _PS_ArticleTypes;
			}
		}

		PS_ContentCollectionEditorController _PS_ContentCollectionEditors;
		public PS_ContentCollectionEditorController PS_ContentCollectionEditors
		{
			get
			{
				if (_PS_ContentCollectionEditors == null) _PS_ContentCollectionEditors = new PS_ContentCollectionEditorController();
				return _PS_ContentCollectionEditors;
			}
		}

		PS_ContentCollectionController _PS_ContentCollections;
		public PS_ContentCollectionController PS_ContentCollections
		{
			get
			{
				if (_PS_ContentCollections == null) _PS_ContentCollections = new PS_ContentCollectionController();
				return _PS_ContentCollections;
			}
		}

		PS_ContentItemController _PS_ContentItems;
		public PS_ContentItemController PS_ContentItems
		{
			get
			{
				if (_PS_ContentItems == null) _PS_ContentItems = new PS_ContentItemController();
				return _PS_ContentItems;
			}
		}

		PS_ContentPermissionController _PS_ContentPermissions;
		public PS_ContentPermissionController PS_ContentPermissions
		{
			get
			{
				if (_PS_ContentPermissions == null) _PS_ContentPermissions = new PS_ContentPermissionController();
				return _PS_ContentPermissions;
			}
		}

		PS_ContentStatusController _PS_ContentStatuses;
		public PS_ContentStatusController PS_ContentStatuses
		{
			get
			{
				if (_PS_ContentStatuses == null) _PS_ContentStatuses = new PS_ContentStatusController();
				return _PS_ContentStatuses;
			}
		}

		PS_ContentTypeController _PS_ContentTypes;
		public PS_ContentTypeController PS_ContentTypes
		{
			get
			{
				if (_PS_ContentTypes == null) _PS_ContentTypes = new PS_ContentTypeController();
				return _PS_ContentTypes;
			}
		}

		PS_CoolStuffController _PS_CoolStuffs;
		public PS_CoolStuffController PS_CoolStuffs
		{
			get
			{
				if (_PS_CoolStuffs == null) _PS_CoolStuffs = new PS_CoolStuffController();
				return _PS_CoolStuffs;
			}
		}

		PS_EventRegistrationController _PS_EventRegistrations;
		public PS_EventRegistrationController PS_EventRegistrations
		{
			get
			{
				if (_PS_EventRegistrations == null) _PS_EventRegistrations = new PS_EventRegistrationController();
				return _PS_EventRegistrations;
			}
		}

		PS_PublicationController _PS_Publications;
		public PS_PublicationController PS_Publications
		{
			get
			{
				if (_PS_Publications == null) _PS_Publications = new PS_PublicationController();
				return _PS_Publications;
			}
		}

		PS_PublishLocationController _PS_PublishLocations;
		public PS_PublishLocationController PS_PublishLocations
		{
			get
			{
				if (_PS_PublishLocations == null) _PS_PublishLocations = new PS_PublishLocationController();
				return _PS_PublishLocations;
			}
		}

		PS_PublishLocationTypeController _PS_PublishLocationTypes;
		public PS_PublishLocationTypeController PS_PublishLocationTypes
		{
			get
			{
				if (_PS_PublishLocationTypes == null) _PS_PublishLocationTypes = new PS_PublishLocationTypeController();
				return _PS_PublishLocationTypes;
			}
		}

		PS_QuestionOptionController _PS_QuestionOptions;
		public PS_QuestionOptionController PS_QuestionOptions
		{
			get
			{
				if (_PS_QuestionOptions == null) _PS_QuestionOptions = new PS_QuestionOptionController();
				return _PS_QuestionOptions;
			}
		}

		PS_QuestionResponseController _PS_QuestionResponses;
		public PS_QuestionResponseController PS_QuestionResponses
		{
			get
			{
				if (_PS_QuestionResponses == null) _PS_QuestionResponses = new PS_QuestionResponseController();
				return _PS_QuestionResponses;
			}
		}

		PS_QuestionController _PS_Questions;
		public PS_QuestionController PS_Questions
		{
			get
			{
				if (_PS_Questions == null) _PS_Questions = new PS_QuestionController();
				return _PS_Questions;
			}
		}

		PS_QuestionTypeController _PS_QuestionTypes;
		public PS_QuestionTypeController PS_QuestionTypes
		{
			get
			{
				if (_PS_QuestionTypes == null) _PS_QuestionTypes = new PS_QuestionTypeController();
				return _PS_QuestionTypes;
			}
		}

		PS_UploadedContentController _PS_UploadedContents;
		public PS_UploadedContentController PS_UploadedContents
		{
			get
			{
				if (_PS_UploadedContents == null) _PS_UploadedContents = new PS_UploadedContentController();
				return _PS_UploadedContents;
			}
		}

		PS_UploadedContentTypeController _PS_UploadedContentTypes;
		public PS_UploadedContentTypeController PS_UploadedContentTypes
		{
			get
			{
				if (_PS_UploadedContentTypes == null) _PS_UploadedContentTypes = new PS_UploadedContentTypeController();
				return _PS_UploadedContentTypes;
			}
		}

		PS_UserSettingController _PS_UserSettings;
		public PS_UserSettingController PS_UserSettings
		{
			get
			{
				if (_PS_UserSettings == null) _PS_UserSettings = new PS_UserSettingController();
				return _PS_UserSettings;
			}
		}

		QE_MatchingAnswerController _QE_MatchingAnswers;
		public QE_MatchingAnswerController QE_MatchingAnswers
		{
			get
			{
				if (_QE_MatchingAnswers == null) _QE_MatchingAnswers = new QE_MatchingAnswerController();
				return _QE_MatchingAnswers;
			}
		}

		QE_PossibleAnswerController _QE_PossibleAnswers;
		public QE_PossibleAnswerController QE_PossibleAnswers
		{
			get
			{
				if (_QE_PossibleAnswers == null) _QE_PossibleAnswers = new QE_PossibleAnswerController();
				return _QE_PossibleAnswers;
			}
		}

		QE_QuestionResponseController _QE_QuestionResponses;
		public QE_QuestionResponseController QE_QuestionResponses
		{
			get
			{
				if (_QE_QuestionResponses == null) _QE_QuestionResponses = new QE_QuestionResponseController();
				return _QE_QuestionResponses;
			}
		}

		QE_QuestionController _QE_Questions;
		public QE_QuestionController QE_Questions
		{
			get
			{
				if (_QE_Questions == null) _QE_Questions = new QE_QuestionController();
				return _QE_Questions;
			}
		}

		QE_QuizResponseController _QE_QuizResponses;
		public QE_QuizResponseController QE_QuizResponses
		{
			get
			{
				if (_QE_QuizResponses == null) _QE_QuizResponses = new QE_QuizResponseController();
				return _QE_QuizResponses;
			}
		}

		QE_QuizController _QE_Quizzes;
		public QE_QuizController QE_Quizzes
		{
			get
			{
				if (_QE_Quizzes == null) _QE_Quizzes = new QE_QuizController();
				return _QE_Quizzes;
			}
		}

		RU_AuthenticationTokenController _RU_AuthenticationTokens;
		public RU_AuthenticationTokenController RU_AuthenticationTokens
		{
			get
			{
				if (_RU_AuthenticationTokens == null) _RU_AuthenticationTokens = new RU_AuthenticationTokenController();
				return _RU_AuthenticationTokens;
			}
		}

		RU_BoardMessageController _RU_BoardMessages;
		public RU_BoardMessageController RU_BoardMessages
		{
			get
			{
				if (_RU_BoardMessages == null) _RU_BoardMessages = new RU_BoardMessageController();
				return _RU_BoardMessages;
			}
		}

		RU_CommissionChangeHistoryController _RU_CommissionChangeHistories;
		public RU_CommissionChangeHistoryController RU_CommissionChangeHistories
		{
			get
			{
				if (_RU_CommissionChangeHistories == null) _RU_CommissionChangeHistories = new RU_CommissionChangeHistoryController();
				return _RU_CommissionChangeHistories;
			}
		}

		RU_CommissionConfigController _RU_CommissionConfigs;
		public RU_CommissionConfigController RU_CommissionConfigs
		{
			get
			{
				if (_RU_CommissionConfigs == null) _RU_CommissionConfigs = new RU_CommissionConfigController();
				return _RU_CommissionConfigs;
			}
		}

		RU_CommissionPayoutController _RU_CommissionPayouts;
		public RU_CommissionPayoutController RU_CommissionPayouts
		{
			get
			{
				if (_RU_CommissionPayouts == null) _RU_CommissionPayouts = new RU_CommissionPayoutController();
				return _RU_CommissionPayouts;
			}
		}

		RU_CommissionPOBController _RU_CommissionPOBs;
		public RU_CommissionPOBController RU_CommissionPOBs
		{
			get
			{
				if (_RU_CommissionPOBs == null) _RU_CommissionPOBs = new RU_CommissionPOBController();
				return _RU_CommissionPOBs;
			}
		}

		RU_CommissionProcessLogController _RU_CommissionProcessLogs;
		public RU_CommissionProcessLogController RU_CommissionProcessLogs
		{
			get
			{
				if (_RU_CommissionProcessLogs == null) _RU_CommissionProcessLogs = new RU_CommissionProcessLogController();
				return _RU_CommissionProcessLogs;
			}
		}

		RU_CommissionRecordController _RU_CommissionRecords;
		public RU_CommissionRecordController RU_CommissionRecords
		{
			get
			{
				if (_RU_CommissionRecords == null) _RU_CommissionRecords = new RU_CommissionRecordController();
				return _RU_CommissionRecords;
			}
		}

		RU_CommissionSeasonController _RU_CommissionSeasons;
		public RU_CommissionSeasonController RU_CommissionSeasons
		{
			get
			{
				if (_RU_CommissionSeasons == null) _RU_CommissionSeasons = new RU_CommissionSeasonController();
				return _RU_CommissionSeasons;
			}
		}

		RU_CommissionSeason_UserTypeController _RU_CommissionSeason_UserTypes;
		public RU_CommissionSeason_UserTypeController RU_CommissionSeason_UserTypes
		{
			get
			{
				if (_RU_CommissionSeason_UserTypes == null) _RU_CommissionSeason_UserTypes = new RU_CommissionSeason_UserTypeController();
				return _RU_CommissionSeason_UserTypes;
			}
		}

		RU_CommTestController _RU_CommTests;
		public RU_CommTestController RU_CommTests
		{
			get
			{
				if (_RU_CommTests == null) _RU_CommTests = new RU_CommTestController();
				return _RU_CommTests;
			}
		}

		RU_DocStatusController _RU_DocStatuses;
		public RU_DocStatusController RU_DocStatuses
		{
			get
			{
				if (_RU_DocStatuses == null) _RU_DocStatuses = new RU_DocStatusController();
				return _RU_DocStatuses;
			}
		}

		RU_HousingInfoController _RU_HousingInfos;
		public RU_HousingInfoController RU_HousingInfos
		{
			get
			{
				if (_RU_HousingInfos == null) _RU_HousingInfos = new RU_HousingInfoController();
				return _RU_HousingInfos;
			}
		}

		RU_LevelAdvanceController _RU_LevelAdvances;
		public RU_LevelAdvanceController RU_LevelAdvances
		{
			get
			{
				if (_RU_LevelAdvances == null) _RU_LevelAdvances = new RU_LevelAdvanceController();
				return _RU_LevelAdvances;
			}
		}

		RU_LoginAuditController _RU_LoginAudits;
		public RU_LoginAuditController RU_LoginAudits
		{
			get
			{
				if (_RU_LoginAudits == null) _RU_LoginAudits = new RU_LoginAuditController();
				return _RU_LoginAudits;
			}
		}

		RU_MessageAttachmentController _RU_MessageAttachments;
		public RU_MessageAttachmentController RU_MessageAttachments
		{
			get
			{
				if (_RU_MessageAttachments == null) _RU_MessageAttachments = new RU_MessageAttachmentController();
				return _RU_MessageAttachments;
			}
		}

		RU_MessageFormatController _RU_MessageFormats;
		public RU_MessageFormatController RU_MessageFormats
		{
			get
			{
				if (_RU_MessageFormats == null) _RU_MessageFormats = new RU_MessageFormatController();
				return _RU_MessageFormats;
			}
		}

		RU_MessageQueue_MessageAttachment_MapController _RU_MessageQueue_MessageAttachment_Maps;
		public RU_MessageQueue_MessageAttachment_MapController RU_MessageQueue_MessageAttachment_Maps
		{
			get
			{
				if (_RU_MessageQueue_MessageAttachment_Maps == null) _RU_MessageQueue_MessageAttachment_Maps = new RU_MessageQueue_MessageAttachment_MapController();
				return _RU_MessageQueue_MessageAttachment_Maps;
			}
		}

		RU_MessageQueue_User_MapController _RU_MessageQueue_User_Maps;
		public RU_MessageQueue_User_MapController RU_MessageQueue_User_Maps
		{
			get
			{
				if (_RU_MessageQueue_User_Maps == null) _RU_MessageQueue_User_Maps = new RU_MessageQueue_User_MapController();
				return _RU_MessageQueue_User_Maps;
			}
		}

		RU_MessageQueueController _RU_MessageQueues;
		public RU_MessageQueueController RU_MessageQueues
		{
			get
			{
				if (_RU_MessageQueues == null) _RU_MessageQueues = new RU_MessageQueueController();
				return _RU_MessageQueues;
			}
		}

		RU_MigrationController _RU_Migrations;
		public RU_MigrationController RU_Migrations
		{
			get
			{
				if (_RU_Migrations == null) _RU_Migrations = new RU_MigrationController();
				return _RU_Migrations;
			}
		}

		RU_PayscaleController _RU_Payscales;
		public RU_PayscaleController RU_Payscales
		{
			get
			{
				if (_RU_Payscales == null) _RU_Payscales = new RU_PayscaleController();
				return _RU_Payscales;
			}
		}

		RU_PhoneCellCarrierController _RU_PhoneCellCarriers;
		public RU_PhoneCellCarrierController RU_PhoneCellCarriers
		{
			get
			{
				if (_RU_PhoneCellCarriers == null) _RU_PhoneCellCarriers = new RU_PhoneCellCarrierController();
				return _RU_PhoneCellCarriers;
			}
		}

		RU_PhoneTypeController _RU_PhoneTypes;
		public RU_PhoneTypeController RU_PhoneTypes
		{
			get
			{
				if (_RU_PhoneTypes == null) _RU_PhoneTypes = new RU_PhoneTypeController();
				return _RU_PhoneTypes;
			}
		}

		RU_PriorCompanyController _RU_PriorCompanies;
		public RU_PriorCompanyController RU_PriorCompanies
		{
			get
			{
				if (_RU_PriorCompanies == null) _RU_PriorCompanies = new RU_PriorCompanyController();
				return _RU_PriorCompanies;
			}
		}

		RU_PriorCompanySaleController _RU_PriorCompanySales;
		public RU_PriorCompanySaleController RU_PriorCompanySales
		{
			get
			{
				if (_RU_PriorCompanySales == null) _RU_PriorCompanySales = new RU_PriorCompanySaleController();
				return _RU_PriorCompanySales;
			}
		}

		RU_RecruitAddressController _RU_RecruitAddresses;
		public RU_RecruitAddressController RU_RecruitAddresses
		{
			get
			{
				if (_RU_RecruitAddresses == null) _RU_RecruitAddresses = new RU_RecruitAddressController();
				return _RU_RecruitAddresses;
			}
		}

		RU_RecruitCohabbitTypeController _RU_RecruitCohabbitTypes;
		public RU_RecruitCohabbitTypeController RU_RecruitCohabbitTypes
		{
			get
			{
				if (_RU_RecruitCohabbitTypes == null) _RU_RecruitCohabbitTypes = new RU_RecruitCohabbitTypeController();
				return _RU_RecruitCohabbitTypes;
			}
		}

		RU_RecruitGoalController _RU_RecruitGoals;
		public RU_RecruitGoalController RU_RecruitGoals
		{
			get
			{
				if (_RU_RecruitGoals == null) _RU_RecruitGoals = new RU_RecruitGoalController();
				return _RU_RecruitGoals;
			}
		}

		RU_RecruitPolicyAndProcedureController _RU_RecruitPolicyAndProcedures;
		public RU_RecruitPolicyAndProcedureController RU_RecruitPolicyAndProcedures
		{
			get
			{
				if (_RU_RecruitPolicyAndProcedures == null) _RU_RecruitPolicyAndProcedures = new RU_RecruitPolicyAndProcedureController();
				return _RU_RecruitPolicyAndProcedures;
			}
		}

		RU_RecruitRegistrationController _RU_RecruitRegistrations;
		public RU_RecruitRegistrationController RU_RecruitRegistrations
		{
			get
			{
				if (_RU_RecruitRegistrations == null) _RU_RecruitRegistrations = new RU_RecruitRegistrationController();
				return _RU_RecruitRegistrations;
			}
		}

		RU_RecruitController _RU_Recruits;
		public RU_RecruitController RU_Recruits
		{
			get
			{
				if (_RU_Recruits == null) _RU_Recruits = new RU_RecruitController();
				return _RU_Recruits;
			}
		}

		RU_RecruitSeasonGoalController _RU_RecruitSeasonGoals;
		public RU_RecruitSeasonGoalController RU_RecruitSeasonGoals
		{
			get
			{
				if (_RU_RecruitSeasonGoals == null) _RU_RecruitSeasonGoals = new RU_RecruitSeasonGoalController();
				return _RU_RecruitSeasonGoals;
			}
		}

		RU_RecruitsHistoryController _RU_RecruitsHistories;
		public RU_RecruitsHistoryController RU_RecruitsHistories
		{
			get
			{
				if (_RU_RecruitsHistories == null) _RU_RecruitsHistories = new RU_RecruitsHistoryController();
				return _RU_RecruitsHistories;
			}
		}

		RU_RoleLocationController _RU_RoleLocations;
		public RU_RoleLocationController RU_RoleLocations
		{
			get
			{
				if (_RU_RoleLocations == null) _RU_RoleLocations = new RU_RoleLocationController();
				return _RU_RoleLocations;
			}
		}

		RU_RollCallRecordController _RU_RollCallRecords;
		public RU_RollCallRecordController RU_RollCallRecords
		{
			get
			{
				if (_RU_RollCallRecords == null) _RU_RollCallRecords = new RU_RollCallRecordController();
				return _RU_RollCallRecords;
			}
		}

		RU_RollCallController _RU_RollCalls;
		public RU_RollCallController RU_RollCalls
		{
			get
			{
				if (_RU_RollCalls == null) _RU_RollCalls = new RU_RollCallController();
				return _RU_RollCalls;
			}
		}

		RU_SchoolController _RU_Schools;
		public RU_SchoolController RU_Schools
		{
			get
			{
				if (_RU_Schools == null) _RU_Schools = new RU_SchoolController();
				return _RU_Schools;
			}
		}

		RU_SeasonController _RU_Seasons;
		public RU_SeasonController RU_Seasons
		{
			get
			{
				if (_RU_Seasons == null) _RU_Seasons = new RU_SeasonController();
				return _RU_Seasons;
			}
		}

		RU_SeasonSummerController _RU_SeasonSummers;
		public RU_SeasonSummerController RU_SeasonSummers
		{
			get
			{
				if (_RU_SeasonSummers == null) _RU_SeasonSummers = new RU_SeasonSummerController();
				return _RU_SeasonSummers;
			}
		}

		RU_SeasonSummerSeason_MapController _RU_SeasonSummerSeason_Maps;
		public RU_SeasonSummerSeason_MapController RU_SeasonSummerSeason_Maps
		{
			get
			{
				if (_RU_SeasonSummerSeason_Maps == null) _RU_SeasonSummerSeason_Maps = new RU_SeasonSummerSeason_MapController();
				return _RU_SeasonSummerSeason_Maps;
			}
		}

		RU_SeasonTeamLocationDefaultController _RU_SeasonTeamLocationDefaults;
		public RU_SeasonTeamLocationDefaultController RU_SeasonTeamLocationDefaults
		{
			get
			{
				if (_RU_SeasonTeamLocationDefaults == null) _RU_SeasonTeamLocationDefaults = new RU_SeasonTeamLocationDefaultController();
				return _RU_SeasonTeamLocationDefaults;
			}
		}

		RU_SiteCodeController _RU_SiteCodes;
		public RU_SiteCodeController RU_SiteCodes
		{
			get
			{
				if (_RU_SiteCodes == null) _RU_SiteCodes = new RU_SiteCodeController();
				return _RU_SiteCodes;
			}
		}

		RU_TeamLocationRosterController _RU_TeamLocationRosters;
		public RU_TeamLocationRosterController RU_TeamLocationRosters
		{
			get
			{
				if (_RU_TeamLocationRosters == null) _RU_TeamLocationRosters = new RU_TeamLocationRosterController();
				return _RU_TeamLocationRosters;
			}
		}

		RU_TeamLocationController _RU_TeamLocations;
		public RU_TeamLocationController RU_TeamLocations
		{
			get
			{
				if (_RU_TeamLocations == null) _RU_TeamLocations = new RU_TeamLocationController();
				return _RU_TeamLocations;
			}
		}

		RU_TeamLocationsAndUserController _RU_TeamLocationsAndUsers;
		public RU_TeamLocationsAndUserController RU_TeamLocationsAndUsers
		{
			get
			{
				if (_RU_TeamLocationsAndUsers == null) _RU_TeamLocationsAndUsers = new RU_TeamLocationsAndUserController();
				return _RU_TeamLocationsAndUsers;
			}
		}

		RU_TeamLocationStateMappingController _RU_TeamLocationStateMappings;
		public RU_TeamLocationStateMappingController RU_TeamLocationStateMappings
		{
			get
			{
				if (_RU_TeamLocationStateMappings == null) _RU_TeamLocationStateMappings = new RU_TeamLocationStateMappingController();
				return _RU_TeamLocationStateMappings;
			}
		}

		RU_TeamController _RU_Teams;
		public RU_TeamController RU_Teams
		{
			get
			{
				if (_RU_Teams == null) _RU_Teams = new RU_TeamController();
				return _RU_Teams;
			}
		}

		RU_TerminationCategoryController _RU_TerminationCategories;
		public RU_TerminationCategoryController RU_TerminationCategories
		{
			get
			{
				if (_RU_TerminationCategories == null) _RU_TerminationCategories = new RU_TerminationCategoryController();
				return _RU_TerminationCategories;
			}
		}

		RU_TerminationNoteController _RU_TerminationNotes;
		public RU_TerminationNoteController RU_TerminationNotes
		{
			get
			{
				if (_RU_TerminationNotes == null) _RU_TerminationNotes = new RU_TerminationNoteController();
				return _RU_TerminationNotes;
			}
		}

		RU_TerminationReasonController _RU_TerminationReasons;
		public RU_TerminationReasonController RU_TerminationReasons
		{
			get
			{
				if (_RU_TerminationReasons == null) _RU_TerminationReasons = new RU_TerminationReasonController();
				return _RU_TerminationReasons;
			}
		}

		RU_TerminationController _RU_Terminations;
		public RU_TerminationController RU_Terminations
		{
			get
			{
				if (_RU_Terminations == null) _RU_Terminations = new RU_TerminationController();
				return _RU_Terminations;
			}
		}

		RU_TerminationStatusCodeController _RU_TerminationStatusCodes;
		public RU_TerminationStatusCodeController RU_TerminationStatusCodes
		{
			get
			{
				if (_RU_TerminationStatusCodes == null) _RU_TerminationStatusCodes = new RU_TerminationStatusCodeController();
				return _RU_TerminationStatusCodes;
			}
		}

		RU_TerminationStatusController _RU_TerminationStatuses;
		public RU_TerminationStatusController RU_TerminationStatuses
		{
			get
			{
				if (_RU_TerminationStatuses == null) _RU_TerminationStatuses = new RU_TerminationStatusController();
				return _RU_TerminationStatuses;
			}
		}

		RU_TerminationTypeController _RU_TerminationTypes;
		public RU_TerminationTypeController RU_TerminationTypes
		{
			get
			{
				if (_RU_TerminationTypes == null) _RU_TerminationTypes = new RU_TerminationTypeController();
				return _RU_TerminationTypes;
			}
		}

		RU_UserAuthenticationController _RU_UserAuthentications;
		public RU_UserAuthenticationController RU_UserAuthentications
		{
			get
			{
				if (_RU_UserAuthentications == null) _RU_UserAuthentications = new RU_UserAuthenticationController();
				return _RU_UserAuthentications;
			}
		}

		RU_UserEmployeeTypeController _RU_UserEmployeeTypes;
		public RU_UserEmployeeTypeController RU_UserEmployeeTypes
		{
			get
			{
				if (_RU_UserEmployeeTypes == null) _RU_UserEmployeeTypes = new RU_UserEmployeeTypeController();
				return _RU_UserEmployeeTypes;
			}
		}

		RU_UserPhotoController _RU_UserPhotos;
		public RU_UserPhotoController RU_UserPhotos
		{
			get
			{
				if (_RU_UserPhotos == null) _RU_UserPhotos = new RU_UserPhotoController();
				return _RU_UserPhotos;
			}
		}

		RU_UserController _RU_Users;
		public RU_UserController RU_Users
		{
			get
			{
				if (_RU_Users == null) _RU_Users = new RU_UserController();
				return _RU_Users;
			}
		}

		RU_UsersHistoryController _RU_UsersHistories;
		public RU_UsersHistoryController RU_UsersHistories
		{
			get
			{
				if (_RU_UsersHistories == null) _RU_UsersHistories = new RU_UsersHistoryController();
				return _RU_UsersHistories;
			}
		}

		RU_UserTypeController _RU_UserTypes;
		public RU_UserTypeController RU_UserTypes
		{
			get
			{
				if (_RU_UserTypes == null) _RU_UserTypes = new RU_UserTypeController();
				return _RU_UserTypes;
			}
		}

		RU_UserTypeTeamTypeController _RU_UserTypeTeamTypes;
		public RU_UserTypeTeamTypeController RU_UserTypeTeamTypes
		{
			get
			{
				if (_RU_UserTypeTeamTypes == null) _RU_UserTypeTeamTypes = new RU_UserTypeTeamTypeController();
				return _RU_UserTypeTeamTypes;
			}
		}

		SAE_AccountInformationController _SAE_AccountInformations;
		public SAE_AccountInformationController SAE_AccountInformations
		{
			get
			{
				if (_SAE_AccountInformations == null) _SAE_AccountInformations = new SAE_AccountInformationController();
				return _SAE_AccountInformations;
			}
		}

		SAE_AccountsInstalledController _SAE_AccountsInstalleds;
		public SAE_AccountsInstalledController SAE_AccountsInstalleds
		{
			get
			{
				if (_SAE_AccountsInstalleds == null) _SAE_AccountsInstalleds = new SAE_AccountsInstalledController();
				return _SAE_AccountsInstalleds;
			}
		}

		SAE_DateController _SAE_Dates;
		public SAE_DateController SAE_Dates
		{
			get
			{
				if (_SAE_Dates == null) _SAE_Dates = new SAE_DateController();
				return _SAE_Dates;
			}
		}

		SAE_GPRM00103Controller _SAE_GPRM00103S;
		public SAE_GPRM00103Controller SAE_GPRM00103S
		{
			get
			{
				if (_SAE_GPRM00103S == null) _SAE_GPRM00103S = new SAE_GPRM00103Controller();
				return _SAE_GPRM00103S;
			}
		}

		SAE_IndividualSalesRecordController _SAE_IndividualSalesRecords;
		public SAE_IndividualSalesRecordController SAE_IndividualSalesRecords
		{
			get
			{
				if (_SAE_IndividualSalesRecords == null) _SAE_IndividualSalesRecords = new SAE_IndividualSalesRecordController();
				return _SAE_IndividualSalesRecords;
			}
		}

		SAE_MaxCreditController _SAE_MaxCredits;
		public SAE_MaxCreditController SAE_MaxCredits
		{
			get
			{
				if (_SAE_MaxCredits == null) _SAE_MaxCredits = new SAE_MaxCreditController();
				return _SAE_MaxCredits;
			}
		}

		SAE_OfficeSalesRecordController _SAE_OfficeSalesRecords;
		public SAE_OfficeSalesRecordController SAE_OfficeSalesRecords
		{
			get
			{
				if (_SAE_OfficeSalesRecords == null) _SAE_OfficeSalesRecords = new SAE_OfficeSalesRecordController();
				return _SAE_OfficeSalesRecords;
			}
		}

		SAE_OfficeUpdateController _SAE_OfficeUpdates;
		public SAE_OfficeUpdateController SAE_OfficeUpdates
		{
			get
			{
				if (_SAE_OfficeUpdates == null) _SAE_OfficeUpdates = new SAE_OfficeUpdateController();
				return _SAE_OfficeUpdates;
			}
		}

		SAE_OfficeUpdateNotController _SAE_OfficeUpdateNots;
		public SAE_OfficeUpdateNotController SAE_OfficeUpdateNots
		{
			get
			{
				if (_SAE_OfficeUpdateNots == null) _SAE_OfficeUpdateNots = new SAE_OfficeUpdateNotController();
				return _SAE_OfficeUpdateNots;
			}
		}

		SAE_RecruitingStructureController _SAE_RecruitingStructures;
		public SAE_RecruitingStructureController SAE_RecruitingStructures
		{
			get
			{
				if (_SAE_RecruitingStructures == null) _SAE_RecruitingStructures = new SAE_RecruitingStructureController();
				return _SAE_RecruitingStructures;
			}
		}

		SAE_RecruitTeamMappingController _SAE_RecruitTeamMappings;
		public SAE_RecruitTeamMappingController SAE_RecruitTeamMappings
		{
			get
			{
				if (_SAE_RecruitTeamMappings == null) _SAE_RecruitTeamMappings = new SAE_RecruitTeamMappingController();
				return _SAE_RecruitTeamMappings;
			}
		}

		SAE_RemoveMeController _SAE_RemoveMes;
		public SAE_RemoveMeController SAE_RemoveMes
		{
			get
			{
				if (_SAE_RemoveMes == null) _SAE_RemoveMes = new SAE_RemoveMeController();
				return _SAE_RemoveMes;
			}
		}

		SAE_RepSalesTotalsSnapShotController _SAE_RepSalesTotalsSnapShots;
		public SAE_RepSalesTotalsSnapShotController SAE_RepSalesTotalsSnapShots
		{
			get
			{
				if (_SAE_RepSalesTotalsSnapShots == null) _SAE_RepSalesTotalsSnapShots = new SAE_RepSalesTotalsSnapShotController();
				return _SAE_RepSalesTotalsSnapShots;
			}
		}

		SAE_TeamRecruitSnapShotController _SAE_TeamRecruitSnapShots;
		public SAE_TeamRecruitSnapShotController SAE_TeamRecruitSnapShots
		{
			get
			{
				if (_SAE_TeamRecruitSnapShots == null) _SAE_TeamRecruitSnapShots = new SAE_TeamRecruitSnapShotController();
				return _SAE_TeamRecruitSnapShots;
			}
		}

		SAE_TechInspectionPercentageController _SAE_TechInspectionPercentages;
		public SAE_TechInspectionPercentageController SAE_TechInspectionPercentages
		{
			get
			{
				if (_SAE_TechInspectionPercentages == null) _SAE_TechInspectionPercentages = new SAE_TechInspectionPercentageController();
				return _SAE_TechInspectionPercentages;
			}
		}

		SAE_TechInspectionScoreController _SAE_TechInspectionScores;
		public SAE_TechInspectionScoreController SAE_TechInspectionScores
		{
			get
			{
				if (_SAE_TechInspectionScores == null) _SAE_TechInspectionScores = new SAE_TechInspectionScoreController();
				return _SAE_TechInspectionScores;
			}
		}

		SAE_ValidSaleController _SAE_ValidSales;
		public SAE_ValidSaleController SAE_ValidSales
		{
			get
			{
				if (_SAE_ValidSales == null) _SAE_ValidSales = new SAE_ValidSaleController();
				return _SAE_ValidSales;
			}
		}

		SOP30200Controller _SOP30200S;
		public SOP30200Controller SOP30200S
		{
			get
			{
				if (_SOP30200S == null) _SOP30200S = new SOP30200Controller();
				return _SOP30200S;
			}
		}

		SY_RecruitSurveyController _SY_RecruitSurveys;
		public SY_RecruitSurveyController SY_RecruitSurveys
		{
			get
			{
				if (_SY_RecruitSurveys == null) _SY_RecruitSurveys = new SY_RecruitSurveyController();
				return _SY_RecruitSurveys;
			}
		}

		TempViewController _TempViews;
		public TempViewController TempViews
		{
			get
			{
				if (_TempViews == null) _TempViews = new TempViewController();
				return _TempViews;
			}
		}

		#endregion //Controllers Properties

		#region View Controllers Properties

		AllRegionalsViewController _AllRegionalsViews;
		public AllRegionalsViewController AllRegionalsViews
		{
			get
			{
				if (_AllRegionalsViews == null) _AllRegionalsViews = new AllRegionalsViewController();
				return _AllRegionalsViews;
			}
		}

		AllSalesManagersViewController _AllSalesManagersViews;
		public AllSalesManagersViewController AllSalesManagersViews
		{
			get
			{
				if (_AllSalesManagersViews == null) _AllSalesManagersViews = new AllSalesManagersViewController();
				return _AllSalesManagersViews;
			}
		}

		RecruitingLineViewController _RecruitingLineViews;
		public RecruitingLineViewController RecruitingLineViews
		{
			get
			{
				if (_RecruitingLineViews == null) _RecruitingLineViews = new RecruitingLineViewController();
				return _RecruitingLineViews;
			}
		}

		RecruitingLineMobileViewController _RecruitingLineMobileViews;
		public RecruitingLineMobileViewController RecruitingLineMobileViews
		{
			get
			{
				if (_RecruitingLineMobileViews == null) _RecruitingLineMobileViews = new RecruitingLineMobileViewController();
				return _RecruitingLineMobileViews;
			}
		}

		RecruitingStructureViewController _RecruitingStructureViews;
		public RecruitingStructureViewController RecruitingStructureViews
		{
			get
			{
				if (_RecruitingStructureViews == null) _RecruitingStructureViews = new RecruitingStructureViewController();
				return _RecruitingStructureViews;
			}
		}

		RecruitPayrollStatusViewController _RecruitPayrollStatusViews;
		public RecruitPayrollStatusViewController RecruitPayrollStatusViews
		{
			get
			{
				if (_RecruitPayrollStatusViews == null) _RecruitPayrollStatusViews = new RecruitPayrollStatusViewController();
				return _RecruitPayrollStatusViews;
			}
		}

		RecruitUserViewController _RecruitUserViews;
		public RecruitUserViewController RecruitUserViews
		{
			get
			{
				if (_RecruitUserViews == null) _RecruitUserViews = new RecruitUserViewController();
				return _RecruitUserViews;
			}
		}

		TeamsViewController _TeamsViews;
		public TeamsViewController TeamsViews
		{
			get
			{
				if (_TeamsViews == null) _TeamsViews = new TeamsViewController();
				return _TeamsViews;
			}
		}

		TeamSalesManagersViewController _TeamSalesManagersViews;
		public TeamSalesManagersViewController TeamSalesManagersViews
		{
			get
			{
				if (_TeamSalesManagersViews == null) _TeamSalesManagersViews = new TeamSalesManagersViewController();
				return _TeamSalesManagersViews;
			}
		}

		RU_RecruitUserViewController _RU_RecruitUserViews;
		public RU_RecruitUserViewController RU_RecruitUserViews
		{
			get
			{
				if (_RU_RecruitUserViews == null) _RU_RecruitUserViews = new RU_RecruitUserViewController();
				return _RU_RecruitUserViews;
			}
		}

		RU_RollCallRecordsLatestByRecruitViewController _RU_RollCallRecordsLatestByRecruitViews;
		public RU_RollCallRecordsLatestByRecruitViewController RU_RollCallRecordsLatestByRecruitViews
		{
			get
			{
				if (_RU_RollCallRecordsLatestByRecruitViews == null) _RU_RollCallRecordsLatestByRecruitViews = new RU_RollCallRecordsLatestByRecruitViewController();
				return _RU_RollCallRecordsLatestByRecruitViews;
			}
		}

		RU_SalesRepsViewController _RU_SalesRepsViews;
		public RU_SalesRepsViewController RU_SalesRepsViews
		{
			get
			{
				if (_RU_SalesRepsViews == null) _RU_SalesRepsViews = new RU_SalesRepsViewController();
				return _RU_SalesRepsViews;
			}
		}

		RU_TeamLocationViewController _RU_TeamLocationViews;
		public RU_TeamLocationViewController RU_TeamLocationViews
		{
			get
			{
				if (_RU_TeamLocationViews == null) _RU_TeamLocationViews = new RU_TeamLocationViewController();
				return _RU_TeamLocationViews;
			}
		}

		RU_TeamLocationRosterByWeekViewController _RU_TeamLocationRosterByWeekViews;
		public RU_TeamLocationRosterByWeekViewController RU_TeamLocationRosterByWeekViews
		{
			get
			{
				if (_RU_TeamLocationRosterByWeekViews == null) _RU_TeamLocationRosterByWeekViews = new RU_TeamLocationRosterByWeekViewController();
				return _RU_TeamLocationRosterByWeekViews;
			}
		}

		RU_TeamLocationRosterTransfersViewController _RU_TeamLocationRosterTransfersViews;
		public RU_TeamLocationRosterTransfersViewController RU_TeamLocationRosterTransfersViews
		{
			get
			{
				if (_RU_TeamLocationRosterTransfersViews == null) _RU_TeamLocationRosterTransfersViews = new RU_TeamLocationRosterTransfersViewController();
				return _RU_TeamLocationRosterTransfersViews;
			}
		}

		RU_TeamLocatonRosterCurrentByRecruitViewController _RU_TeamLocatonRosterCurrentByRecruitViews;
		public RU_TeamLocatonRosterCurrentByRecruitViewController RU_TeamLocatonRosterCurrentByRecruitViews
		{
			get
			{
				if (_RU_TeamLocatonRosterCurrentByRecruitViews == null) _RU_TeamLocatonRosterCurrentByRecruitViews = new RU_TeamLocatonRosterCurrentByRecruitViewController();
				return _RU_TeamLocatonRosterCurrentByRecruitViews;
			}
		}

		RU_TechniciansViewController _RU_TechniciansViews;
		public RU_TechniciansViewController RU_TechniciansViews
		{
			get
			{
				if (_RU_TechniciansViews == null) _RU_TechniciansViews = new RU_TechniciansViewController();
				return _RU_TechniciansViews;
			}
		}

		RU_TerminationStatusCurrentStatusViewController _RU_TerminationStatusCurrentStatusViews;
		public RU_TerminationStatusCurrentStatusViewController RU_TerminationStatusCurrentStatusViews
		{
			get
			{
				if (_RU_TerminationStatusCurrentStatusViews == null) _RU_TerminationStatusCurrentStatusViews = new RU_TerminationStatusCurrentStatusViewController();
				return _RU_TerminationStatusCurrentStatusViews;
			}
		}

		RU_TerminationsWithStatusViewController _RU_TerminationsWithStatusViews;
		public RU_TerminationsWithStatusViewController RU_TerminationsWithStatusViews
		{
			get
			{
				if (_RU_TerminationsWithStatusViews == null) _RU_TerminationsWithStatusViews = new RU_TerminationsWithStatusViewController();
				return _RU_TerminationsWithStatusViews;
			}
		}

		RU_UserSalesInfoConnextViewController _RU_UserSalesInfoConnextViews;
		public RU_UserSalesInfoConnextViewController RU_UserSalesInfoConnextViews
		{
			get
			{
				if (_RU_UserSalesInfoConnextViews == null) _RU_UserSalesInfoConnextViews = new RU_UserSalesInfoConnextViewController();
				return _RU_UserSalesInfoConnextViews;
			}
		}

		RU_UsersCallerIDViewController _RU_UsersCallerIDViews;
		public RU_UsersCallerIDViewController RU_UsersCallerIDViews
		{
			get
			{
				if (_RU_UsersCallerIDViews == null) _RU_UsersCallerIDViews = new RU_UsersCallerIDViewController();
				return _RU_UsersCallerIDViews;
			}
		}

		RU_UsersTechViewController _RU_UsersTechViews;
		public RU_UsersTechViewController RU_UsersTechViews
		{
			get
			{
				if (_RU_UsersTechViews == null) _RU_UsersTechViews = new RU_UsersTechViewController();
				return _RU_UsersTechViews;
			}
		}

		TechniciansViewController _TechniciansViews;
		public TechniciansViewController TechniciansViews
		{
			get
			{
				if (_TechniciansViews == null) _TechniciansViews = new TechniciansViewController();
				return _TechniciansViews;
			}
		}

		#endregion //View Controllers Properties
	}

	#region Controllers

	public class APT_AddressController : BaseTableController<APT_Address, APT_AddressCollection> { }
	public class APT_ApartmentController : BaseTableController<APT_Apartment, APT_ApartmentCollection> { }
	public class APT_ApartmentBySeasonController : BaseTableController<APT_ApartmentBySeason, APT_ApartmentBySeasonCollection> { }
	public class APT_ApartmentFurnitureItemController : BaseTableController<APT_ApartmentFurnitureItem, APT_ApartmentFurnitureItemCollection> { }
	public class APT_ApartmentNoteController : BaseTableController<APT_ApartmentNote, APT_ApartmentNoteCollection> { }
	public class APT_ApartmentTypeController : BaseTableController<APT_ApartmentType, APT_ApartmentTypeCollection> { }
	public class APT_BedController : BaseTableController<APT_Bed, APT_BedCollection> { }
	public class APT_BedroomController : BaseTableController<APT_Bedroom, APT_BedroomCollection> { }
	public class APT_BedTypeController : BaseTableController<APT_BedType, APT_BedTypeCollection> { }
	public class APT_ComplaintController : BaseTableController<APT_Complaint, APT_ComplaintCollection> { }
	public class APT_ComplexController : BaseTableController<APT_Complex, APT_ComplexCollection> { }
	public class APT_ContactController : BaseTableController<APT_Contact, APT_ContactCollection> { }
	public class APT_ContactTypeController : BaseTableController<APT_ContactType, APT_ContactTypeCollection> { }
	public class APT_DefaultFurniturePackageController : BaseTableController<APT_DefaultFurniturePackage, APT_DefaultFurniturePackageCollection> { }
	public class APT_DefaultFurniturePackageContentController : BaseTableController<APT_DefaultFurniturePackageContent, APT_DefaultFurniturePackageContentCollection> { }
	public class APT_DocumentController : BaseTableController<APT_Document, APT_DocumentCollection> { }
	public class APT_DocumentTypeController : BaseTableController<APT_DocumentType, APT_DocumentTypeCollection> { }
	public class APT_ExpenseController : BaseTableController<APT_Expense, APT_ExpenseCollection> { }
	public class APT_ExpenseBillController : BaseTableController<APT_ExpenseBill, APT_ExpenseBillCollection> { }
	public class APT_ExpensePaymentController : BaseTableController<APT_ExpensePayment, APT_ExpensePaymentCollection> { }
	public class APT_ExpenseTypeController : BaseTableController<APT_ExpenseType, APT_ExpenseTypeCollection> { }
	public class APT_FurniturePackageController : BaseTableController<APT_FurniturePackage, APT_FurniturePackageCollection> { }
	public class APT_FurniturePieceController : BaseTableController<APT_FurniturePiece, APT_FurniturePieceCollection> { }
	public class APT_ManagementCompanyController : BaseTableController<APT_ManagementCompany, APT_ManagementCompanyCollection> { }
	public class APT_OccupantController : BaseTableController<APT_Occupant, APT_OccupantCollection> { }
	public class APT_RecruitChargeController : BaseTableController<APT_RecruitCharge, APT_RecruitChargeCollection> { }
	public class dtpropertyController : BaseTableController<dtproperty, dtpropertyCollection> { }
	public class ES_MessageAttachmentController : BaseTableController<ES_MessageAttachment, ES_MessageAttachmentCollection> { }
	public class ES_MessageRecipientController : BaseTableController<ES_MessageRecipient, ES_MessageRecipientCollection> { }
	public class ES_MessageController : BaseTableController<ES_Message, ES_MessageCollection> { }
	public class GP_CustAcctSummaryController : BaseTableController<GP_CustAcctSummary, GP_CustAcctSummaryCollection> { }
	public class HS_ComplexController : BaseTableController<HS_Complex, HS_ComplexCollection> { }
	public class HS_ComplexCheckTypeController : BaseTableController<HS_ComplexCheckType, HS_ComplexCheckTypeCollection> { }
	public class HS_ComplexNoteController : BaseTableController<HS_ComplexNote, HS_ComplexNoteCollection> { }
	public class HS_ContactInformationController : BaseTableController<HS_ContactInformation, HS_ContactInformationCollection> { }
	public class HS_ContactInformationAddressController : BaseTableController<HS_ContactInformationAddress, HS_ContactInformationAddressCollection> { }
	public class HS_ContactInformationAddressTypeController : BaseTableController<HS_ContactInformationAddressType, HS_ContactInformationAddressTypeCollection> { }
	public class HS_FurnitureCompanyController : BaseTableController<HS_FurnitureCompany, HS_FurnitureCompanyCollection> { }
	public class HS_FurnitureItemController : BaseTableController<HS_FurnitureItem, HS_FurnitureItemCollection> { }
	public class HS_FurniturePackageController : BaseTableController<HS_FurniturePackage, HS_FurniturePackageCollection> { }
	public class HS_FurniturePackageContentController : BaseTableController<HS_FurniturePackageContent, HS_FurniturePackageContentCollection> { }
	public class HS_HousingUserController : BaseTableController<HS_HousingUser, HS_HousingUserCollection> { }
	public class HS_OfficeComplexController : BaseTableController<HS_OfficeComplex, HS_OfficeComplexCollection> { }
	public class HS_OfficeComplexUnitController : BaseTableController<HS_OfficeComplexUnit, HS_OfficeComplexUnitCollection> { }
	public class HS_UnitController : BaseTableController<HS_Unit, HS_UnitCollection> { }
	public class HS_UnitAddressController : BaseTableController<HS_UnitAddress, HS_UnitAddressCollection> { }
	public class HS_UnitDamageController : BaseTableController<HS_UnitDamage, HS_UnitDamageCollection> { }
	public class HS_UnitDamageUserController : BaseTableController<HS_UnitDamageUser, HS_UnitDamageUserCollection> { }
	public class HS_UnitFurniturePackageController : BaseTableController<HS_UnitFurniturePackage, HS_UnitFurniturePackageCollection> { }
	public class HS_UnitNoteController : BaseTableController<HS_UnitNote, HS_UnitNoteCollection> { }
	public class HS_UnitOccupantController : BaseTableController<HS_UnitOccupant, HS_UnitOccupantCollection> { }
	public class HS_UnitTypeController : BaseTableController<HS_UnitType, HS_UnitTypeCollection> { }
	public class HS_UnitVacateNoticeSentByTypeController : BaseTableController<HS_UnitVacateNoticeSentByType, HS_UnitVacateNoticeSentByTypeCollection> { }
	public class MC_PoliticalCountryController : BaseTableController<MC_PoliticalCountry, MC_PoliticalCountryCollection> { }
	public class MC_PoliticalStateController : BaseTableController<MC_PoliticalState, MC_PoliticalStateCollection> { }
	public class MC_PoliticalTimeZoneController : BaseTableController<MC_PoliticalTimeZone, MC_PoliticalTimeZoneCollection> { }
	public class PR_AccountCalculationController : BaseTableController<PR_AccountCalculation, PR_AccountCalculationCollection> { }
	public class PR_AccountStateExceptionController : BaseTableController<PR_AccountStateException, PR_AccountStateExceptionCollection> { }
	public class PR_AccountStateController : BaseTableController<PR_AccountState, PR_AccountStateCollection> { }
	public class PR_AccountsToIgnoreController : BaseTableController<PR_AccountsToIgnore, PR_AccountsToIgnoreCollection> { }
	public class PR_AutomaticTransactionController : BaseTableController<PR_AutomaticTransaction, PR_AutomaticTransactionCollection> { }
	public class PR_BackendAccountCalculationController : BaseTableController<PR_BackendAccountCalculation, PR_BackendAccountCalculationCollection> { }
	public class PR_BackendAccountExceptionController : BaseTableController<PR_BackendAccountException, PR_BackendAccountExceptionCollection> { }
	public class PR_BackendAccountMappingController : BaseTableController<PR_BackendAccountMapping, PR_BackendAccountMappingCollection> { }
	public class PR_BackendAccountMappingTypeController : BaseTableController<PR_BackendAccountMappingType, PR_BackendAccountMappingTypeCollection> { }
	public class PR_BackendAccountNumbersExceptionController : BaseTableController<PR_BackendAccountNumbersException, PR_BackendAccountNumbersExceptionCollection> { }
	public class PR_BackendAdjustmentController : BaseTableController<PR_BackendAdjustment, PR_BackendAdjustmentCollection> { }
	public class PR_BackendAggregateTransactionController : BaseTableController<PR_BackendAggregateTransaction, PR_BackendAggregateTransactionCollection> { }
	public class PR_BackendAggregateTransactionTypeController : BaseTableController<PR_BackendAggregateTransactionType, PR_BackendAggregateTransactionTypeCollection> { }
	public class PR_BackendExceptionController : BaseTableController<PR_BackendException, PR_BackendExceptionCollection> { }
	public class PR_BackendLegionController : BaseTableController<PR_BackendLegion, PR_BackendLegionCollection> { }
	public class PR_BackendManagerPayscaleController : BaseTableController<PR_BackendManagerPayscale, PR_BackendManagerPayscaleCollection> { }
	public class PR_BackendManagerPayScheduleController : BaseTableController<PR_BackendManagerPaySchedule, PR_BackendManagerPayScheduleCollection> { }
	public class PR_BackendPayscaleController : BaseTableController<PR_BackendPayscale, PR_BackendPayscaleCollection> { }
	public class PR_BackendPayScheduleController : BaseTableController<PR_BackendPaySchedule, PR_BackendPayScheduleCollection> { }
	public class PR_BackendPeriodController : BaseTableController<PR_BackendPeriod, PR_BackendPeriodCollection> { }
	public class PR_BackendPeriodSeasonMappingController : BaseTableController<PR_BackendPeriodSeasonMapping, PR_BackendPeriodSeasonMappingCollection> { }
	public class PR_BackendPeriodUserTypeMappingController : BaseTableController<PR_BackendPeriodUserTypeMapping, PR_BackendPeriodUserTypeMappingCollection> { }
	public class PR_BackendQualificationExceptionController : BaseTableController<PR_BackendQualificationException, PR_BackendQualificationExceptionCollection> { }
	public class PR_BackendRentExceptionController : BaseTableController<PR_BackendRentException, PR_BackendRentExceptionCollection> { }
	public class PR_BackendController : BaseTableController<PR_Backend, PR_BackendCollection> { }
	public class PR_BackendTransactionCategoryController : BaseTableController<PR_BackendTransactionCategory, PR_BackendTransactionCategoryCollection> { }
	public class PR_BackendTransactionController : BaseTableController<PR_BackendTransaction, PR_BackendTransactionCollection> { }
	public class PR_BackendTransactionTypeController : BaseTableController<PR_BackendTransactionType, PR_BackendTransactionTypeCollection> { }
	public class PR_BarcodesToIgnoreController : BaseTableController<PR_BarcodesToIgnore, PR_BarcodesToIgnoreCollection> { }
	public class PR_BatchController : BaseTableController<PR_Batch, PR_BatchCollection> { }
	public class PR_HistoricalPaycheckController : BaseTableController<PR_HistoricalPaycheck, PR_HistoricalPaycheckCollection> { }
	public class PR_HistoricalTransactionController : BaseTableController<PR_HistoricalTransaction, PR_HistoricalTransactionCollection> { }
	public class PR_LeadTechBackendController : BaseTableController<PR_LeadTechBackend, PR_LeadTechBackendCollection> { }
	public class PR_MiscBackendAmountController : BaseTableController<PR_MiscBackendAmount, PR_MiscBackendAmountCollection> { }
	public class PR_MiscBackendTransactionController : BaseTableController<PR_MiscBackendTransaction, PR_MiscBackendTransactionCollection> { }
	public class PR_OfficeCheckRecipientController : BaseTableController<PR_OfficeCheckRecipient, PR_OfficeCheckRecipientCollection> { }
	public class PR_OfficeCheckController : BaseTableController<PR_OfficeCheck, PR_OfficeCheckCollection> { }
	public class PR_OfficeTransactionController : BaseTableController<PR_OfficeTransaction, PR_OfficeTransactionCollection> { }
	public class PR_OfficeTransactionTypeController : BaseTableController<PR_OfficeTransactionType, PR_OfficeTransactionTypeCollection> { }
	public class PR_PaycheckController : BaseTableController<PR_Paycheck, PR_PaycheckCollection> { }
	public class PR_PayPeriodController : BaseTableController<PR_PayPeriod, PR_PayPeriodCollection> { }
	public class PR_PayPeriodSeasonMappingController : BaseTableController<PR_PayPeriodSeasonMapping, PR_PayPeriodSeasonMappingCollection> { }
	public class PR_PayPeriodValidAccountSeasonController : BaseTableController<PR_PayPeriodValidAccountSeason, PR_PayPeriodValidAccountSeasonCollection> { }
	public class PR_PayScheduleController : BaseTableController<PR_PaySchedule, PR_PayScheduleCollection> { }
	public class PR_RegionalResidualAccountMappingController : BaseTableController<PR_RegionalResidualAccountMapping, PR_RegionalResidualAccountMappingCollection> { }
	public class PR_RegionalResidualPayeeController : BaseTableController<PR_RegionalResidualPayee, PR_RegionalResidualPayeeCollection> { }
	public class PR_RegionalResidualPeriodController : BaseTableController<PR_RegionalResidualPeriod, PR_RegionalResidualPeriodCollection> { }
	public class PR_RegionalResidualController : BaseTableController<PR_RegionalResidual, PR_RegionalResidualCollection> { }
	public class PR_RollingTransactionController : BaseTableController<PR_RollingTransaction, PR_RollingTransactionCollection> { }
	public class PR_RollingTransactionTypeController : BaseTableController<PR_RollingTransactionType, PR_RollingTransactionTypeCollection> { }
	public class PR_SalesManagerBackendController : BaseTableController<PR_SalesManagerBackend, PR_SalesManagerBackendCollection> { }
	public class PR_SalesRegionalBackendController : BaseTableController<PR_SalesRegionalBackend, PR_SalesRegionalBackendCollection> { }
	public class PR_SalesRegionalBackendTotalController : BaseTableController<PR_SalesRegionalBackendTotal, PR_SalesRegionalBackendTotalCollection> { }
	public class PR_SalesRepBackendController : BaseTableController<PR_SalesRepBackend, PR_SalesRepBackendCollection> { }
	public class PR_SigningBonusAmountController : BaseTableController<PR_SigningBonusAmount, PR_SigningBonusAmountCollection> { }
	public class PR_TechBackendController : BaseTableController<PR_TechBackend, PR_TechBackendCollection> { }
	public class PR_TechRecruitingBonuseController : BaseTableController<PR_TechRecruitingBonuse, PR_TechRecruitingBonuseCollection> { }
	public class PR_TechRecruitingBonusTreeController : BaseTableController<PR_TechRecruitingBonusTree, PR_TechRecruitingBonusTreeCollection> { }
	public class PR_TransactionCategoryController : BaseTableController<PR_TransactionCategory, PR_TransactionCategoryCollection> { }
	public class PR_TransactionCodeController : BaseTableController<PR_TransactionCode, PR_TransactionCodeCollection> { }
	public class PR_TransactionController : BaseTableController<PR_Transaction, PR_TransactionCollection> { }
	public class PR_TransactionTypeController : BaseTableController<PR_TransactionType, PR_TransactionTypeCollection> { }
	public class PR_WeeklyTransactionController : BaseTableController<PR_WeeklyTransaction, PR_WeeklyTransactionCollection> { }
	public class PS_ArticleController : BaseTableController<PS_Article, PS_ArticleCollection> { }
	public class PS_ArticleTypeController : BaseTableController<PS_ArticleType, PS_ArticleTypeCollection> { }
	public class PS_ContentCollectionEditorController : BaseTableController<PS_ContentCollectionEditor, PS_ContentCollectionEditorCollection> { }
	public class PS_ContentCollectionController : BaseTableController<PS_ContentCollection, PS_ContentCollectionCollection> { }
	public class PS_ContentItemController : BaseTableController<PS_ContentItem, PS_ContentItemCollection> { }
	public class PS_ContentPermissionController : BaseTableController<PS_ContentPermission, PS_ContentPermissionCollection> { }
	public class PS_ContentStatusController : BaseTableController<PS_ContentStatus, PS_ContentStatusCollection> { }
	public class PS_ContentTypeController : BaseTableController<PS_ContentType, PS_ContentTypeCollection> { }
	public class PS_CoolStuffController : BaseTableController<PS_CoolStuff, PS_CoolStuffCollection> { }
	public class PS_EventRegistrationController : BaseTableController<PS_EventRegistration, PS_EventRegistrationCollection> { }
	public class PS_PublicationController : BaseTableController<PS_Publication, PS_PublicationCollection> { }
	public class PS_PublishLocationController : BaseTableController<PS_PublishLocation, PS_PublishLocationCollection> { }
	public class PS_PublishLocationTypeController : BaseTableController<PS_PublishLocationType, PS_PublishLocationTypeCollection> { }
	public class PS_QuestionOptionController : BaseTableController<PS_QuestionOption, PS_QuestionOptionCollection> { }
	public class PS_QuestionResponseController : BaseTableController<PS_QuestionResponse, PS_QuestionResponseCollection> { }
	public class PS_QuestionController : BaseTableController<PS_Question, PS_QuestionCollection> { }
	public class PS_QuestionTypeController : BaseTableController<PS_QuestionType, PS_QuestionTypeCollection> { }
	public class PS_UploadedContentController : BaseTableController<PS_UploadedContent, PS_UploadedContentCollection> { }
	public class PS_UploadedContentTypeController : BaseTableController<PS_UploadedContentType, PS_UploadedContentTypeCollection> { }
	public class PS_UserSettingController : BaseTableController<PS_UserSetting, PS_UserSettingCollection> { }
	public class QE_MatchingAnswerController : BaseTableController<QE_MatchingAnswer, QE_MatchingAnswerCollection> { }
	public class QE_PossibleAnswerController : BaseTableController<QE_PossibleAnswer, QE_PossibleAnswerCollection> { }
	public class QE_QuestionResponseController : BaseTableController<QE_QuestionResponse, QE_QuestionResponseCollection> { }
	public class QE_QuestionController : BaseTableController<QE_Question, QE_QuestionCollection> { }
	public class QE_QuizResponseController : BaseTableController<QE_QuizResponse, QE_QuizResponseCollection> { }
	public class QE_QuizController : BaseTableController<QE_Quiz, QE_QuizCollection> { }
	public class RU_AuthenticationTokenController : BaseTableController<RU_AuthenticationToken, RU_AuthenticationTokenCollection> { }
	public class RU_BoardMessageController : BaseTableController<RU_BoardMessage, RU_BoardMessageCollection> { }
	public class RU_CommissionChangeHistoryController : BaseTableController<RU_CommissionChangeHistory, RU_CommissionChangeHistoryCollection> { }
	public class RU_CommissionConfigController : BaseTableController<RU_CommissionConfig, RU_CommissionConfigCollection> { }
	public class RU_CommissionPayoutController : BaseTableController<RU_CommissionPayout, RU_CommissionPayoutCollection> { }
	public class RU_CommissionPOBController : BaseTableController<RU_CommissionPOB, RU_CommissionPOBCollection> { }
	public class RU_CommissionProcessLogController : BaseTableController<RU_CommissionProcessLog, RU_CommissionProcessLogCollection> { }
	public class RU_CommissionRecordController : BaseTableController<RU_CommissionRecord, RU_CommissionRecordCollection> { }
	public class RU_CommissionSeasonController : BaseTableController<RU_CommissionSeason, RU_CommissionSeasonCollection> { }
	public class RU_CommissionSeason_UserTypeController : BaseTableController<RU_CommissionSeason_UserType, RU_CommissionSeason_UserTypeCollection> { }
	public class RU_CommTestController : BaseTableController<RU_CommTest, RU_CommTestCollection> { }
	public class RU_DocStatusController : BaseTableController<RU_DocStatus, RU_DocStatusCollection> { }
	public class RU_HousingInfoController : BaseTableController<RU_HousingInfo, RU_HousingInfoCollection> { }
	public class RU_LevelAdvanceController : BaseTableController<RU_LevelAdvance, RU_LevelAdvanceCollection> { }
	public class RU_LoginAuditController : BaseTableController<RU_LoginAudit, RU_LoginAuditCollection> { }
	public class RU_MessageAttachmentController : BaseTableController<RU_MessageAttachment, RU_MessageAttachmentCollection> { }
	public class RU_MessageFormatController : BaseTableController<RU_MessageFormat, RU_MessageFormatCollection> { }
	public class RU_MessageQueue_MessageAttachment_MapController : BaseTableController<RU_MessageQueue_MessageAttachment_Map, RU_MessageQueue_MessageAttachment_MapCollection> { }
	public class RU_MessageQueue_User_MapController : BaseTableController<RU_MessageQueue_User_Map, RU_MessageQueue_User_MapCollection> { }
	public class RU_MessageQueueController : BaseTableController<RU_MessageQueue, RU_MessageQueueCollection> { }
	public class RU_MigrationController : BaseTableController<RU_Migration, RU_MigrationCollection> { }
	public class RU_PayscaleController : BaseTableController<RU_Payscale, RU_PayscaleCollection> { }
	public class RU_PhoneCellCarrierController : BaseTableController<RU_PhoneCellCarrier, RU_PhoneCellCarrierCollection> { }
	public class RU_PhoneTypeController : BaseTableController<RU_PhoneType, RU_PhoneTypeCollection> { }
	public class RU_PriorCompanyController : BaseTableController<RU_PriorCompany, RU_PriorCompanyCollection> { }
	public class RU_PriorCompanySaleController : BaseTableController<RU_PriorCompanySale, RU_PriorCompanySaleCollection> { }
	public class RU_RecruitAddressController : BaseTableController<RU_RecruitAddress, RU_RecruitAddressCollection> { }
	public class RU_RecruitCohabbitTypeController : BaseTableController<RU_RecruitCohabbitType, RU_RecruitCohabbitTypeCollection> { }
	public class RU_RecruitGoalController : BaseTableController<RU_RecruitGoal, RU_RecruitGoalCollection> { }
	public class RU_RecruitPolicyAndProcedureController : BaseTableController<RU_RecruitPolicyAndProcedure, RU_RecruitPolicyAndProcedureCollection> { }
	public class RU_RecruitRegistrationController : BaseTableController<RU_RecruitRegistration, RU_RecruitRegistrationCollection> { }
	public class RU_RecruitController : BaseTableController<RU_Recruit, RU_RecruitCollection> { }
	public class RU_RecruitSeasonGoalController : BaseTableController<RU_RecruitSeasonGoal, RU_RecruitSeasonGoalCollection> { }
	public class RU_RecruitsHistoryController : BaseTableController<RU_RecruitsHistory, RU_RecruitsHistoryCollection> { }
	public class RU_RoleLocationController : BaseTableController<RU_RoleLocation, RU_RoleLocationCollection> { }
	public class RU_RollCallRecordController : BaseTableController<RU_RollCallRecord, RU_RollCallRecordCollection> { }
	public class RU_RollCallController : BaseTableController<RU_RollCall, RU_RollCallCollection> { }
	public class RU_SchoolController : BaseTableController<RU_School, RU_SchoolCollection> { }
	public class RU_SeasonController : BaseTableController<RU_Season, RU_SeasonCollection> { }
	public class RU_SeasonSummerController : BaseTableController<RU_SeasonSummer, RU_SeasonSummerCollection> { }
	public class RU_SeasonSummerSeason_MapController : BaseTableController<RU_SeasonSummerSeason_Map, RU_SeasonSummerSeason_MapCollection> { }
	public class RU_SeasonTeamLocationDefaultController : BaseTableController<RU_SeasonTeamLocationDefault, RU_SeasonTeamLocationDefaultCollection> { }
	public class RU_SiteCodeController : BaseTableController<RU_SiteCode, RU_SiteCodeCollection> { }
	public class RU_TeamLocationRosterController : BaseTableController<RU_TeamLocationRoster, RU_TeamLocationRosterCollection> { }
	public class RU_TeamLocationController : BaseTableController<RU_TeamLocation, RU_TeamLocationCollection> { }
	public class RU_TeamLocationsAndUserController : BaseTableController<RU_TeamLocationsAndUser, RU_TeamLocationsAndUserCollection> { }
	public class RU_TeamLocationStateMappingController : BaseTableController<RU_TeamLocationStateMapping, RU_TeamLocationStateMappingCollection> { }
	public class RU_TeamController : BaseTableController<RU_Team, RU_TeamCollection> { }
	public class RU_TerminationCategoryController : BaseTableController<RU_TerminationCategory, RU_TerminationCategoryCollection> { }
	public class RU_TerminationNoteController : BaseTableController<RU_TerminationNote, RU_TerminationNoteCollection> { }
	public class RU_TerminationReasonController : BaseTableController<RU_TerminationReason, RU_TerminationReasonCollection> { }
	public class RU_TerminationController : BaseTableController<RU_Termination, RU_TerminationCollection> { }
	public class RU_TerminationStatusCodeController : BaseTableController<RU_TerminationStatusCode, RU_TerminationStatusCodeCollection> { }
	public class RU_TerminationStatusController : BaseTableController<RU_TerminationStatus, RU_TerminationStatusCollection> { }
	public class RU_TerminationTypeController : BaseTableController<RU_TerminationType, RU_TerminationTypeCollection> { }
	public class RU_UserAuthenticationController : BaseTableController<RU_UserAuthentication, RU_UserAuthenticationCollection> { }
	public class RU_UserEmployeeTypeController : BaseTableController<RU_UserEmployeeType, RU_UserEmployeeTypeCollection> { }
	public class RU_UserPhotoController : BaseTableController<RU_UserPhoto, RU_UserPhotoCollection> { }
	public class RU_UserController : BaseTableController<RU_User, RU_UserCollection> { }
	public class RU_UsersHistoryController : BaseTableController<RU_UsersHistory, RU_UsersHistoryCollection> { }
	public class RU_UserTypeController : BaseTableController<RU_UserType, RU_UserTypeCollection> { }
	public class RU_UserTypeTeamTypeController : BaseTableController<RU_UserTypeTeamType, RU_UserTypeTeamTypeCollection> { }
	public class SAE_AccountInformationController : BaseTableController<SAE_AccountInformation, SAE_AccountInformationCollection> { }
	public class SAE_AccountsInstalledController : BaseTableController<SAE_AccountsInstalled, SAE_AccountsInstalledCollection> { }
	public class SAE_DateController : BaseTableController<SAE_Date, SAE_DateCollection> { }
	public class SAE_GPRM00103Controller : BaseTableController<SAE_GPRM00103, SAE_GPRM00103Collection> { }
	public class SAE_IndividualSalesRecordController : BaseTableController<SAE_IndividualSalesRecord, SAE_IndividualSalesRecordCollection> { }
	public class SAE_MaxCreditController : BaseTableController<SAE_MaxCredit, SAE_MaxCreditCollection> { }
	public class SAE_OfficeSalesRecordController : BaseTableController<SAE_OfficeSalesRecord, SAE_OfficeSalesRecordCollection> { }
	public class SAE_OfficeUpdateController : BaseTableController<SAE_OfficeUpdate, SAE_OfficeUpdateCollection> { }
	public class SAE_OfficeUpdateNotController : BaseTableController<SAE_OfficeUpdateNot, SAE_OfficeUpdateNotCollection> { }
	public class SAE_RecruitingStructureController : BaseTableController<SAE_RecruitingStructure, SAE_RecruitingStructureCollection> { }
	public class SAE_RecruitTeamMappingController : BaseTableController<SAE_RecruitTeamMapping, SAE_RecruitTeamMappingCollection> { }
	public class SAE_RemoveMeController : BaseTableController<SAE_RemoveMe, SAE_RemoveMeCollection> { }
	public class SAE_RepSalesTotalsSnapShotController : BaseTableController<SAE_RepSalesTotalsSnapShot, SAE_RepSalesTotalsSnapShotCollection> { }
	public class SAE_TeamRecruitSnapShotController : BaseTableController<SAE_TeamRecruitSnapShot, SAE_TeamRecruitSnapShotCollection> { }
	public class SAE_TechInspectionPercentageController : BaseTableController<SAE_TechInspectionPercentage, SAE_TechInspectionPercentageCollection> { }
	public class SAE_TechInspectionScoreController : BaseTableController<SAE_TechInspectionScore, SAE_TechInspectionScoreCollection> { }
	public class SAE_ValidSaleController : BaseTableController<SAE_ValidSale, SAE_ValidSaleCollection> { }
	public class SOP30200Controller : BaseTableController<SOP30200, SOP30200Collection> { }
	public class SY_RecruitSurveyController : BaseTableController<SY_RecruitSurvey, SY_RecruitSurveyCollection> { }
	public class TempViewController : BaseTableController<TempView, TempViewCollection> { }

	#endregion //Controllers

	#region View Controllers

	public class AllRegionalsViewController : BaseViewController<AllRegionalsView, AllRegionalsViewCollection> { }
	public class AllSalesManagersViewController : BaseViewController<AllSalesManagersView, AllSalesManagersViewCollection> { }
	public class RecruitingLineViewController : BaseViewController<RecruitingLineView, RecruitingLineViewCollection> { }
	public class RecruitingLineMobileViewController : BaseViewController<RecruitingLineMobileView, RecruitingLineMobileViewCollection> { }
	public class RecruitingStructureViewController : BaseViewController<RecruitingStructureView, RecruitingStructureViewCollection> { }
	public class RecruitPayrollStatusViewController : BaseViewController<RecruitPayrollStatusView, RecruitPayrollStatusViewCollection> { }
	public class RecruitUserViewController : BaseViewController<RecruitUserView, RecruitUserViewCollection> { }
	public class TeamsViewController : BaseViewController<TeamsView, TeamsViewCollection> { }
	public class TeamSalesManagersViewController : BaseViewController<TeamSalesManagersView, TeamSalesManagersViewCollection> { }
	public class RU_RecruitUserViewController : BaseViewController<RU_RecruitUserView, RU_RecruitUserViewCollection> { }
	public class RU_RollCallRecordsLatestByRecruitViewController : BaseViewController<RU_RollCallRecordsLatestByRecruitView, RU_RollCallRecordsLatestByRecruitViewCollection> { }
	public class RU_SalesRepsViewController : BaseViewController<RU_SalesRepsView, RU_SalesRepsViewCollection> { }
	public class RU_TeamLocationViewController : BaseViewController<RU_TeamLocationView, RU_TeamLocationViewCollection> { }
	public class RU_TeamLocationRosterByWeekViewController : BaseViewController<RU_TeamLocationRosterByWeekView, RU_TeamLocationRosterByWeekViewCollection> { }
	public class RU_TeamLocationRosterTransfersViewController : BaseViewController<RU_TeamLocationRosterTransfersView, RU_TeamLocationRosterTransfersViewCollection> { }
	public class RU_TeamLocatonRosterCurrentByRecruitViewController : BaseViewController<RU_TeamLocatonRosterCurrentByRecruitView, RU_TeamLocatonRosterCurrentByRecruitViewCollection> { }
	public class RU_TechniciansViewController : BaseViewController<RU_TechniciansView, RU_TechniciansViewCollection> { }
	public class RU_TerminationStatusCurrentStatusViewController : BaseViewController<RU_TerminationStatusCurrentStatusView, RU_TerminationStatusCurrentStatusViewCollection> { }
	public class RU_TerminationsWithStatusViewController : BaseViewController<RU_TerminationsWithStatusView, RU_TerminationsWithStatusViewCollection> { }
	public class RU_UserSalesInfoConnextViewController : BaseViewController<RU_UserSalesInfoConnextView, RU_UserSalesInfoConnextViewCollection> { }
	public class RU_UsersCallerIDViewController : BaseViewController<RU_UsersCallerIDView, RU_UsersCallerIDViewCollection> { }
	public class RU_UsersTechViewController : BaseViewController<RU_UsersTechView, RU_UsersTechViewCollection> { }
	public class TechniciansViewController : BaseViewController<TechniciansView, TechniciansViewCollection> { }

	#endregion //View Controllers
}
