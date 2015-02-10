namespace RestService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestServiceImpl" in code, svc and config file together.
	public class RestServiceImpl : IRestServiceImpl
	{
		public void DoWork()
		{
		}

		#region Implementation of IRestServiceImpl

		public ResponseData Auth(RequestData oRequestData)
		{
			/** Validate input. */
			if (oRequestData != null)
			{
				/** Initialize. */
				var oData = oRequestData.Details.Split('|');
				var oResponse = new ResponseData
				                	{
				                		Name = oData[0],
				                		Age = oData[1],
				                		Exp = oData[2],
				                		Technology = oData[3]
				                	};
			return oResponse;
			}

			return null;
		}

		#endregion
	}
}
