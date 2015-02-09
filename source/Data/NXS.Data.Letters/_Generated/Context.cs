


using System;
using SubSonic;
using SOS.Data;

namespace NXS.Data.Letters
{
	public partial class LettersDataContext
	{
		#region Internal Instance
		
		private static LettersDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();
		
		public static LettersDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new LettersDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}
		
		#endregion // Internal Instance
		
		#region Controllers Properties

		LD_CreditRequestReasonController _LD_CreditRequestReasons;
		public LD_CreditRequestReasonController LD_CreditRequestReasons
		{
			get
			{
				if (_LD_CreditRequestReasons == null) _LD_CreditRequestReasons = new LD_CreditRequestReasonController();
				return _LD_CreditRequestReasons;
			}
		}

		LD_DepartmentController _LD_Departments;
		public LD_DepartmentController LD_Departments
		{
			get
			{
				if (_LD_Departments == null) _LD_Departments = new LD_DepartmentController();
				return _LD_Departments;
			}
		}

		LD_DocTypeController _LD_DocTypes;
		public LD_DocTypeController LD_DocTypes
		{
			get
			{
				if (_LD_DocTypes == null) _LD_DocTypes = new LD_DocTypeController();
				return _LD_DocTypes;
			}
		}

		LD_ExtraInfoController _LD_ExtraInfos;
		public LD_ExtraInfoController LD_ExtraInfos
		{
			get
			{
				if (_LD_ExtraInfos == null) _LD_ExtraInfos = new LD_ExtraInfoController();
				return _LD_ExtraInfos;
			}
		}

		LD_FieldController _LD_Fields;
		public LD_FieldController LD_Fields
		{
			get
			{
				if (_LD_Fields == null) _LD_Fields = new LD_FieldController();
				return _LD_Fields;
			}
		}

		LD_InsertController _LD_Inserts;
		public LD_InsertController LD_Inserts
		{
			get
			{
				if (_LD_Inserts == null) _LD_Inserts = new LD_InsertController();
				return _LD_Inserts;
			}
		}

		LD_LetterController _LD_Letters;
		public LD_LetterController LD_Letters
		{
			get
			{
				if (_LD_Letters == null) _LD_Letters = new LD_LetterController();
				return _LD_Letters;
			}
		}

		LD_LettersToPrintController _LD_LettersToPrints;
		public LD_LettersToPrintController LD_LettersToPrints
		{
			get
			{
				if (_LD_LettersToPrints == null) _LD_LettersToPrints = new LD_LettersToPrintController();
				return _LD_LettersToPrints;
			}
		}

		LD_PDFFieldController _LD_PDFFields;
		public LD_PDFFieldController LD_PDFFields
		{
			get
			{
				if (_LD_PDFFields == null) _LD_PDFFields = new LD_PDFFieldController();
				return _LD_PDFFields;
			}
		}

		LD_PDFFieldTypeController _LD_PDFFieldTypes;
		public LD_PDFFieldTypeController LD_PDFFieldTypes
		{
			get
			{
				if (_LD_PDFFieldTypes == null) _LD_PDFFieldTypes = new LD_PDFFieldTypeController();
				return _LD_PDFFieldTypes;
			}
		}

		LD_PDFReadOnlyController _LD_PDFReadOnlies;
		public LD_PDFReadOnlyController LD_PDFReadOnlies
		{
			get
			{
				if (_LD_PDFReadOnlies == null) _LD_PDFReadOnlies = new LD_PDFReadOnlyController();
				return _LD_PDFReadOnlies;
			}
		}

		LD_PDFTemplateController _LD_PDFTemplates;
		public LD_PDFTemplateController LD_PDFTemplates
		{
			get
			{
				if (_LD_PDFTemplates == null) _LD_PDFTemplates = new LD_PDFTemplateController();
				return _LD_PDFTemplates;
			}
		}

		LD_PriorityController _LD_Priorities;
		public LD_PriorityController LD_Priorities
		{
			get
			{
				if (_LD_Priorities == null) _LD_Priorities = new LD_PriorityController();
				return _LD_Priorities;
			}
		}

		LD_StandardInsertController _LD_StandardInserts;
		public LD_StandardInsertController LD_StandardInserts
		{
			get
			{
				if (_LD_StandardInserts == null) _LD_StandardInserts = new LD_StandardInsertController();
				return _LD_StandardInserts;
			}
		}

		LD_TemplateController _LD_Templates;
		public LD_TemplateController LD_Templates
		{
			get
			{
				if (_LD_Templates == null) _LD_Templates = new LD_TemplateController();
				return _LD_Templates;
			}
		}

		#endregion //Controllers Properties
		
		#region View Controllers Properties

		#endregion //View Controllers Properties
	}
	
	#region Controllers
	
	public class LD_CreditRequestReasonController : BaseTableController<LD_CreditRequestReason, LD_CreditRequestReasonCollection> { }
	public class LD_DepartmentController : BaseTableController<LD_Department, LD_DepartmentCollection> { }
	public class LD_DocTypeController : BaseTableController<LD_DocType, LD_DocTypeCollection> { }
	public class LD_ExtraInfoController : BaseTableController<LD_ExtraInfo, LD_ExtraInfoCollection> { }
	public class LD_FieldController : BaseTableController<LD_Field, LD_FieldCollection> { }
	public class LD_InsertController : BaseTableController<LD_Insert, LD_InsertCollection> { }
	public class LD_LetterController : BaseTableController<LD_Letter, LD_LetterCollection> { }
	public class LD_LettersToPrintController : BaseTableController<LD_LettersToPrint, LD_LettersToPrintCollection> { }
	public class LD_PDFFieldController : BaseTableController<LD_PDFField, LD_PDFFieldCollection> { }
	public class LD_PDFFieldTypeController : BaseTableController<LD_PDFFieldType, LD_PDFFieldTypeCollection> { }
	public class LD_PDFReadOnlyController : BaseTableController<LD_PDFReadOnly, LD_PDFReadOnlyCollection> { }
	public class LD_PDFTemplateController : BaseTableController<LD_PDFTemplate, LD_PDFTemplateCollection> { }
	public class LD_PriorityController : BaseTableController<LD_Priority, LD_PriorityCollection> { }
	public class LD_StandardInsertController : BaseTableController<LD_StandardInsert, LD_StandardInsertCollection> { }
	public class LD_TemplateController : BaseTableController<LD_Template, LD_TemplateCollection> { }

	#endregion //Controllers
	
	#region View Controllers
	

	#endregion //View Controllers
}
