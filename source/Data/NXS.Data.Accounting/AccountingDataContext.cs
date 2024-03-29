﻿using NXS.Data.Accounting.Controllers;

namespace NXS.Data.Accounting
{
	public partial class AccountingDataContext
	{
		#region Controllers Properties

		WorkersCompController _workersComps;
		public WorkersCompController WorkersComps
		{
			get
			{
				if (_workersComps == null) _workersComps = new WorkersCompController();
				return _workersComps;
			}
		}

		FilingStatusController _filingStatuses;
		public FilingStatusController FilingStatuses
		{
			get
			{
				if (_filingStatuses == null) _filingStatuses = new FilingStatusController();
				return _filingStatuses;
			}
		}

		TaxCodeController _taxCodes;
		public TaxCodeController TaxCodes
		{
			get
			{
				if (_taxCodes == null) _taxCodes = new TaxCodeController();
				return _taxCodes;
			}
		}

		#endregion //Controllers Properties
	}
}
