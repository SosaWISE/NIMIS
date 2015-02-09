namespace SOS.Lib.RazorEngine.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Defines the <see cref="ConfigurationElement"/> that represents a namespaces element.
    /// </summary>
    public class NamespaceConfigurationElement : ConfigurationElement
    {
        #region Fields
        private const string _NAMESPACE_ATTRIBUTE = "namespace";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        [ConfigurationProperty(_NAMESPACE_ATTRIBUTE, IsRequired = true, IsKey = true)]
        public string Namespace
        {
            get { return (string)this[_NAMESPACE_ATTRIBUTE]; }
            set { this[_NAMESPACE_ATTRIBUTE] = value; }
        }
        #endregion
    }
}