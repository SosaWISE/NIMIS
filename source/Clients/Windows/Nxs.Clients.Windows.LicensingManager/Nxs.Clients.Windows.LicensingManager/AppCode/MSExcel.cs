using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Nxs.Clients.Windows.LicensingManager
{
    public class MSExcel
    {
        Type ExcelType;
	    readonly object _excelApplication;
        public object oBook;

        public MSExcel()
        {
            ExcelType = Type.GetTypeFromProgID("Excel.Application");
            _excelApplication = Activator.CreateInstance(ExcelType);
        }

        public void Open(string szFileName)
        {
            object FileName = szFileName;
            object ReadOnly = true;
            object missing = System.Reflection.Missing.Value;
            var oParams = new object[1];

            object oDocs = _excelApplication.GetType().InvokeMember("Workbooks", System.Reflection.BindingFlags.GetProperty, null, _excelApplication, null, CultureInfo.InvariantCulture);
            oParams = new object[3];
            oParams[0] = FileName;
            oParams[1] = missing;
            oParams[2] = ReadOnly;

            //open first worksheet
            oBook = oDocs.GetType().InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, oDocs, oParams, CultureInfo.InvariantCulture);
        }

        public void Close()
        {
            oBook.GetType().InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, oBook, null, CultureInfo.InvariantCulture);
        }

        public void Print()
        {
            oBook.GetType().InvokeMember("PrintOut", System.Reflection.BindingFlags.InvokeMethod, null, oBook, null, CultureInfo.InvariantCulture);
        }

        public void Quit()
        {
            _excelApplication.GetType().InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, _excelApplication, null, CultureInfo.InvariantCulture);
        }
    }
}
