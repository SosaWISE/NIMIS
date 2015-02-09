namespace NXS.Logic.MonitoringStations.Models
{
	public class RequestTypes
	{
		#region .ctor

		private RequestTypes(ReqType requestType)
		{
			_requestType = requestType;
		}

		#endregion .ctor

		#region Properties

		private readonly ReqType _requestType;
		public enum ReqType
		{
			A,
			C,
			D
		}

		public string Value
		{
			get
			{
				switch (_requestType)
				{
					case ReqType.A:
						return "A";
					case ReqType.C:
						return "C";
					case ReqType.D:
						return "D";
				}

				// ** Return value
				return null;
			}
		}
		#endregion Properties

		#region Methods

		public static RequestTypes A()
		{
			return new RequestTypes(ReqType.A);
		}

		public static RequestTypes C()
		{
			return new RequestTypes(ReqType.C);
		}

		public static RequestTypes D()
		{
			return new RequestTypes(ReqType.D);
		}

		#endregion Methods
	}
}