using System.Collections.Generic;

namespace SonicDb
{
	public class ArgMan
	{
		Dictionary<string, string> _dict;

		public ArgMan(string[] args)
		{
			_dict = new Dictionary<string, string>();
			foreach (var arg in args)
			{
				var i = arg.IndexOf("=");
				string name, value;
				if (i > -1)
				{
					name = arg.Substring(0, i);
					value = arg.Substring(i + 1);
					_dict.Add(name, value);
				}
			}
		}

		public string this[string name]
		{
			get
			{
				string value;
				_dict.TryGetValue(name, out value);
				return value;//?? "";
			}
		}
	}
}
