using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
    public class ChangeMonTypes:Base
    {
        #region Properties

        public ChangeMonTypesInfo ChangeMonType { get; set; }

        #endregion Properties

        #region Methods

        public string Serialize()
        {
            return Serialize<ChangeMonTypes>();
        }

        #endregion Methods
    }

    public class ChangeMonTypesInfo
    {
        #region Properties

        [XmlAttribute(DataType = "string", AttributeName = "from_config")]
        public string FromConfig { get; set; }

        [XmlAttribute(DataType = "string", AttributeName = "to_config")]
        public string ToConfig { get; set; }
        
        [XmlAttribute(DataType = "string", AttributeName = "panel_phone")]
        public string PanelPhone { get; set; }
        
        [XmlAttribute(DataType = "string", AttributeName = "site_phone1")]
        public string SitePhone1 { get; set; }
        #endregion Properties
    }

}
