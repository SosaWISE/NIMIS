using System.IO;
using System.Xml.Serialization;

namespace SOS.Lib.Util
{
	public static class XmlHelper
	{
		public static T DeserializeXml<T>(string path)
		{
			using (var stream = new FileStream(path, FileMode.Open))
			{
				return DeserializeXml<T>(stream);
			}
		}

		public static T DeserializeXml<T>(Stream stream)
		{
			var serializer = new XmlSerializer(typeof (T));
			return (T) serializer.Deserialize(stream);
		}
	}
}