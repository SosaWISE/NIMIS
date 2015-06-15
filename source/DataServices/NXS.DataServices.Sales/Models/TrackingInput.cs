using NXS.Data.Sales;
using System;

namespace NXS.DataServices.Sales.Models
{
	public class TrackingInput
	{
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }

		internal void ToDb(SL_Tracking item)
		{
			//item.RepCompanyID = this.RepCompanyID;
			item.Latitude = this.Latitude;
			item.Longitude = this.Longitude;
		}
	}
}
