using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Parser;

namespace SOS.Lib.RazorEngine.Compilation.Spans
{
    public class ModelSpan : CodeSpan {
        public ModelSpan(SourceLocation start, string content, string modelTypeName) : base(start, content) {
            this.ModelTypeName = modelTypeName;
        }

        public string ModelTypeName { get; private set; }

        public override int GetHashCode() {
            return base.GetHashCode() ^ (ModelTypeName ?? String.Empty).GetHashCode();
        }

        public override bool Equals(object obj) {
            ModelSpan span = obj as ModelSpan;
            return span != null && Equals(span);
        }

        private bool Equals(ModelSpan span) {
            return base.Equals(span) && string.Equals(ModelTypeName, span.ModelTypeName, StringComparison.Ordinal);
        }

        public new static ModelSpan Create(ParserContext context, string modelTypeName) {
            return new ModelSpan(context.CurrentSpanStart, context.ContentBuffer.ToString(), modelTypeName);
        }
    }
}
