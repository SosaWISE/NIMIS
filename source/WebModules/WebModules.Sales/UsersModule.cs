using NXS.DataServices.Sales;
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
	public class UsersModule //: BaseModule
	{
		public const int USERID = 8;
		public const string COMPANYID = "SHUMA001";

		//BlahService Srv { get { return new BlahService(this.User.GPEmployeeID); } }
		//
		//public UsersModule()
		//	: base("/User", "/ng")
		//{
		//
		//	//# $http.post('ng/User/sign_in', {
		//	Post["/sign_in", true] = async (x, ct) =>
		//	{
		//		var creds = this.BindBody<Creds>();
		//		var user = await Srv.UserSignInAsync(creds.username, creds.password).ConfigureAwait(false);
		//		return new
		//		{
		//			results = user,
		//			success = user != null,
		//		};
		//	};
		//	//# $http.post('ng/User/sign_out', {})
		//	Post["/sign_out", true] = async (x, ct) =>
		//	{
		//		await Task.Delay(10);
		//		return true;
		//	};
		//
		//	//$http.post('ng/User/save_password', {
		//	Post["/save_password", true] = async (x, ct) =>
		//	{
		//		//@QUANDARY: shouldn't this be asking for the current password??
		//		var userId = UsersModule.USERID;
		//		var creds = this.BindBody<UpdateCreds>();
		//		var count = await Srv.UpdatePasswordAsync(userId, creds.password1).ConfigureAwait(false);
		//		return new
		//		{
		//			results = count != 0,
		//			success = count != 0,
		//		};
		//	};
		//	//public static function save_password($userId, $password1, $password2) {
		//	//	if ($userId != $_SESSION['userid'])
		//	//		throw new Exception("You don't gots permission to change someone's password");
		//	//
		//	//	if ($password1 != $password2)
		//	//		throw new Exception("Your passwords don't match, goofnut");
		//	//
		//	//	$sql = "UPDATE users SET password=MD5('$password1') WHERE id='$userId'";
		//	//	$result = db_query($sql);
		//	//
		//	//	return true;
		//	//}
		//
		//	//$http.post('ng/User/save_pin', {
		//	Post["/save_pin", true] = async (x, ct) =>
		//	{
		//		var userId = UsersModule.USERID;
		//		var creds = this.BindBody<UpdateCreds>();
		//		var count = await Srv.UpdatePinAsync(userId, creds.pin1).ConfigureAwait(false);
		//		return new
		//		{
		//			results = count != 0,
		//			success = count != 0,
		//		};
		//	};
		//	//public static function save_pin($userId, $pin1, $pin2) {
		//	//	if ($userId != $_SESSION['userid'])
		//	//		throw new Exception("You don't gots permission to change someone's PIN");
		//	//
		//	//	if ($pin1 != $pin2)
		//	//		throw new Exception("Your passwords don't match, pinhead");
		//	//
		//	//	$sql = "UPDATE users SET PIN=MD5('$pin1') WHERE id='$userId'";
		//	//	$result = db_query($sql);
		//	//
		//	//	return true;
		//	//}
		//}
	}

	//public class Creds
	//{
	//	public string username { get; set; }
	//	public string password { get; set; }
	//}
	//public class UpdateCreds
	//{
	//	public string password1 { get; set; }
	//	public string pin1 { get; set; }
	//}
}
