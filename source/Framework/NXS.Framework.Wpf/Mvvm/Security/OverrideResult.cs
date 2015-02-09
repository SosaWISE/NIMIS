using System;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	public class OverrideResult
	{
		public UserSecurityInfo User { get; private set; }
		/// <summary>
		/// If true the override doesn't stop once the command has executed. The override will need to be stopped manually.
		/// </summary>
		public bool IsPermanent { get; private set; }

		public OverrideResult(UserSecurityInfo user)
			: this(user, false)
		{
		}
		public OverrideResult(UserSecurityInfo user, bool isPermanent)
		{
			if (user == null)
				throw new ArgumentNullException("user");

			this.User = user;
			this.IsPermanent = isPermanent;
		}
	}
}
