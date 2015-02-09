using System;

namespace NXS.Framework.Wpf.ParentChildService
{
	public static class WCFHelper
	{
		#region Constants

		public const string BaseUri = "net.pipe://localhost/PPro.Lib.ParentChildService";
		public const string ParentServiceUri = "ApplicationParent";
		public const string ChildServiceUri = "ApplicationChild";

		#endregion Constants

		#region Properties

		#region Public

		public static string BaseChildUri
		{
			get
			{
				return string.Format("{0}/{1}", BaseUri, ChildServiceUri);
			}
		}

		public static string FullParentServiceUri
		{
			get
			{
				return string.Format("{0}/{1}", BaseUri, ParentServiceUri);
			}
		}

		#endregion Public

		#endregion Properties

		#region Methods

		#region Public

		public static string GetBaseChildServiceUri(Guid childWindowID)
		{
			return string.Format("{0}/{1}/{2}", BaseUri, ChildServiceUri, GetChildServiceUri(childWindowID));
		}

		public static string GetFullChildServiceUri(Guid childWindowID)
		{
			return string.Format("{0}/{1}", GetBaseChildServiceUri(childWindowID), GetChildServiceUri(childWindowID));
		}

		public static string GetChildServiceUri(Guid childWindowID)
		{
			return childWindowID.ToString("N");
		}

		#endregion Public

		#endregion Methods
	}
}