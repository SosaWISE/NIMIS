﻿namespace SOS.Lib.RazorEngine.Compilation.VisualBasic
{
    using System;
    using System.Linq;
    using System.Web.Razor.Parser;

    using Microsoft.VisualBasic;

    /// <summary>
    /// Defines a direct compiler service for the VB syntax.
    /// </summary>
    public class VBDirectCompilerService : DirectCompilerServiceBase
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="VBDirectCompilerService"/>.
        /// </summary>
        /// <param name="strictMode">Specifies whether the strict mode parsing is enabled.</param>
        /// <param name="markupParser">The markup parser to use.</param>
        public VBDirectCompilerService(bool strictMode = true, MarkupParser markupParser = null)
            : base(
                new VBRazorCodeLanguage(strictMode),
                new VBCodeProvider(),
                markupParser) { }
        #endregion

        #region Methods
        /// <summary>
        /// Builds a type name for the specified generic type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="isDynamic">Specifies whether the type is dynamic.</param>
        /// <returns>
        /// The string typename (including namespace and generic type parameters).
        /// </returns>
        public override string BuildTypeNameInternal(Type type, bool isDynamic)
        {
            if (!type.IsGenericType)
                return type.FullName;

            return type.Namespace
                   + "."
                   + type.Name.Substring(0, type.Name.IndexOf('`'))
                   + "(Of "
                   + (isDynamic ? "Object" : string.Join(", ", type.GetGenericArguments().Select(t => BuildTypeNameInternal(t, CompilerServices.IsDynamicType(t)))))
                   + ")";
        }
        #endregion
    }
}