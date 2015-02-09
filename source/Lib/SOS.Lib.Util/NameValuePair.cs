namespace SOS.Lib.Util
{
	public class NameValuePair<TNameT, TValueT>
	{
		#region Properties

		#region Private

		#endregion Private

		#region Public

		public TNameT Name { get; set; }

		public TValueT Value { get; set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public NameValuePair()
		{
		}

		public NameValuePair(TNameT name, TValueT value)
		{
			Name = name;
			Value = value;
		}

		#endregion Constructors

		#region Methods

		#region Private

		#endregion Private

		#region Public

		#endregion Public

		#endregion Methods
	}
}