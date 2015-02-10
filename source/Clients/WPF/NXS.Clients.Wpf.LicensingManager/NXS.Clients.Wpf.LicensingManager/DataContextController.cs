using NXS.Data.GreatPlains;
using NXS.Data.Letters;
using NXS.Data.Licensing;
using SOS.Data.HumanResource;
using SOS.Data.SosCrm;
using StructureMap;

namespace NXS.Clients.Wpf.LicensingManager
{
	public class DataContextController
	{
		#region Properties

		#region Private
		private SosCrmDataContext _interimContext;
		private HumanResourceDataContext _recruitingContext;
		private GreatPlainsDataContext _greatPlainsContext;
		private LicensingDataContext _licensingContext;
		private LettersDataContext _letterContext;
		#endregion Private

		#region Public

		public SosCrmDataContext InterimContext
		{
			get
			{
				if (_interimContext == null)
					_interimContext = ObjectFactory.GetInstance<SosCrmDataContext>();
				return _interimContext;
			}
		}

		public HumanResourceDataContext RecruitingContext
		{
			get
			{
				if (_recruitingContext == null)
					_recruitingContext = ObjectFactory.GetInstance<HumanResourceDataContext>();
				return _recruitingContext;
			}
		}

		public GreatPlainsDataContext GreatPlainsContext
		{
			get
			{
				if (_greatPlainsContext == null)
					_greatPlainsContext = ObjectFactory.GetInstance<GreatPlainsDataContext>();
				return _greatPlainsContext;
			}
		}
		public LicensingDataContext LicensingContext
		{
			get
			{
				if (_licensingContext == null)
					_licensingContext = ObjectFactory.GetInstance<LicensingDataContext>();
				return _licensingContext;
			}
		}

		public LettersDataContext LetterContext
		{
			get
			{
				if (_letterContext == null)
					_letterContext = ObjectFactory.GetInstance<LettersDataContext>();
				return _letterContext;
			}
		}
		#endregion Public

		#endregion Properties

		#region Singleton Implementation

		private DataContextController()
		{
		}

		public static DataContextController Instance
		{
			get
			{
				return Nested.ControllerInstance;
			}
		}

		private class Nested
		{
			static Nested()
			{
			}

			internal static readonly DataContextController ControllerInstance = new DataContextController();
		}

		#endregion Singleton Implementation
	}
}
