namespace SOS.Lib.RazorEngine.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Defines the <see cref="ConfigurationElement"/> that represents a template service element.
    /// </summary>
    public class TemplateServiceConfigurationElement : ConfigurationElement
    {
        #region Fields
        private const string _ACTIVATOR_ATTRIBUTE = "activator";
        private const string _LANGUAGE_ATTRIBUTE = "language";
        private const string _MARKUP_PARSER_ATTRIBUTE = "markupParser";
        private const string _NAME_ATTRIBUTE = "name";
        private const string _NAMESPACES_ATTRIBUTE = "namespaces";
        private const string _STRICT_MODE_ATTRIBUTE = "strictMode";
        private const string _TEMPLATE_BASE_ATTRIBUTE = "templateBase";
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
        /// Defines the language supported by the template service.
        /// </summary>
        [ConfigurationProperty(_LANGUAGE_ATTRIBUTE, IsRequired = false, DefaultValue = Language.CSharp)]
        public Language Language
        {
            get { return (Language)this[_LANGUAGE_ATTRIBUTE]; }
            set { this[_LANGUAGE_ATTRIBUTE] = value; }
        }

        /// <summary>
        /// Gets or sets the markup parser.
        /// </summary>
        [ConfigurationProperty(_MARKUP_PARSER_ATTRIBUTE)]
        public string MarkupParser
        {
            get { return (string)this[_MARKUP_PARSER_ATTRIBUTE]; }
            set { this[_MARKUP_PARSER_ATTRIBUTE] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the template service.
        /// </summary>
        [ConfigurationProperty(_NAME_ATTRIBUTE, IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this[_NAME_ATTRIBUTE]; }
            set { this[_NAME_ATTRIBUTE] = value; }
        }

        /// <summary>
        /// Gets or sets the collection of namespaces.
        /// </summary>
        [ConfigurationProperty(_NAMESPACES_ATTRIBUTE)]
        public NamespaceConfigurationElementCollection Namespaces
        {
            get { return (NamespaceConfigurationElementCollection)this[_NAMESPACES_ATTRIBUTE]; }
            set { this[_NAMESPACES_ATTRIBUTE] = value; }
        }

        /// <summary>
        /// Gets or sets whether the template service should be running in strict mode.
        /// </summary>
        [ConfigurationProperty(_STRICT_MODE_ATTRIBUTE)]
        public bool StrictMode
        {
            get { return (bool)this[_STRICT_MODE_ATTRIBUTE]; }
            set { this[_STRICT_MODE_ATTRIBUTE] = value; }
        }

        /// <summary>
        /// Gets or sets the template base
        /// </summary>
        [ConfigurationProperty(_TEMPLATE_BASE_ATTRIBUTE)]
        public string TemplateBase
        {
            get { return (string)this[_TEMPLATE_BASE_ATTRIBUTE]; }
            set { this[_TEMPLATE_BASE_ATTRIBUTE] = value; }
        }
        #endregion
    }
}