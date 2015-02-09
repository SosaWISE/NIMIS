namespace SOS.Lib.RazorEngine.Templating
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines a template writer used for helper templates.
    /// </summary>
    public class TemplateWriter
    {
        #region Fields
        private readonly Action<TextWriter> _writerDelegate;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialises a new instance of <see cref="TemplateWriter"/>.
        /// </summary>
        /// <param name="writer">The writer delegate used to write using the specified <see cref="TextWriter"/>.</param>
        public TemplateWriter(Action<TextWriter> writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            _writerDelegate = writer;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Executes the write delegate and returns the result of this <see cref="TemplateWriter"/>.
        /// </summary>
        /// <returns>The string result of the helper template.</returns>
        public override string ToString()
        {
            using (var writer = new StringWriter())
            {
                _writerDelegate(writer);
                return writer.ToString();
            }
        }
        #endregion
    }
}