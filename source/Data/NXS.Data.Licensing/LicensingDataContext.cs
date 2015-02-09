
using NXS.Data.Licensing.Controllers;

namespace NXS.Data.Licensing
{
    public partial class LicensingDataContext
    {
		#region Controllers Properties

		MetAndNeededRequirementController _metAndNeededRequirements;
		public MetAndNeededRequirementController MetAndNeededRequirements
		{
			get { return _metAndNeededRequirements ?? (_metAndNeededRequirements = new MetAndNeededRequirementController()); }
		}


		#endregion Controllers Properties
	}
}
