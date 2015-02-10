using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace SOS.Clients.MVC.CorpSite2.Models
{
	[DataContract(Name = "SosResult")]
	[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	public class SosResult : ISosResult
	{
		#region .ctor

		public SosResult(int nCode, string szMessage, string szValueType)
		{
			Code = nCode;
			Message = szMessage;
			ValueType = szValueType;
		}

		#endregion .ctor

		#region Properties

		public enum MessageCodes
		{
			Success = 0,

			GeneralWarning = 1,

			GeneralException = 100
		}

		[DataMember]
		public object Value { get; set; }

		#endregion Properties

		#region Implementation of IPtpResult

		#region Properties

		[DataMember]
		public int Code { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public long SessionId { get; set; }

		[DataMember]
		public string ValueType { get; set; }

		#endregion Properties

		#region Methods

		public object GetValue()
		{
			return Value;
		}

		#endregion Methods

		#endregion Implementation of IPtpResult	
	}

	[Serializable]
	[KnownType(typeof (string))]

	[DataContract(Name = "SosResult")]
	[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	public class SosResult<TValueType> : ISosResult<TValueType>
	{
		#region .ctor
		public SosResult()
		{
			Code = 0;  // Default code for success.
		}
		#endregion .ctor

		#region Properties

		[DataMember(Name = "Value", Order = int.MaxValue)]
		public TValueType Value;

		public SosResult(int nCode, string szMessage, string szValueType)
		{
			Code = nCode;
			Message = szMessage;
			ValueType = szValueType;
		}

		#endregion Properties

		#region Implementation of IPtpResult

		[DataMember]
		public int Code { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public long SessionId { get; set; }

		[DataMember]
		public string ValueType { get; set; }

		public object GetValue()
		{
			return Value;
		}

		#endregion Implementation of IPtpResult
	}

}