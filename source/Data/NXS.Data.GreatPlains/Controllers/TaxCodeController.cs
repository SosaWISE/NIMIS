using System;
using System.Collections.Generic;
using SOS.Data;
using SOS.Lib.Util;
using AR = NXS.Data.GreatPlains.TaxCode;

namespace NXS.Data.GreatPlains.Controllers
{
	public class TaxCodeController : BaseModelController<AR>
	{
		private static Dictionary<string, TaxCode> _taxCodes;
		private static readonly object _syncRootTaxCodes = new object();

		private static Dictionary<string, TaxCode> TaxCodes
		{
			get
			{
				if (_taxCodes == null)
				{
					lock (_syncRootTaxCodes)
					{
						if (_taxCodes == null)
						{
							_taxCodes = new Dictionary<string, TaxCode>(StringComparer.InvariantCultureIgnoreCase);
							var controller = new TaxCodeController();
							foreach (TaxCode curr in controller.LoadCollectionByProcedure(GreatPlainsStoredProcedureManager.GetTaxCodes()))
							{
								if (!_taxCodes.ContainsKey(curr.Description))
								{
									_taxCodes.Add(curr.Description, curr);
								}
							}
						}
					}
				}
				return _taxCodes;
			}
		}

		public AR GetTaxCode(string stateAbbreviation, string countyName, string cityName)
		{
			string key = string.Format("{0}:{1}:{2}", stateAbbreviation, countyName, cityName);
			if (TaxCodes.ContainsKey(key))
				return TaxCodes[key];

			key = string.Format("{0}:{1}", stateAbbreviation, countyName);
			if (TaxCodes.ContainsKey(key))
				return TaxCodes[key];

			key = stateAbbreviation;
			if (TaxCodes.ContainsKey(key))
				return TaxCodes[key];

			return null;
		}

		public AR GetTaxCode(string code)
		{
			foreach (TaxCode currCode in TaxCodes.Values)
			{
				if (StringUtility.AreEqual(currCode.Code, code, false))
				{
					return currCode;
				}
			}
			return null;
		}
	}
}