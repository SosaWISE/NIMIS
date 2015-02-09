
namespace NXS.Framework.Wpf.Validation
{
	public class ShortRangeValidator : IInputValidator<short>
    {
		public ShortRangeValidator(short min, short max)
		{
			this.Min = min;
			this.Max = max;
		}

        public short Min { get; private set; }
        public short Max { get; private set; }

		#region IInputValidator<short> Members

		public bool Validate(short value)
        {
            var valid =
                (Min == 0 || value >= Min)
                && (Max == 0 || value <= Max)
                ;

            return valid;
        }

        #endregion

		public static ShortRangeValidator Create(short min, short max)
		{
			return new ShortRangeValidator(min, max);
		}
    }
}
