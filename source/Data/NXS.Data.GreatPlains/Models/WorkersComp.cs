using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NXS.Data.GreatPlains.Models
{
	[DataContract]
	public class WorkersComp
	{
		[DataMember]
		public string WrkrComp { get; set; }
		[DataMember]
		public string Dscriptn { get; set; }
	}
}
