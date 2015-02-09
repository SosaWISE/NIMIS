using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Nxs.Services.CorsConnext.Models
{
	[DataContract(Name = "CorsConnextResult")]
	[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	public class CorsConnextResult : ICorsConnextResult
	{
		#region .ctor

		public CorsConnextResult(int nCode, string szMessage, string szValueType)
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
	[KnownType(typeof(string))]

	[DataContract(Name = "CorsConnextResult")]
	[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	public class CorsConnextResult<TValueType> : ICorsConnextResult<TValueType> where TValueType : new()
	{
		#region .ctor

		public CorsConnextResult()
		{
			Code = 0; // Default code for success.
		}

		#endregion .ctor

		#region Properties

		[DataMember(Name = "Value", Order = int.MaxValue)]
		public TValueType Value;

		public CorsConnextResult(int nCode, string szMessage, string szValueType)
		{
			Code = nCode;
			Message = szMessage;
			ValueType = szValueType;
			Value = new TValueType();
		}

		public CorsConnextResult(int nCode, string sMessage)
		{
			Code = nCode;
			Message = sMessage;
			ValueType = typeof(TValueType).ToString();
			Value = new TValueType();
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