using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using SOS.Lib.RestCake.Attributes;
using SOS.Lib.RestCake.Util;


namespace SOS.Lib.RestCake.Metadata
{
	public class AssemblyMetadata
	{
		private readonly Assembly _mAssembly;
		public List<ServiceMetadata> Services { get; private set; }

		public string AssemblyName
		{
			get { return _mAssembly.GetName().Name; }
		}


		public AssemblyMetadata(string assmeblyToparse)
			// Using LoadFrom() instead of Load() is very important!  LoadFrom() will resolve dependencies.  Load() will not.
			// This is important, since the likely have dependencies on a data layer (DTO classes in a diff assembly, etc)
			// That will cause errors unless you use LoadFrom(), which will resolve those dependencies.
			: this(Assembly.LoadFrom(Path.GetFullPath(assmeblyToparse)))
		{ }

		public AssemblyMetadata(Assembly assembly)
		{
			_mAssembly = assembly;
			Services = new List<ServiceMetadata>();
			PopulateServices();
		}

		private void PopulateServices()
		{
			// WCF and RestCake services
			IEnumerable<Type> serviceTypes = ReflectionHelper.GetTypesWithAttribute(_mAssembly, typeof (ServiceContractAttribute)) // WCF
				.Concat(ReflectionHelper.GetTypesWithAttribute(_mAssembly, typeof (RestServiceAttribute))); // RestCake

			foreach (Type serviceType in serviceTypes)
				Services.Add(new ServiceMetadata(serviceType));
		}


	}
}
