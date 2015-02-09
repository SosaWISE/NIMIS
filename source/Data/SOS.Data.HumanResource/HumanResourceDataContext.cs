using SOS.Data.HumanResource.Controllers;

namespace SOS.Data.HumanResource
{
	public partial class HumanResourceDataContext
	{
		#region Controllers Properties

		private UserDocumentController _userDocuments;
		public UserDocumentController UserDocuments
		{
			get { return _userDocuments ?? (_userDocuments = new UserDocumentController()); }
		}

		private ScannedDocumentController _scannedDocuments;
		public ScannedDocumentController ScannedDocuments
		{
			get { return _scannedDocuments ?? (_scannedDocuments = new ScannedDocumentController()); }
		}

		private SeasonsMapController _seasonsMaps;
		public SeasonsMapController SeasonsMaps
		{
			get { return _seasonsMaps ?? (_seasonsMaps = new SeasonsMapController()); }
		}

		private IDTextController _idTexts;
		public IDTextController IDTexts
		{
			get { return _idTexts ?? (_idTexts = new IDTextController()); }
		}

		#endregion //Controllers Properties
	}

}
