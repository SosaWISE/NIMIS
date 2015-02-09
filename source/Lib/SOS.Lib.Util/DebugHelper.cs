using System;
using System.ComponentModel;
using System.Diagnostics;

namespace SOS.Lib.Util
{
	public static class DebugHelper
	{
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void VerifyPropertyName(object obj, string propertyName)
		{
			VerifyPropertyName(obj, propertyName, false);
		}

		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void VerifyPropertyName(object obj, string propertyName, bool throwOnInvalidPropertyName)
		{
			// Verify that the property name matches a real,  
			// public, instance property on this object.
			if (TypeDescriptor.GetProperties(obj)[propertyName] == null)
			{
				string msg = "Invalid property name: " + propertyName;

				if (throwOnInvalidPropertyName)
				{
					throw new Exception(msg);
				}
				else
				{
					Debug.Fail(msg);
				}
			}
		}
	}
}