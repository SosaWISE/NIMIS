namespace NXS.Framework.Wpf.Validation
{
	public class DecimalRangeValidator : IInputValidator<decimal>
	{
		public decimal Min { get; private set; }
		public decimal Max { get; private set; }

		public DecimalRangeValidator(decimal min, decimal max)
		{
			Min = min;
			Max = max;
		}

		public static DecimalRangeValidator Create(decimal min, decimal max)
		{
			return new DecimalRangeValidator(min, max);
		}

		public bool Validate(decimal value)
		{
			return (value >= Min) && (value <= Max);
		}
	}

	public class NullableDecimalRangeValidator : IInputValidator<decimal?>
	{
		public decimal Min { get; private set; }
		public decimal Max { get; private set; }

		public NullableDecimalRangeValidator(decimal min, decimal max)
		{
			Min = min;
			Max = max;
		}

		public static NullableDecimalRangeValidator Create(decimal min, decimal max)
		{
			return new NullableDecimalRangeValidator(min, max);
		}

		public bool Validate(decimal? value)
		{
			return (value != null) && (value.Value >= Min) && (value.Value <= Max);
		}
	}
}