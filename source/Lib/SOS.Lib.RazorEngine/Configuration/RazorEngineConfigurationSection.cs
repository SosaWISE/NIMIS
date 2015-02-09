namespace SOS.Lib.RazorEngine.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Defines the main configuration section for the RazorEngine.
    /// </summary>
    public class RazorEngineConfigurationSection : ConfigurationSection
    {
        #region Fields
        private const string _ACTIVATOR_ATTRIBUTE = "activator";
        private const string _FACTORY_ATTRIBUTE = "factory";
        private const string _NAMESPACES_ELEMENT = "namespaces";
        private const string _SECTION_PATH = "razorEngine";
        private const string _TEMPLATE_SERVICES_ELEMENT = "templateServices";
        #endregion

        #region Properties
        /// <summary>
        /// Gets the activator used for this template service.
        /// </summary>
        [ConfigurationProperty(_ACTIVATOR_ATTRIBUTE, IsRequired = false)]
        public string Activator
        {
            get { return (string)this[_ACTIVATOR_ATTRIBUTE]; }
            set { this[_ACTIVATOR_ATTRIBUTE] = value; }
        }

        /// <summary>
        /// Gets or sets the factory used to create compiler service instances.
        /// </summary>
        [ConfigurationProperty(_FACTORY_ATTRIBUTE)]
        public string Factory
        {
            get { return (string)this[_FACTORY_ATTRIBUTE]; }
            set { this[_FACTORY_ATTRIBUTE] = value; }
        }

        /// <summary>
        /// Gets or sets the collection of namespaces.
        /// </summary>
        [ConfigurationProperty(_NAMESPACES_ELEMENT)]
        public NamespaceConfigurationElementCollection Namespaces
        {
            get { return (NamespaceConfigurationElementCollection)this[_NAMESPACES_ELEMENT]; }
            set { this[_NAMESPACES_ELEMENT] = value; }
        }

        /// <summary>
        /// Gets or sets the collection of template service configurations.
        /// </summary>
        [ConfigurationProperty(_TEMPLATE_SERVICES_ELEMENT)]
        public TemplateServiceConfigurationElementConfiguration TemplateServices
        {
            get { return (TemplateServiceConfigurationElementConfiguration)this[_TEMPLATE_SERVICES_ELEMENT]; }
            set { this[_TEMPLATE_SERVICES_ELEMENT] = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets an instance of <see cref="RazorEngineConfigurationSection"/> that represents the current configuration.
        /// </summary>
        /// <returns>An instance of <see cref="RazorEngineConfigurationSection"/>, or null if no configuration is specified.</returns>
        public static RazorEngineConfigurationSection GetConfiguration()
        {
            return ConfigurationManager.GetSection(_SECTION_PATH) as RazorEngineConfigurationSection;
        }
        #endregion
    }
}