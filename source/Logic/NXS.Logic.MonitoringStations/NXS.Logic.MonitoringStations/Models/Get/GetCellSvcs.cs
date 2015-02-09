using System.Collections.Generic;
using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models.Get
{
	public class GetCellSvcs : Base
	{
		#region Properties

		public CellSvc GetCellSvc { get; set; } 

		#endregion Properties

		#region Methods

		public string Serialize()
		{
			return Serialize<GetCellSvcs>();
		}

		#endregion Methods
	}

	public class CellSvc
	{
		#region Properties

		[XmlAttribute(DataType = "string", AttributeName = "cell_provider")]
		public string CellProvider { get; set; }

		#endregion Properties
	}
}
