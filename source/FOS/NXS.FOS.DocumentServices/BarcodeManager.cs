using System;
using System.Data.SqlClient;
using NXS.Lib.Barcode;
using org.pdfclown.files;
using SOS.Data.Logging;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util;

namespace NXS.FOS.DocumentServices
{
	public class BarcodeManager
	{
		#region .ctor

		public BarcodeManager(string inputFilePath, string fileOutputFolderPath, DBErrorManager errorManager)
		{
			_errorManager = errorManager;
			InputFilePath = inputFilePath;
			OutputFolderPath = string.IsNullOrEmpty(fileOutputFolderPath)
				? SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("NXS.Lib.Barcode.IDAutomationHC39M_OutFolderPath")
				: fileOutputFolderPath;
		}

		#endregion .ctor

		#region Properties

		public ErrorMessageType MessageType { get; private set; }
		public string InputFilePath { get; private set; }
		public string OutputFolderPath { get; private set; }
		public string OutputFilePath { get; private set; }
		private readonly DBErrorManager _errorManager;

		#endregion Properties

		#region Methods

		#endregion Methods

		public bool AssigneBarcodesToAMATry(string amaBarcodeTypeID, long accountId, out string message)
		{
			// ** Initialize. 
			const string METHOD_NAME = "AssigneBarcodesToAMATry";
			OutputFilePath = string.Format("{0}\\{1}-{2}-{3:yyyyMMddmmss}.pdf", OutputFolderPath, accountId, amaBarcodeTypeID, DateTime.UtcNow);
			var result = false;

			try
			{
				// ** Execute to get Barcode Numbers.
				var barcodeNumbers =
					SOS.Data.SosCrm.SosCrmDataContext.Instance.BX_BarcodeTypesAMAAndNOCViews.GeneGenerateBarcodeNumbersAMA(
						amaBarcodeTypeID, accountId);

				if (barcodeNumbers == null)
				{
					message = string.Format("There were no barcode numbers generated for BarcodeTypeId: {0}, and AccountID: {1}",
						amaBarcodeTypeID, accountId);
					return false;
				}

				// ** Call barcode library to bind to AMA.
				var barcode3Of9 = new IDAutomationHC39M(null, InputFilePath, OutputFilePath);
				if (!barcode3Of9.CreateAMABarcode(barcodeNumbers.AMABarcodeNumber, barcodeNumbers.NOCBarcodeNumber,
					SerializationModeEnum.Incremental))
				{
					message = string.Format("Error occurred generating a barcode on PDF.");
					return false;
				}

				result = true;
				message = string.Format("Success");
			}
			catch (SqlException sqlEx)
			{
				message = string.Format("SQL Exception was thrown.");
				MessageType = ErrorMessageType.Critical;
				var sqlUtil = MsSqlExceptionUtil.Parse(sqlEx.Message, sqlEx);
				_errorManager.AddSqlExceptionMessage(sqlEx, "Assigning Barcodes",
					string.Format("SQL Exception thrown at {0}: {1}", METHOD_NAME, sqlUtil.ErrorMessage));
			}
			catch (Exception ex)
			{
				message = string.Format("The following exception was thrown: {0}", ex.Message);
				MessageType = ErrorMessageType.Critical;
			}

			return result;
		}
	}
}
