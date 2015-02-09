using System;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class DateTimeRangeValidator : IInputValidator<DateTime>
	{
		public DateTime? MinDate { get; set; }
		public DateTime? MaxDate { get; set; }

		public DateTimeRangeValidator()
		{
		}

		public static DateTimeRangeValidator Create()
		{
			return new DateTimeRangeValidator();
		}

		public static DateTimeRangeValidator Create(DateTime? minDate, DateTime? maxDate)
		{
			return new DateTimeRangeValidator()
			{
				MinDate = minDate,
				MaxDate = maxDate
			};
		}

		public bool Validate(DateTime value)
		{
			bool valid =
				(!this.MinDate.HasValue || value >= this.MinDate.Value)
				&& (!this.MaxDate.HasValue || value <= this.MaxDate.Value)
				;

			return valid;
		}
	}
	public class NullableDateTimeRangeValidator : IInputValidator<DateTime?>
	{
		public DateTime? MinDate { get; set; }
		public DateTime? MaxDate { get; set; }

		public NullableDateTimeRangeValidator()
		{
		}

		public static NullableDateTimeRangeValidator Create()
		{
			return new NullableDateTimeRangeValidator();
		}

		public static NullableDateTimeRangeValidator Create(DateTime? minDate, DateTime? maxDate)
		{
			return new NullableDateTimeRangeValidator()
			{
				MinDate = minDate,
				MaxDate = maxDate
			};
		}

		public bool Validate(DateTime? value)
		{
			bool valid =
				(!this.MinDate.HasValue || value >= this.MinDate.Value)
				&& (!this.MaxDate.HasValue || value <= this.MaxDate.Value)
				;

			return valid;
		}
	}
	public class DateStringRangeValidator : IInputValidator<string>
	{
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }

		public DateStringRangeValidator()
		{
		}

		public static DateStringRangeValidator Create(DateTime minDate, DateTime maxDate)
		{
			return new DateStringRangeValidator()
			{
				MinDate = minDate,
				MaxDate = maxDate
			};
		}

		public bool Validate(string stringValue)
		{
			bool valid = true; // Count as valid by default

			if (StringUtility.NullIfWhiteSpace(stringValue) != null)
			{
				DateTime value = DateTime.Parse(stringValue);

				valid =
					(this.MinDate == DateTime.MinValue || value >= this.MinDate)
					&& (this.MaxDate == DateTime.MinValue || value <= this.MaxDate)
					;
			}

			return valid;
		}
	}
}
