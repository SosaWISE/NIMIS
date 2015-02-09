namespace SOS.Lib.RazorEngine.Templating
{
    using System;

    /// <summary>
    /// A Delegate to resolve a template
    /// </summary>
    public delegate string TemplateResolverDelegate(string name, out string location);

    /// <summary>
    /// Defines a template resolver that uses a delegate to resolve a named template.
    /// </summary>
    internal class DelegateTemplateResolver : ITemplateResolver
    {
        #region Fields
        private readonly TemplateResolverDelegate _resolver;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="DelegateTemplateResolver"/>.
        /// </summary>
        /// <param name="resolver">The resolver delegate.</param>
        public DelegateTemplateResolver(TemplateResolverDelegate resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException("resolver");

            _resolver = resolver;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the template with the specified name.
        /// </summary>
        /// <param name="name">The name of the template.</param>
        /// <param name="location">The location of the template. This is used to report errors for the correct file or to be able to debug a template.
        /// This parameter is optional. Set it to null if you don't want provide this information.</param>
        /// <returns>The string template.</returns>
        public string GetTemplate(string name, out string location)
        {
            return _resolver(name, out location);
        }
        #endregion
    }
}