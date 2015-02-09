using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NXS.Data.GreatPlains.Models
{
	[DataContract]
	public class FilingStatus
	{
		[DataMember]
		public string ID { get; set; }
		[DataMember]
		public string Description { get; set; }
	}
}
