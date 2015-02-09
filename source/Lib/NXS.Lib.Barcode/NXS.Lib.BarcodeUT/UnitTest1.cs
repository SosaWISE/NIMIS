using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NXS.Lib.Barcode;
using org.pdfclown.files;

namespace NXS.Lib.BarcodeUT
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void GenerateAMABarcode()
		{
			/** INititialize.  */
			//const string FONT_PATH = "C:\\CodeBaseNXS\\Trunk\\Samples\\PDFClown\\main\\res\\samples\\input\\fonts";
			const string FONT_PATH = ".\\Res\\fonts";
			const string TEMPLATE_PATH = ".\\Res\\templates\\Nxs_GEN_AMA_F_080514_dv.pdf";
			const string OUTPUT_PATH = ".\\Output\\";
			const string BARCODE_PRE = "1GEN-{0}-GR3-{1}";

			const string BARCODE_NUMBER = "000000001";
			var amaBarcodeNumber = string.Format(BARCODE_PRE, "AMA", BARCODE_NUMBER);
			var nocBarcodeNumber = string.Format(BARCODE_PRE, "NOC", BARCODE_NUMBER);

			try
			{
				//var barcode3Of9 = new IDAutomationHC39M(FONT_PATH);
				var barcode3Of9 = new IDAutomationHC39M(FONT_PATH, TEMPLATE_PATH, string.Format("{0}{1}.pdf", OUTPUT_PATH, amaBarcodeNumber));

				Assert.IsNotNull(barcode3Of9, string.Format("Barcode returned null"));

				var barcodeGenerated = barcode3Of9.CreateAMABarcode(amaBarcodeNumber, nocBarcodeNumber, SerializationModeEnum.Standard);

				Assert.IsTrue(barcodeGenerated, string.Format("Barcode should not be null."));

			}
			catch (Exception ex)
			{
				Debug.WriteLine("The following error was thrown: {0}", ex.Message);
			}
		}
	}
}
