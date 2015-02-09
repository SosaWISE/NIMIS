using System.Collections.Generic;

namespace NXS.Framework.Wpf.Mvvm.Models
{
	public class MenuNode : BaseLink
	{
		public List<MenuNode> Children { get; set; }
	}
}