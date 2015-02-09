using System;
using System.Collections.Generic;
using System.Reflection;

namespace SOS.Lib.Util
{
	public static class ReflectionHelper
	{
		public static object GetLastContext(object context, List<string> nestedProperties)
		{
			foreach (string propName in nestedProperties)
			{

				context = GetPropertyValue(context, propName);
				if (context == null)
				{
					break;
				}
			}

			return context;
		}

		public static object GetPropertyValue(object context, string propName)
		{
			if (context == null) return null;

			PropertyInfo pi = context.GetType().GetProperty(propName);
			if (pi == null) return null;

			object result = pi.GetValue(context, null);
			return result;
		}
		public static void SetPropertyValue(object context, string propName, object value)
		{
			if (context == null) return;

			PropertyInfo pi = context.GetType().GetProperty(propName);
			if (pi == null) return;

			pi.SetValue(context, value, null);
		}
		public static Type GetPropertyType(object context, string propName)
		{
			if (context == null) return null;

			PropertyInfo pi = context.GetType().GetProperty(propName);
			if (pi == null) return null;

			return pi.PropertyType;
		}

		public static bool TryCallMethodValue(object context, string methodName, out object result, params object[] parameters)
		{
			if (context == null)
			{
				result = null;
				return false;
			}

			MethodInfo mi = context.GetType().GetMethod(methodName);
			if (mi == null)
			{
				result = null;
				return false;
			}

			result = mi.Invoke(context, parameters);
			return true;
		}
		public static Type GetMethodReturnType(object context, string methodName)
		{
			if (context == null) return null;

			MethodInfo mi = context.GetType().GetMethod(methodName);
			if (mi == null) return null;

			return mi.ReturnType;
		}

		//public static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
		//{
		//    while (toCheck != typeof(object)) {
		//        var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
		//        if (generic == cur) {
		//            return true;
		//        }
		//        toCheck = toCheck.BaseType;
		//    }
		//    return false;
		//}
		public static bool IsInstanceOfGeneric<T>(Type toCheck, out Type foundGeneric)
		{
			Type genericCheck = typeof(T);
			if (genericCheck.IsGenericType)
			{

				genericCheck = genericCheck.GetGenericTypeDefinition();

				while (toCheck != typeof(object))
				{

					if (toCheck.IsGenericType)
					{
						Type toCheckGeneric = toCheck.GetGenericTypeDefinition();
						if (genericCheck == toCheckGeneric)
						{
							foundGeneric = toCheck;
							return true;
						}
					}
					toCheck = toCheck.BaseType;
				}
			}
			foundGeneric = null;
			return false;
		}
		public static Type GetPropertyType(Type t, string propertyName)
		{
			PropertyInfo pi = t.GetProperty(propertyName);
			if (pi == null)
			{
				return null;
			}
			else
			{
				return pi.PropertyType;
			}
		}

		public static Type GetTypeFromAssembly(string assemblyName, string typeName)
		{
			Type resultType;
			Assembly assembly = Assembly.Load(assemblyName);
			if (assembly != null)
			{
				resultType = assembly.GetType(typeName, false, false);
			}
			else
			{
				resultType = null;
			}
			return resultType;
		}
	}
}
