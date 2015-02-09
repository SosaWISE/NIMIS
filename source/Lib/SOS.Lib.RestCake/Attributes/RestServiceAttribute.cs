using System;

namespace SOS.Lib.RestCake.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class RestServiceAttribute : Attribute
	{
		public string Namespace { get; set; }
		public string Name { get; set; }
		public bool EnableHelp { get; set; }
		public Type ServiceContract { get; set; }
	}
}
