namespace SSE.Lib.HE910API.Helper
{
	public class GPSIndicator
	{
		#region .ctor

		public GPSIndicator(IndicatorTypes type)
		{
			_type = type;
		}

		#endregion .ctor

		#region Properties

		private readonly IndicatorTypes _type;
		public string Value
		{
			get
			{
				switch (_type)
				{
					case IndicatorTypes.N:
						return "N";
					case IndicatorTypes.E:
						return "E";
					case IndicatorTypes.W:
						return "W";
					case IndicatorTypes.S:
						return "S";
					default:
						return string.Empty;
				}
			}
		}

		public static readonly GPSIndicator InstanceN = new GPSIndicator(IndicatorTypes.N);
		public static readonly GPSIndicator InstanceE = new GPSIndicator(IndicatorTypes.E);
		public static readonly GPSIndicator InstanceS = new GPSIndicator(IndicatorTypes.S);
		public static readonly GPSIndicator InstanceW = new GPSIndicator(IndicatorTypes.W);

		public enum IndicatorTypes
		{
			N, S, E, W
		}
		#endregion Properties
	}
}
