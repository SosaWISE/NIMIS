using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.FOS.DocumentServices;
using SOS.Data.Logging;
using SOS.Lib.Core;

namespace NXS.FOS.DocumentServicesUT
{
	[TestClass]
	public class DocumentManagerUT
	{
		private static DBErrorManager _errorManager;

		#region Properties

		private const string _PDF_AMA_INPUT_PATH = ".\\Res\\templates\\{0}.pdf";
		private const string _PDF_AMA_OUTPUT_PATH = ".\\Output\\{0}_{1}_{2}.pdf";

		#endregion Properties

		[ClassInitialize]
		public static void MyClassInitialize(TestContext testContext)
		{

			Helpers.InitializeAndConfigure.Instance.Initialize();
//			_service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

			/** Initialize Error Manager. */
			_errorManager = new DBErrorManager(LogSource.SosClientsMVCCorpSite2, null);
			_errorManager.MessageAdded += OnErrorManagerMessageAdded;
			DBErrorManager.SetSingletonInstance(_errorManager);
		}

		private static void OnErrorManagerMessageAdded()
		{
			Debug.WriteLine("There was an error thrown.");
		}

		[TestMethod]
		public void BindArrayToDict()
		{
			// ** Initialize
			var keyPairs = new Dictionary<string, object>();
			var arrayOfChars = "411111111111119".ToCharArray();

			// ** Execute
			try
			{
				DocumentManager.BindArrayToDict(keyPairs, arrayOfChars, "CC_Number", 20);

				Assert.IsTrue(keyPairs.Count == 20, "Sorry but the key is wrong");
				Assert.IsTrue(keyPairs.ContainsKey("CC_Number0"), "Key is missing");

				Assert.IsTrue(keyPairs.ContainsKey(string.Format("CC_Number{0}", arrayOfChars.Length - 1)), "Last Key is not present");

				object value;
				if (keyPairs.TryGetValue(string.Format("CC_Number{0}", arrayOfChars.Length - 1), out value))
					Assert.IsTrue(value.ToString().Equals("9"), "Wrong value in the last position.");
			}
			catch (Exception ex)
			{
				Assert.IsFalse(ex == null, string.Format("Following exception was thrown: '{0}'", ex.Message));
			}

		}

		[TestMethod]
		public void BindArrayToDictRightJustified()
		{
			// ** Initialize
			var keyPairs = new Dictionary<string, object>();
			var arrayOfChars = "411111111111119".ToCharArray();
			const int SLOT_SIZE = 20;

			// ** Execute
			try
			{
				DocumentManager.BindArrayToDict(keyPairs, arrayOfChars, "CC_Number", SLOT_SIZE, "", "RIGHT");

				Assert.IsTrue(keyPairs.ContainsKey("CC_Number0"), "Key is missing");

				Assert.IsTrue(keyPairs.ContainsKey(string.Format("CC_Number{0}", arrayOfChars.Length - 1)), "Last Key is not present");

				object value;
				if (keyPairs.TryGetValue(string.Format("CC_Number{0}", SLOT_SIZE - 1), out value))
					Assert.IsTrue(value.ToString().Equals("9"), "Wrong value in the last position.");
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex == null, string.Format("Following exception was thrown: '{0}'", ex.Message));
			}

		}

		[TestMethod]
		public void InitDocument()
		{
			// ** Initialize.
			var docManager = new DocumentManager();

			string message;
			Assert.IsTrue(docManager.InitDocumentTry("AMNXS001", 130532, out message), string.Format("The following message was returned: '{0}'", message));

			// ** Check Premise AreaCode
			object value;
			if(docManager.KeyValues.TryGetValue("PremiseAreaCode", out value))
				Assert.IsTrue(value.ToString().Equals("801"));
		}

		[TestMethod]
		public void PDFDocument()
		{
			// ** Initialize. 
			const long ACCOUNT_ID = 130532;
			const long CUSTOMER_NUMBER = 9876543;
			const string TEMPLATE_NAME = "Nxs_GEN_AMA_F_080514_dv_FORM";
			const string BARCODE_TYPE_ID = "AMNXS001";
			var docManager = new DocumentManager();

			string message;
			if (!docManager.InitDocumentTry(BARCODE_TYPE_ID, ACCOUNT_ID, out message))
				Assert.IsTrue(false, string.Format("The following message was returned: '{0}'", message));

			// ** Bind KeyValues to PDF
			string pdfInputPath = string.Format(_PDF_AMA_INPUT_PATH, TEMPLATE_NAME);
			string pdfOutputPath = string.Format(_PDF_AMA_OUTPUT_PATH, TEMPLATE_NAME, CUSTOMER_NUMBER, ACCOUNT_ID);
			var pdfManager = new PDFManager(docManager.KeyValues, pdfInputPath, pdfOutputPath);

			if(!pdfManager.TryBindData(out message))
				Assert.IsTrue(false, string.Format("The following message was returned: {0}", message));

			// ** catch execution
				Assert.IsNotNull(pdfManager.OutputFilePath, "Error occurred");
		}

