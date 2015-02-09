namespace NXS.Framework.Wpf.Validation
{
	public class IntRangeValidator : IInputValidator<int>
	{
		public int Min { get; private set; }
		public int Max { get; private set; }

		public IntRangeValidator(int min, int max)
		{
			Min = min;
			Max = max;
		}

		public static IntRangeValidator Create(int min, int max)
		{
			return new IntRangeValidator(min, max);
		}

		#region IInputValidator<int> Members

		public bool Validate(int value)
		{
			var valid =
				(Min == 0 || value >= Min)
				&& (Max == 0 || value <= Max)
				;

			return valid;
		}

		#endregion
	}
}