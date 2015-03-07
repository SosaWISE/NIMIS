using System.Xml.Serialization;

namespace NSE.Lib.Hart.Models
{
	public class HartXml
	{
		#region Properties
		#region Privates
		#endregion Privates

		#region Public
		[XmlAttributeAttribute()]
		public string Version { get; set; }

		[XmlElement("HX5_transaction_information")]
		public TransactionInformation Hx5TransactionInformation { get; set; }
		#endregion Public
		#endregion Properties
	}
}
