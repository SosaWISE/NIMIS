using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Lib.Core
{
	public class Result<T>
	{
		public int Code { get; set; }
		public string Message { get; set; }
		public T Value { get; set; }

		public Result() { }
		public Result(int code = 0, string message = "", T value = default(T))
		{
			Code = code;
			Message = message;
			Value = value;
		}

		public bool Success
		{
			get { return Code == 0; }
		}
		public bool Failure
		{
			get { return Code != 0; }
		}

		public bool ShouldSerializeSuccess() { return false; }
		public bool ShouldSerializeFailure() { return false; }
	}
}
