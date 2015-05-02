using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Lib.Core
{
	public class ResultException : Exception
	{
		public int Code { get; private set; }

		public ResultException(int code, string message)
			: base(message)
		{
			this.Code = code;
		}
	}
}
