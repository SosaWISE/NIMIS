using Newtonsoft.Json;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSE.Services.CmsCORS.Helpers
{
	public class ResultAny<T> : Result<T>
	{
		public bool ValueSuccess() { return false; }
		[JsonProperty(PropertyName = "Value")]
		public object RealValue { get; set; }

		public ResultAny(int code, string message, object realValue)
			: base(code, message, default(T))
		{
			this.RealValue = realValue;
		}
	}
}