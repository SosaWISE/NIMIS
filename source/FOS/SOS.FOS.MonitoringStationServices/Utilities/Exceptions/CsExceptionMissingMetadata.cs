namespace SOS.FOS.MonitoringStationServices.Utilities.Exceptions
{
	public class CsExceptionMissingMetadata : CsExceptions
	{
		#region .ctor

		public CsExceptionMissingMetadata(long accountId, string metaDataName, string note = null) : base(string.Format("NMS Message for MS account '{0}' the '{1}' is missing.  {2}", accountId, metaDataName, note))
		{
			AccountID = accountId;
			MetadataName = metaDataName;
		}

		#endregion .ctor

		#region Properties
		public long AccountID { get; private set; }
		public string MetadataName { get; private set; }
		#endregion Properties

	}
}
