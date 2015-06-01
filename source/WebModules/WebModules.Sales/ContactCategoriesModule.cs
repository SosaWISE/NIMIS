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
	public class ContactCategoriesModule : BaseModule
	{
		ContactsService Srv { get { return new ContactsService(this.User.GPEmployeeID); } }

		public ContactCategoriesModule()
			: base("/Sales/Categorys")
		{
			this.RequiresPermission((string)null, null);

			Get["/", true] = async (x, ct) =>
			{
				return (await Srv.CategoriesAsync().ConfigureAwait(false));
			};

			Get["/Icons"] = (x) =>
			{
				#region icons
				var icons = new string[]{
					"blue-arrow.png",
					"blue-blank.png",
					"blue-check.png",
					"blue-do-not-enter.png",
					"blue-flag.png",
					"blue-frown.png",
					"blue-question.png",
					"blue-single.png",
					"blue-smile.png",
					"blue-triangle.png",
					"blue-x.png",
					"gray-arrow.png",
					"gray-blank.png",
					"gray-check.png",
					"gray-do-not-enter.png",
					"gray-flag.png",
					"gray-frown.png",
					"gray-question.png",
					"gray-single.png",
					"gray-smile.png",
					"gray-triangle.png",
					"gray-x.png",
					"green-arrow.png",
					"green-blank.png",
					"green-check.png",
					"green-do-not-enter.png",
					"green-flag.png",
					"green-frown.png",
					"green-question.png",
					"green-single.png",
					"green-smile.png",
					"green-triangle.png",
					"green-x.png",
					"purple-check.png",
					"purple-frown.png",
					"purple-single.png",
					"yellow-arrow.png",
					"yellow-blank.png",
					"yellow-check.png",
					"yellow-do-not-enter.png",
					"yellow-flag.png",
					"yellow-frown.png",
					"yellow-question.png",
					"yellow-single.png",
					"yellow-smile.png",
					"yellow-triangle.png",
					"yellow-x.png",
				};
				#endregion // icons
				return new Result<string[]>(value: icons);
			};

			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<CategoryInput>();
				return (await Srv.SaveCategoryAsync(inputItem).ConfigureAwait(false));
			};

			Delete["/{id:int}", true] = async (x, ct) =>
			{
				return (await Srv.DeleteCategoryAsync((int)x.id).ConfigureAwait(false));
			};
		}
	}
}


// 	public static function get_category_icons() {
// 		User::require_authentication();
// 		$images = array();
// 
// 		$dh = opendir(IMG_PATH . '/map/markers/categories');
// 		while (($file = readdir($dh)) !== FALSE) {
// 			if (substr($file, 0, 1) != '.') {
// 				$path_parts = pathinfo(IMG_PATH . '/map/markers/categories/' . $file);
// 				if ($path_parts['extension'] == 'png')
// 					$images[] = $file;
// 			}
// 		}
// 		closedir($dh);
// 		sort($images);
// 		return $images;
// 	}
