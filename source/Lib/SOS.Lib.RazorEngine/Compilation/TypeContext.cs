namespace SOS.Lib.RazorEngine.Compilation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a type context used for compilation.
    /// </summary>
    public class TypeContext
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="TypeContext"/>.
        /// </summary>
        public TypeContext()
        {
            ClassName = CompilerServices.GenerateClassName();
            Namespaces = new HashSet<string>();   
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the class name.
        /// </summary>
        public string ClassName { get; private set; }

        /// <summary>
        /// Gets or sets the model type.
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// Gets the set of namespace imports.
        /// </summary>
        public ISet<string> Namespaces { get; private set; }

        /// <summary>
        /// Gets or sets the template content.
        /// </summary>
        public string TemplateContent { get; set; }

        /// <summary>
        /// Gets or sets the template type.
        /// </summary>
        public Type TemplateType { get; set; }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the template location.
        /// </summary>
        /// <value>The template location.</value>
        public string TemplateLocation { get; set; }
        #endregion 
    }
}