namespace NSE.Lib.Hart.Models
{
	public class TransactionInformation
	{
		#region Properties
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Transid { get; set; }

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Token { get; set; }
		#endregion Properties
	}
}
