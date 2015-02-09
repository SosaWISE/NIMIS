using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NXS.Logic.MonitoringStations.Models
{
	public class Base
	{

		#region Methods

		public string Serialize<T>()
		{
			// ** Initialize
			// Convert to XML.
			var x = new XmlSerializer(typeof(T));
			var mStream = new MemoryStream();
			var xmlTextWriter = new XmlTextWriter(mStream, Encoding.UTF8);
			x.Serialize(xmlTextWriter, this);
			mStream = (MemoryStream)xmlTextWriter.BaseStream;
			var encoding = new UTF8Encoding();
			string result = encoding.GetString(mStream.ToArray());
			result = result.Substring(1);

			// ** Return 
			return result;
		}

		#endregion Methods
	}
}
