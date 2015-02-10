using System.Diagnostics;

namespace SOS.services.Wcf.Signals.Support
{
	[DebuggerDisplay("MethodName = {MethodName}, Type = {Type}, Template = {Template.ToString()}")]
	public class CallParameters
	{
		public string MethodName { get; set; }
		public UriTemplate Template { get; set; }
		public CallType Type { get; set; }
	}
}