        [TestMethod]
        public void PDFSopSONXS001()
	    {
            // ** Initialize. 
            const long ACCOUNT_ID = 130532;
            const long CUSTOMER_NUMBER = 9876543;
            const string TEMPLATE_NAME = "SOP_GEN_NXS_v1m_DV FORM";
            const string BARCODE_TYPE_ID = "SONXS001";
            var docManager = new DocumentManager();

            string message;
            Assert.IsTrue(docManager.InitDocumentTry(BARCODE_TYPE_ID, ACCOUNT_ID, out message), string.Format("The following message was returned: '{0}'"
				, message));

            // ** Bind KeyValues to PDF
            string pdfInputPath = string.Format(_PDF_AMA_INPUT_PATH, TEMPLATE_NAME);
            string pdfOutputPath = string.Format(_PDF_AMA_OUTPUT_PATH, TEMPLATE_NAME, CUSTOMER_NUMBER, ACCOUNT_ID);
            var pdfManager = new PDFManager(docManager.KeyValues, pdfInputPath, pdfOutputPath);

			Assert.IsTrue(pdfManager.TryBindData(out message), string.Format("The following message was returned: {0}", message));

            // ** catch execution
            Assert.IsNotNull(pdfManager.OutputFilePath, "Error occurred");
        }

		[TestMethod]
		public void BarcodeDocumentWrongBarcodeTypeId()
		{
			/** Initialize BarcodeManager */
			const long ACCOUNT_ID = 130532;
			const string BARCODE_TYPE_ID = "SSSSSS";
			string message;

			// ** Assign barcodes to the document
			const string TEMPLATE_FOLDER_PATH = ".\\Res\\templates";
			const string FILE_OUTPUT_FOLDER_PATH = ".\\Output";
			var barcodeManager = new BarcodeManager(TEMPLATE_FOLDER_PATH, FILE_OUTPUT_FOLDER_PATH, _errorManager);

			Assert.IsFalse(barcodeManager.AssigneBarcodesToAMATry(BARCODE_TYPE_ID, ACCOUNT_ID, out message), string.Format("The following error message was returned: {0}", message));
		}

		[TestMethod]
		public void BarcodeDocument()
		{
			// ** Initialize. 
			const long ACCOUNT_ID = 130532;
			const long CUSTOMER_NUMBER = 9876543;
			const string TEMPLATE_NAME = "Nxs_GEN_AMA_F_080514_dv_FORM";
			const string BARCODE_TYPE_ID = "AMNXS001";
			var docManager = new DocumentManager();

			string message;
			if (!docManager.InitDocumentTry(BARCODE_TYPE_ID, ACCOUNT_ID, out message))
				Assert.IsTrue(false, string.Format("The following message was returned: '{0}'", message));

			// ** Bind KeyValues to PDF
			string pdfInputPath = string.Format(_PDF_AMA_INPUT_PATH, TEMPLATE_NAME);
			string pdfOutputPath = string.Format(_PDF_AMA_OUTPUT_PATH, TEMPLATE_NAME, CUSTOMER_NUMBER, ACCOUNT_ID);
			var pdfManager = new PDFManager(docManager.KeyValues, pdfInputPath, pdfOutputPath);

			if (!pdfManager.TryBindData(out message))
				Assert.IsTrue(false, string.Format("The following message was returned: {0}", message));

			// ** catch execution
			Assert.IsNotNull(pdfManager.OutputFilePath, "Error occurred");

			/** Initialize BarcodeManager */
			// ** Assign barcodes to the document
			const string FILE_OUTPUT_FOLDER_PATH = ".\\Output";
			var barcodeManager = new BarcodeManager(pdfOutputPath, FILE_OUTPUT_FOLDER_PATH, _errorManager);

			Assert.IsTrue(barcodeManager.AssigneBarcodesToAMATry(BARCODE_TYPE_ID, ACCOUNT_ID, out message), string.Format("The following error message was returned: {0}", message));
		}

		[TestMethod]
		public void BarcodeDocumentWithEchoSignFields()
		{
			// ** Initialize. 
			const long ACCOUNT_ID = 130532;
			const long CUSTOMER_NUMBER = 9876543;
			const string TEMPLATE_NAME = "Nxs_GEN_AMA_F_080514_dv_FORM_ES";
			const string BARCODE_TYPE_ID = "AMNXS001";
			var docManager = new DocumentManager();

			string message;
			if (!docManager.InitDocumentTry(BARCODE_TYPE_ID, ACCOUNT_ID, out message))
				Assert.IsTrue(false, string.Format("The following message was returned: '{0}'", message));

			// ** Bind KeyValues to PDF
			string pdfInputPath = string.Format(_PDF_AMA_INPUT_PATH, TEMPLATE_NAME);
			string pdfOutputPath = string.Format(_PDF_AMA_OUTPUT_PATH, TEMPLATE_NAME, CUSTOMER_NUMBER, ACCOUNT_ID);
			var pdfManager = new PDFManager(docManager.KeyValues, pdfInputPath, pdfOutputPath);

			if (!pdfManager.TryBindData(out message))
				Assert.IsTrue(false, string.Format("The following message was returned: {0}", message));

			// ** catch execution
			Assert.IsNotNull(pdfManager.OutputFilePath, "Error occurred");

			/** Initialize BarcodeManager */
			// ** Assign barcodes to the document
			const string FILE_OUTPUT_FOLDER_PATH = ".\\Output";
			var barcodeManager = new BarcodeManager(pdfOutputPath, FILE_OUTPUT_FOLDER_PATH, _errorManager);

			Assert.IsTrue(barcodeManager.AssigneBarcodesToAMATry(BARCODE_TYPE_ID, ACCOUNT_ID, out message), string.Format("The following error message was returned: {0}", message));
		}
	}
}
