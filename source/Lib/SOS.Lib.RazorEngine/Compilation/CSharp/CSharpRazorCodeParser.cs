using System;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using SOS.Lib.RazorEngine.Compilation.Spans;

namespace SOS.Lib.RazorEngine.Compilation.CSharp {
    public class CSharpRazorCodeParser : CSharpCodeParser {
        public string TypeName { get; set; }

        public CSharpRazorCodeParser() {
            RazorKeywords.Add("model", WrapSimpleBlockParser(BlockType.Directive, ParseModelStatement));
            RazorKeywords.Add("text", WrapSimpleBlockParser(BlockType.Statement, ParseTextBlock));

        }

        bool ParseTextBlock(CodeBlockInfo block) {
            bool complete = false;

            RequireSingleWhiteSpace();
            Context.AcceptWhiteSpace(includeNewLines: false);

            using (Context.StartTemporaryBuffer()) {
                Context.AcceptWhiteSpace(includeNewLines: true);
                if (CurrentCharacter != '{') { //this isn't a valid section throw an error
                    Context.RejectTemporaryBuffer();
                    OnError(CurrentLocation, "The @text {} must be a code block");
                    End(SectionHeaderSpan.Create(Context, "text", acceptedCharacters: AcceptedCharacters.Any));
                    return false;
                } else {
                    Context.AcceptTemporaryBuffer();
                }
            }

            Context.AcceptCurrent();

            Context.SwitchActiveParser();
            Context.MarkupParser.ParseSection(Tuple.Create("{", "}"), caseSensitive: true);
            Context.SwitchActiveParser();

            if (Context.CurrentCharacter == '}') {
                complete = true;
                Context.AcceptCurrent();
                End(MetaCodeSpan.Create(Context, hidden: false, acceptedCharacters: AcceptedCharacters.None));
            }

            return complete;
        }

        bool ParseModelStatement(CodeBlockInfo block) {
            End(MetaCodeSpan.Create);

            SourceLocation endModelLocation = CurrentLocation;

            Context.AcceptWhiteSpace(includeNewLines: false);

            if (ParserHelpers.IsIdentifierStart(CurrentCharacter)) {
                using (Context.StartTemporaryBuffer()) {
                    AcceptTypeName();
                    Context.AcceptTemporaryBuffer();
                }
            } else {
                OnError(endModelLocation, "Model Keyword Must Be Followed By Type Name");
            }

            End(ModelSpan.Create(Context, TypeName));

            return false;
        }
    }
}
