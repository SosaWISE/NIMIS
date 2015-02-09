using System;
using System.Web.Razor;
using System.Web.Razor.Parser;
using SOS.Lib.RazorEngine.Compilation.CSharp;
using SOS.Lib.RazorEngine.Compilation.VisualBasic;

namespace SOS.Lib.RazorEngine.Compilation {
    public class RazorEngineHost : System.Web.Razor.RazorEngineHost {

        public RazorEngineHost(RazorCodeLanguage codeLanguage, Func<MarkupParser> markupParserFactory)
            : base(codeLanguage, markupParserFactory) { }

        public override System.Web.Razor.Generator.RazorCodeGenerator DecorateCodeGenerator(System.Web.Razor.Generator.RazorCodeGenerator generator) {
            if (generator is CSharpRazorCodeGenerator) {
                return new CSharpRazorCodeGenerator(generator.ClassName,
                                                       generator.RootNamespaceName,
                                                       generator.SourceFileName,
                                                       generator.Host, false);
            } else if (generator is VBRazorCodeGenerator) {
                return new VBRazorCodeGenerator(generator.ClassName,
                                                generator.RootNamespaceName,
                                                generator.SourceFileName,
                                                generator.Host, false);
            }
                        
            return base.DecorateCodeGenerator(generator);
        }

        public override ParserBase DecorateCodeParser(ParserBase incomingCodeParser) {
            if (incomingCodeParser is CSharpCodeParser) {
                return new CSharpRazorCodeParser();
            } else {
                //return new RazorVBCodeParser();
                //} else {
                return base.DecorateCodeParser(incomingCodeParser);
            }
        }
    }
}
