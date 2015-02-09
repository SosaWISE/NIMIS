using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountCellularType : IMsAccountCellularType
	{
		#region Properties
		public string CellularTypeID { get; set; }
		public string CellularTypeName { get; set; }
		#endregion Properties
	}

	#region Interface

	public interface IMsAccountCellularType
	{
		#region Properties
		[DataMember]
		string CellularTypeID { get; set; }
		[DataMember]
		string CellularTypeName { get; set; }

		#endregion Properties
	}

	#endregion Interface
}