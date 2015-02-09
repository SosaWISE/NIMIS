using System;

namespace SOS.Services.Interfaces
{
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
	public class ServiceUrlAttribute : Attribute
	{
		public string Version { get; set; }
		public string BaseUrl { get; set; }

		public ServiceUrlAttribute(string version, string baseUrl)
		{
			Version = version;
			BaseUrl = baseUrl;
		}
	}
}
