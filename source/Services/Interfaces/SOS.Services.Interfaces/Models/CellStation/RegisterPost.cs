using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SOS.Services.Interfaces.Models.CellStation
{
	public class RegisterPost
	{
		public string SerialNumber { get; set; }
		public bool EnableTwoWay { get; set; }
	}
}
