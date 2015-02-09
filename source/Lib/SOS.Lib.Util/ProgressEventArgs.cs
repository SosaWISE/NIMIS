namespace SOS.Lib.Util
{
	public delegate void UpdateProgressDelegate(ProgressEventArgs args);

	public class ProgressEventArgs
	{
		#region Properties

		#region Private

		#endregion Private

		#region Public

		public int NStepsTotal { get; set; }

		public int NStepsCompleted { get; set; }

		public string Message { get; set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public ProgressEventArgs()
		{
		}

		public ProgressEventArgs(int nStepsTotal, int nStepsCompleted, string message)
		{
			NStepsTotal = nStepsTotal;
			NStepsCompleted = nStepsCompleted;
			Message = message;
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