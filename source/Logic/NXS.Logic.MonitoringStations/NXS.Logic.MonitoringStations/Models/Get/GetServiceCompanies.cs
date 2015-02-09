using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models.Get
{
    public class GetServiceCompanies : Base
    {
        public ServiceCompany GetServiceCompany { get; set; }
  
    }

    public class ServiceCompany
    {
        #region Properties

        [XmlAttribute(DataType = "string", AttributeName = "servco_no")]
		public string ServiceCompanyNumber { get; set; }

        #endregion Properties
    }

}

