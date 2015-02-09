using System;
using System.Drawing;
using System.IO;
using org.pdfclown.documents;
using org.pdfclown.documents.contents.composition;
using org.pdfclown.documents.contents.xObjects;
using org.pdfclown.tools;
// ReSharper disable once RedundantUsingDirective
using fonts = org.pdfclown.documents.contents.fonts;
using files = org.pdfclown.files;

namespace NXS.Lib.Barcode
{
    public class IDAutomationHC39M : BaseBarcode
	{
		#region .ctor

		public IDAutomationHC39M(string fontFontResourcePath, string templateFilePath, string outputFilePath)
			: base(outputFilePath)
		{
			if (!string.IsNullOrEmpty(fontFontResourcePath))
				FontResourcePath = fontFontResourcePath;
			_templateFilePath = templateFilePath;
		}

	    #endregion .ctor

		#region Properties

	    private const string _CHECK_SUM = "*{0}*";
	    private readonly string _templateFilePath;

		private const string _ID_AUTOMATION_HC39_M_FONT_FILE = "IDAutomationHC39M.ttf";

		#endregion Properties

		#region Methods


		private FormXObject CreateBarcode(string barcodeStr, Document document, fonts.Font font, double fontSize, PointF pointF)
	    {
			/** Initialize. */
		    SizeF size = document.GetSize();

			// 1. Create an external form object to represent the barcode!
		    var barcode = new FormXObject(document, size);

			// 2. Inserting the contencts of the barcode...
			// 2.1. Create a content composer!
			var composer = new PrimitiveComposer(barcode);
			// 2.2. Inserting the contents...
			// Set the font to use!
//			var font = fonts::Font.Get(document, Path.Combine(_fontResourcePath, _ID_AUTOMATION_HC39_M_FONT_FILE));
			composer.SetFont(font, fontSize);
			// Show the text
		    composer.ShowText(
				string.Format(_CHECK_SUM, barcodeStr),
//				new PointF((size.Width / 2f) + 20, 10), // Anchor location: page center.
				pointF,
				XAlignmentEnum.Center,
				YAlignmentEnum.Top,
				0 // Rotation
			    );
			// 2.3. Flush the contents into the barcode!
			composer.Flush();

			// ** Returen result
		    return barcode;
	    }

		private void ApplyAMABarcodeToDoc(FormXObject amaBarcode)
	    {
			// 1. Instantiate the stamper!
			/* NOTE: The PageStamper is optimized for dealing with pages. */
			var stamper = new PageStamper();

			// Validate
			if (amaBarcode.Document.Pages.Count < 2)
				throw new Exception("AMA PDF should have at least two pages.");

			// 2. Inserting the amaBarcode into first page of AMA.
			// 2.1. Associate the page to the stamper!
			stamper.Page = amaBarcode.Document.Pages[0];
			// 2.2. Stamping the barcode on the foreground...
			// Get the content composer!
			PrimitiveComposer composer = stamper.Foreground;
			// Show the barcode into the page background!
			composer.ShowXObject(amaBarcode);

			// 2.3. End the stamping!
			stamper.Flush();


			//foreach (Page page in amaBarcode.Document.Pages)
			//{
			//	// 2.1. Associate the page to the stamper!
			//	stamper.Page = page;

			//	// 2.2. Stamping the barcode on the foreground...
			//	// Get the content composer!
			//	PrimitiveComposer composer = stamper.Foreground;
			//	// Show the barcode into the page background!
			//	composer.ShowXObject(amaBarcode);

			//	// 2.3. End the stamping!
			//	stamper.Flush();
			//}
	    }

		private void ApplyNOCBarcodeToDoc(FormXObject nocBarcode)
		{
			// 1. Instantiate the stamper!
			/* NOTE: The PageStamper is optimized for dealing with pages. */
			var stamper = new PageStamper();

			// Validate
			if (nocBarcode.Document.Pages.Count < 2)
				throw new Exception("AMA PDF should have at least two pages.");

			// 2. Inserting the amaBarcode into first page of AMA.
			// 2.1. Associate the page to the stamper!
			stamper.Page = nocBarcode.Document.Pages[0];
			// 2.2. Stamping the barcode on the foreground...
			// Get the content composer!
			PrimitiveComposer composer = stamper.Foreground;
			// Show the barcode into the page background!
			composer.ShowXObject(nocBarcode);

			// 2.3. End the stamping!
			stamper.Flush();
		}


		public bool CreateAMABarcode(string amaBarcodeNumber, string nocBarcodeNumber, files.SerializationModeEnum serializationMode)
		{
			// ** Initialize
			var file = new files::File(_templateFilePath);
			Document document = file.Document;
			SizeF size = document.GetSize();

			// ** Attach AMA barcode
			var font = fonts::Font.Get(document, Path.Combine(FontResourcePath, _ID_AUTOMATION_HC39_M_FONT_FILE));
			var pointF = new PointF((size.Width/2f) + 20, 10); // Anchor location: page center.;
			FormXObject amaBarcode = CreateBarcode(amaBarcodeNumber, document, font, 10, pointF);

			// ** Apply barcode to AMA
			ApplyAMABarcodeToDoc(amaBarcode);

			// ** Attach NOC barcode
			font = fonts::Font.Get(document, Path.Combine(FontResourcePath, _ID_AUTOMATION_HC39_M_FONT_FILE));
			pointF = new PointF((size.Width / 2f) + 170, 995); // Anchor location: page center.;
			FormXObject nocBarcode = CreateBarcode(nocBarcodeNumber, document, font, 10, pointF);

			// ** Apply barcode to AMA
			ApplyNOCBarcodeToDoc(nocBarcode);

			// ** Save file to FILE folder.  If returns null then there was an error.
			return !string.IsNullOrEmpty(Serialize(file, serializationMode));
		}

	    #endregion Methods
	}
}
