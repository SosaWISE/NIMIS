namespace NSE.Lib.Abara
{
	public class InqueryResponse
	{

		#region Properteis

		[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
		public enum IqResultValue
		{
			[System.Runtime.Serialization.EnumMemberAttribute]
			NotSet = 0,
			[System.Runtime.Serialization.EnumMemberAttribute]
			Scored = 1,
			[System.Runtime.Serialization.EnumMemberAttribute]
			NotScored = 2,
			[System.Runtime.Serialization.EnumMemberAttribute]
			Declined = 3,
			[System.Runtime.Serialization.EnumMemberAttribute]
			NoHit = 4,
			[System.Runtime.Serialization.EnumMemberAttribute]
			// ReSharper disable InconsistentNaming
			UNDEFINED = 5,
			[System.Runtime.Serialization.EnumMemberAttribute]
			NEVER_QUERY = 6
			// ReSharper restore InconsistentNaming
		}

		[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
		public enum HandshakeResult
		{

			// ReSharper disable InconsistentNaming
			[System.Runtime.Serialization.EnumMemberAttribute]
			SUCCESS = 0,
			[System.Runtime.Serialization.EnumMemberAttribute]
			FAILURE = 1,
			[System.Runtime.Serialization.EnumMemberAttribute]
			UNAUTHORIZED = 2,
			[System.Runtime.Serialization.EnumMemberAttribute]
			ERROR = 3,
			[System.Runtime.Serialization.EnumMemberAttribute]
			UNDEFINED = 4
			// ReSharper restore InconsistentNaming
		}

		#endregion Properties

		#region Fields

		public int Score { get; set; }
		public bool IsScored { get; set; }
		public bool IsHit { get; set; }
		public string ReportHtml { get; set; }
		public string ReportXml { get; set; }
		public string Report { get; set; }
		public HandshakeResult Result { get; set; }
		public string ErrorMessage { get; set; }
		public int? ReportID { get; set; }
		public System.Guid ReportGuid { get; set; }
		public string HitStatus { get; set; }
		public IqResultValue ReportOutcome { get; set; }
		public string DecisionCode { get; set; }
		public string DecisionText { get; set; }

		#endregion Fields

		#region Methods

		internal static string GetHandshakeResultString(HandshakeResult eResult)
		{
			switch (eResult)
			{
				case HandshakeResult.ERROR:
					return "ERROR";
				case HandshakeResult.FAILURE:
					return "FAILURE";
				case HandshakeResult.SUCCESS:
					return "SUCCESS";
				case HandshakeResult.UNAUTHORIZED:
					return "UNAUTHORIZED";
				default:
					return "[UNDEFINED]";
			}
		}

		#endregion Methods
	}
}
