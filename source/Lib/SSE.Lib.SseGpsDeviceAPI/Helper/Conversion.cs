using System;

namespace SSE.Lib.SseGpsDeviceAPI.Helper
{
	public class Conversion
	{
		#region .ctor

		private Conversion()
		{
		}

		#endregion .ctor

		#region Properties

		#region Singleton Constructor
		private static Conversion _instance;
		private static readonly object InstanceSync = new object();
		public static Conversion Instance
		{
			get
			{
				if (_instance == null)
					lock (InstanceSync)
					{
						if (_instance == null)
// ReSharper disable PossibleMultipleWriteAccessInDoubleCheckLocking
							return _instance = new Conversion();
// ReSharper restore PossibleMultipleWriteAccessInDoubleCheckLocking
					}

				/** Return result. */
				return _instance;
			}
		}
		#endregion Singleton Constructor

		#endregion Properties

		#region Methods

		private const double _KM_TO_KT_RATIO = 0.539956803455724;

		public double KmToKt(double kmh)
		{
			/** Initialize. */
			double result = _KM_TO_KT_RATIO*kmh;

			/** Return result. */
			return result;
		}

		public double KmToKt(string kmh)
		{
			return KmToKt(Convert.ToDouble(kmh));
		}

		#endregion Methods
	}
}
