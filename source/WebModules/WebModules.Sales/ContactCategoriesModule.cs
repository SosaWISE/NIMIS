using NXS.DataServices.Sales;
using NXS.DataServices.Sales.Models;
using NXS.Lib.Web;
using NXS.Lib.Web.Authentication;
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
			//: base("/Contact", "/ng")
			: base("/Sales/Categorys")
		{
			this.RequiresPermission((string)null, null);

			//$http.get('ng/Contact/get_categories')
			//Get["/get_categories", true] = async (x, ct) =>
			Get["/", true] = async (x, ct) =>
			{
				//var userId = UsersModule.USERID;
				//return (await Srv.CategoriesAsync(userId).ConfigureAwait(false)).Value;
				return (await Srv.CategoriesAsync().ConfigureAwait(false));
			};

			//$http.get('ng/Contact/get_category_icons')
			//Get["/get_category_icons"] = (x) =>
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
				//return icons;
				return new Result<string[]>(value: icons);
			};

			//$http.post('ng/Contact/save_category', {
			//Post["/save_category", true] = async (x, ct) =>
			Post["/", true] = async (x, ct) =>
			{
				var inputItem = this.BindBody<CategoryInput>();
				return (await Srv.SaveCategoryAsync(inputItem).ConfigureAwait(false));
			};

			//$http.post('ng/Contact/delete_category', {
			//Post["/delete_category", true] = async (x, ct) =>
			Delete["/{id:int}", true] = async (x, ct) =>
			{
				return (await Srv.DeleteCategoryAsync((int)x.id).ConfigureAwait(false));
			};
		}
	}
}


// 	public static function get_category_icons() {
// 		User::require_authentication();
// 
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
// 
// 		sort($images);
// 
// 		return $images;
// 	}
// 
// 
// 
// 	/***
// 	* This method saves a new category for the current user
// 	***/
// 	public static function save_category($id, $name, $filename) {
// 		User::require_authentication();
// 
// 		$userid = $_SESSION['userid'];
// 		$id = intval($id);
// 
// 		$_name = db_escape($name);
// 		// check if the image exists
// 		$filename = preg_replace("/[^a-z0-9\.\-_]/i", '', $filename);
// 		if (!file_exists(IMG_PATH . '/map/markers/categories/' . $filename))
// 			throw Exception("Bad filename used for new category marker: $filename");
// 
// 		if ($id) {
// 			$sql = "UPDATE salesContactCategories SET userId=$userid, name='$_name', filename='$filename'
// 				WHERE id=$id";
// 			if (db_query($sql))
// 				return array('id'=>$id, 'name'=>$name, 'filename'=>$filename);
// 		}
// 		else {
// 			$sql = "INSERT INTO salesContactCategories (userId, name, filename)
// 				VALUES($userid, '$_name', '$filename')";
// 			if (db_query($sql)) {
// 				$id = db_get_insert_id();
// 				return array('id'=>$id, 'name'=>$name, 'filename'=>$filename);
// 			}
// 		}
// 		return false;
// 	}
// 
// 
// 	/***
// 	* This method marks a category as deleted for the current user
// 	***/
// 	public static function delete_category($id) {
// 		User::require_authentication();
// 
// 		$userid = $_SESSION['userid'];
// 		$id = intval($id);
// 
// 		$sql = "SELECT userId FROM salesContactCategories
// 			WHERE id=$id";
// 		$result = db_query($sql);
// 		$row = db_fetch_assoc($result);
// 		if ($row) {
// 			if ($row['userId'] == 0) {
// 				// this generic category can't be deleted because it's used by all users.  Instead, we can add it to this user's blacklist
// 				$sql = "INSERT INTO salesContactCategoriesBlacklist
// 					(categoryId, userId) VALUES($id, $userid)";
// 				db_query($sql);
// 			}
// 			else if ($row['userId'] == $userid) {
// 				$sql = "UPDATE salesContactCategories SET status='X'
// 					WHERE id=$id AND userId=$userid";
// 				db_query($sql);
// 			}
// 			else
// 				throw new Exception("You don't have permission to delete that category, troll");
// 		}
// 		else
// 			throw new Exception("Invalid category identifier");
// 
// 		return true;
// 	}
// 
// }
// 
// 