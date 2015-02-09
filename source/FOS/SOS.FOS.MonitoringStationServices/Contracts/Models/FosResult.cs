using System.Runtime.Serialization;
using NSE.FOS.Contracts.Models;

namespace SOS.FOS.MonitoringStationServices.Contracts.Models
{
	public class FosResult<T> : IFosResult<T>
	{
		#region Properties
		[DataMember]
		public int Code { get; set; }
		[DataMember]
		public string Message { get; set; }

		[DataMember(Name = "Value", Order = int.MaxValue)] 
		public T Value;

		#endregion Properties

		#region Methods
		public object GetValue()
		{
			return Value;
		}
		#endregion Methods
	}
}