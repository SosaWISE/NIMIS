using System;
using SOS.Lib.RazorEngine.Compilation;
using SOS.Lib.RazorEngine.Templating;

namespace SOS.Lib.RazorEngine
{
	public class Templater
	{
		private readonly ICompilerServiceFactory _compilerServiceFactory;
		private readonly TemplateService _templateService;

		public Templater()
		{
			_compilerServiceFactory = new DefaultCompilerServiceFactory();
			var service = _compilerServiceFactory.CreateCompilerService();

			_templateService = new TemplateService(service);

		}
		/// <summary>
		/// Adds a resolver used to resolve named template content.
		/// </summary>
		/// <param name="resolver">The resolver to add.</param>
		public void AddResolver(ITemplateResolver resolver)
		{
			_templateService.AddResolver(resolver);
		}

		/// <summary>
		/// Adds a resolver used to resolve named template content.
		/// </summary>
		/// <param name="resolverDelegate">The resolver delegate to add.</param>
		public void AddResolver(TemplateResolverDelegate resolverDelegate)
		{
			_templateService.AddResolver(resolverDelegate);
		}

		/// <summary>
		/// Pre-compiles the specified template and caches it using the specified name.
		/// </summary>
		/// <param name="template">The template to precompile.</param>
		/// <param name="name">The cache name for the template.</param>
		/// <param name="location">[Optional] The location of the template. This is used to report errors for the correct file or to be able to debug a template. Set it to null if you don't want provide this information. </param>
		public void Compile(string template, string name, string location = null)
		{
			_templateService.CompileWithAnonymous(template, name, location);
		}

		/// <summary>
		/// Pre-compiles the specified template and caches it using the specified name.
		/// </summary>
		/// <param name="template">The template to precompile.</param>
		/// <param name="modelType">The type of model used in the template.</param>
		/// <param name="name">The cache name for the template.</param>
		/// <param name="location">[Optional] The location of the template. This is used to report errors for the correct file or to be able to debug a template. Set it to null if you don't want provide this information. </param>
		public void Compile(string template, Type modelType, string name, string location = null)
		{
			_templateService.Compile(template, modelType, name, location);
		}

		/// <summary>
		/// Pre-compiles the specified template and caches it using the specified name.
		/// This method should be used when an anonymous model is used in the template.
		/// </summary>
		/// <param name="template">The template to precompile.</param>
		/// <param name="name">The cache name for the template.</param>
		/// <param name="location">[Optional] The location of the template. This is used to report errors for the correct file or to be able to debug a template. Set it to null if you don't want provide this information. </param>
		public void CompileWithAnonymous(string template, string name, string location = null)
		{
			_templateService.CompileWithAnonymous(template, name, location);
		}

		/// <summary>
		/// Parses the given template and returns the result.
		/// </summary>
		/// <param name="template">The template to parse.</param>
		/// <param name="name">[Optional] The name of the template. This is used to cache the template.</param>
		/// <param name="location">[Optional] The location of the template. This is used to report errors for the correct file or to be able to debug a template. Set it to null if you don't want provide this information. </param>
		/// <returns>
		/// The string result of the parsed template.
		/// </returns>
		public string Parse(string template, string name = null, string location = null)
		{
			return _templateService.Parse(template, name, location);
		}

		/// <summary>
		/// Parses the given template and returns the result.
		/// </summary>
		/// <typeparam name="T">The model type.</typeparam>
		/// <param name="template">The template to parse.</param>
		/// <param name="model">The model.</param>
		/// <param name="name">[Optional] The name of the template. This is used to cache the template.</param>
		/// <param name="location">[Optional] The location of the template. This is used to report errors for the correct file or to be able to debug a template. Set it to null if you don't want provide this information. </param>
		/// <returns>The string result of the parsed template.</returns>
		public string Parse<T>(string template, T model, string name = null, string location = null)
		{
			return _templateService.Parse<T>(template, model, name, location);
		}

		/// <summary>
		/// Runs the template with the specified name.
		/// </summary>
		/// <param name="name">The name of the template to run.</param>
		/// <returns>The result of the template.</returns>
		public string Run(string name)
		{
			return _templateService.Run(name);
		}

		/// <summary>
		/// Runs the template with the specified name.
		/// </summary>
		/// <typeparam name="T">The model type.</typeparam>
		/// <param name="model">The model.</param>
		/// <param name="name">The name of the template to run.</param>
		/// <returns>The result of the template.</returns>
		public string Run<T>(T model, string name)
		{
			return _templateService.Run<T>(model, name);
		}

		/// <summary>
		/// Sets the activator used to create types.
		/// </summary>
		/// <param name="activator">The activator to use.</param>
		public void SetActivator(IActivator activator)
		{
			_templateService.SetActivator(activator);
		}

		/// <summary>
		/// Sets the activator delegate used to create types.
		/// </summary>
		/// <param name="activator">The activator delegate to use.</param>
		public void SetActivator(Func<Type, ITemplate> activator)
		{
			_templateService.SetActivator(activator);
		}

		/// <summary>
		/// Sets the template base type.
		/// </summary>
		/// <param name="type">The template base type.</param>
		public void SetTemplateBase(Type type)
		{
			_templateService.SetTemplateBase(type);
		}
	}
}
