
namespace NXS.Framework.Wpf.Validation
{
	public class LongRangeValidator : IInputValidator<long>
	{
		public decimal Min { get; private set; }
		public decimal Max { get; private set; }

		public LongRangeValidator(long min, long max)
		{
			Min = min;
			Max = max;
		}

		public static LongRangeValidator Create(long min, long max)
		{
			return new LongRangeValidator(min, max);
		}

		public bool Validate(long value)
		{
			return (value >= Min) && (value <= Max);
		}
	}

	public class NullableLongRangeValidator : IInputValidator<long?>
	{
		public decimal Min { get; private set; }
		public decimal Max { get; private set; }

		public NullableLongRangeValidator(long min, long max)
		{
			Min = min;
			Max = max;
		}

		public static NullableLongRangeValidator Create(long min, long max)
		{
			return new NullableLongRangeValidator(min, max);
		}

		public bool Validate(long? value)
		{
			if (value == null)
			{
				return true;
			}

			return (value.Value >= Min) && (value.Value <= Max);
			//return (value != null) && (value.Value >= Min) && (value.Value <= Max);
		}
	}
}
