namespace SOS.Lib.RazorEngine.Compilation.CSharp
{
    using System.Web.Razor;
    using System.Web.Razor.Parser.SyntaxTree;

    using Templating;
    using Spans;

    /// <summary>
    /// Defines a code generator that supports C# syntax.
    /// </summary>
    public class CSharpRazorCodeGenerator : System.Web.Razor.Generator.CSharpRazorCodeGenerator
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CSharpRazorCodeGenerator"/> class.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="rootNamespaceName">Name of the root namespace.</param>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="host">The host.</param>
        /// <param name="strictMode">Flag to specify that this generator is running in struct mode.</param>
        public CSharpRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host, bool strictMode)
            : base(className, rootNamespaceName, sourceFileName, host)
        {
            StrictMode = strictMode;
        }
        #endregion

        #region Model Keyword Support
        protected override bool TryVisitSpecialSpan(Span span) {
            return TryVisit<ModelSpan>(span, VisitModelSpan); //This is where you would add more special span tests || TryVisit<SomeOtherSpan>(span, Method);
        }

        void VisitModelSpan(ModelSpan span) {
            //string modelName = span.ModelTypeName;

            if (DesignTimeMode) {
                WriteHelperVariable(span.Content, "__modelHelper");
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether the code generator is running in strict mode.
        /// </summary>
        public bool StrictMode { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Visits an error generated through parsing.
        /// </summary>
        /// <param name="err">The error that was generated.</param>
        public override void VisitError(RazorError err)
        {
            if (StrictMode)
                throw new TemplateParsingException(err);
        }
        #endregion
    }
}