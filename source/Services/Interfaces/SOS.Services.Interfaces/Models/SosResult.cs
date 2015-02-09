using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	[DataContract(Name = "SosResult")]
	[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	public class SosResult : IServiceResult
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

		[DataMember]
		public object Value { get; set; }

		#endregion Properties

		#region Implementation of IServiceResult

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

		#endregion Implementation of IServiceResult
	}

	[Serializable]
	[KnownType(typeof(SosSessionInfo))]

	[DataContract(Name = "SosResult")]
	[DebuggerDisplay("Code = {Code}, Message = {Message}")]
	public class SosResult<TValueType> : IServiceResult<TValueType>
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

		public SosResult(int nCode, string sMessage)
		{
			Code = nCode;
			Message = sMessage;
			ValueType = typeof (TValueType).ToString();
		}

		#endregion Properties
	
		#region Implementation of IServiceResult

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

		#endregion Implementation of IServiceResult
	}
}