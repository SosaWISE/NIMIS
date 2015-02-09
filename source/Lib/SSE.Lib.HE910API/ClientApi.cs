
namespace SSE.Lib.HE910API
{
    public class ClientApi
	{
		#region Singleton .ctor

	    private ClientApi()
	    {
	    }

	    private static volatile ClientApi _instance;
		private static readonly object _instanceSync = new object();
	    public static ClientApi Instance
	    {
		    get
		    {
			    if (_instance == null)
			    {
				    lock (_instanceSync)
				    {
					    if (_instance == null)
					    {
							_instance = new ClientApi();
					    }
				    }
			    }

				/** Return intance. */
			    return _instance;
		    }
	    }

	    #endregion Singleton .ctor

		#region Member Vairables
		#endregion Member Vairables
	}
}
