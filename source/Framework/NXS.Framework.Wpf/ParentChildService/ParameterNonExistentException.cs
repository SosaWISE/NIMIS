using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PPro.Framework.WpfFramework.ParentChildService
{
	public class ParameterNonExistentException : Exception
	{
		public ParameterNonExistentException(string parameterName)
			: base(string.Format("{0} doesn't exist in ParameterDictionary", parameterName))
		{
		}
	}
}
