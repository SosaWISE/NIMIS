namespace SOS.FOS.MonitoringStationServices.Utilities.Exceptions
{
	public class CsExceptionMissingMetadata : CsExceptions
	{
		#region .ctor

		public CsExceptionMissingMetadata(long accountId, string metaDataName) : base(string.Format("For MS account of '{0}' the '{1}' is missing.", accountId, metaDataName))
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
