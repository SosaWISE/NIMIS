namespace SOS.Lib.RazorEngine.Templating
{
    /// <summary>
    /// Defines the required contract for implementing a template resolver.
    /// </summary>
    public interface ITemplateResolver
    {
        #region Methods
        /// <summary>
        /// Gets the template with the specified name.
        /// </summary>
        /// <param name="name">The name of the template.</param>
        /// <param name="location">The location of the template. This is used to report errors for the correct file or to be able to debug a template.
        /// This parameter is optional. Set it to null if you don't want provide this information.
        /// </param>
        /// <returns>The string template.</returns>
        string GetTemplate(string name, out string location);
        #endregion
    }
}
