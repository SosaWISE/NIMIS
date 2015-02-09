using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace SSE.Services.CmsCORS.Models
{
	[DataContract(Name = "CmsCORSResult")]
	[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	public class CmsCORSResult : ICmsResult
	{
		#region .ctor

		public CmsCORSResult(int nCode, string szMessage, string szValueType)
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

	[DataContract(Name = "CmsCORSResult")]
	[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	public class CmsCORSResult<TValueType> : ICmsResult<TValueType> where TValueType : new()
	{
		#region .ctor

		public CmsCORSResult()
		{
			Code = 0; // Default code for success.
		}

		#endregion .ctor

		#region Properties

		[DataMember(Name = "Value", Order = int.MaxValue)]
		public TValueType Value;

		public CmsCORSResult(int nCode, string szMessage, string szValueType)
		{
			Code = nCode;
			Message = szMessage;
			ValueType = szValueType;
			Value = new TValueType();
		}

		public CmsCORSResult(int nCode, string sMessage)
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

	//public interface IApiResult
	//{
	//	int Code { get; set; }
	//	string Message { get; set; }
	//	object Value { get; }
	//}
	//[DataContract(Name = "ApiResult")]
	//[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	//public class ApiResult<T> : IApiResult
	//{
	//	[DataMember]
	//	public int Code { get; set; }
	//	[DataMember]
	//	public string Message { get; set; }
	//	[DataMember]
	//	public object Value { get; private set; }
	//
	//	public T TValue
	//	{
	//		get { return (T)Value; }
	//		set { Value = value; }
	//	}
	//	public ApiResult(int code = 0, string message = "", T value = default(T))
	//	{
	//		Code = code;
	//		Message = message;
	//		Value = value;
	//	}
	//
	//	public ApiResult<T> FromFnsResult(SOS.FunctionalServices.Contracts.Models.IFnsResult<T> fnsResult)
	//	{
	//		Code = fnsResult.Code;
	//		Message = fnsResult.Message;
	//		TValue = fnsResult.GetTValue();
	//		return this;
	//	}
	//}
}