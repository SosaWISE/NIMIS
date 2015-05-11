using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDb.Langs
{
	public interface ILang
	{
		string Name();
		string FileExt();
		string GetSysType(string sqlType);
		string GetSysTypeFull(string sqlType, bool isNullable);
		string FormatArgs(SP sp);
	}
}
