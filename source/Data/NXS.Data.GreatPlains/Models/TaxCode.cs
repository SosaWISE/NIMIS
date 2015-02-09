using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NXS.Data.GreatPlains
{
	[DataContract]
	public class TaxCode
	{
		[DataMember]
		public string ClassID { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public string Code { get; set; }
		[DataMember]
		public decimal Rate { get; set; }
	}
}