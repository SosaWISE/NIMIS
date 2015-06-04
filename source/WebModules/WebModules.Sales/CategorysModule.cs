using NXS.DataServices.Sales;
using NXS.DataServices.Sales.Models;
using NXS.Lib;
using NXS.Lib.Authentication;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Sales
{
	public class CategorysModule : BaseModule
	{
		ContactsService Srv { get { return new ContactsService(this.User.GPEmployeeID); } }

		public CategorysModule()
			: base("/Sales/Categorys")
		{
			this.RequiresPermission((string)null, null);

			Get["/", true] = async (x, ct) =>
			{
				return (await Srv.CategoriesAsync().ConfigureAwait(false));
			};

			Get["/Icons"] = (x) =>
			{
				return new Result<string[]>(value: Srv.CategoryIcons());
			};

			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<CategoryInput>();
				return (await Srv.SaveCategoryAsync(inputItem).ConfigureAwait(false));
			};
			Post["/{id:int}", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<CategoryInput>();
				inputItem.ID = (int)x.id;
				return (await Srv.SaveCategoryAsync(inputItem).ConfigureAwait(false));
			};

			Delete["/{id:int}", true] = async (x, ct) =>
			{
				return (await Srv.DeleteCategoryAsync((int)x.id).ConfigureAwait(false));
			};
		}
	}
}
