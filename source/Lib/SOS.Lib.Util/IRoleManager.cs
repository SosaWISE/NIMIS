namespace SOS.Lib.Util
{
	public interface IRoleManager
	{
		bool IsUserInRole(string username, string roleName);
		bool RoleExists(string roleName);
		string[] GetUsersInRole(string roleName);
		string[] GetRolesForUser(string username);
		string[] GetAllRoles();
	}
}