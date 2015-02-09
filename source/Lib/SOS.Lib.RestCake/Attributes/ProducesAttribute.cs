using System;

namespace SOS.Lib.RestCake.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ProducesAttribute : Attribute
	{
		public string ContentType { get; set; }

		public ProducesAttribute(string contentType)
		{
			ContentType = contentType;
		}
	}
}
