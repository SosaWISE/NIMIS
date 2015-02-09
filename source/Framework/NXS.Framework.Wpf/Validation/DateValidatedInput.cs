using System;

namespace NXS.Framework.Wpf.Validation
{
	public class DateValidatedInput : ValidatedInput<DateTime>
	{
		public DateTime? DisplayDateStart
		{
			get
			{
				DateTimeRangeValidator dateTimeRangeValidator = GetDateTimeRangeValidator();
				if (dateTimeRangeValidator != null) {
					return dateTimeRangeValidator.MinDate;
				}
				else {
					return null;
				}
			}
			set
			{
				DateTimeRangeValidator dateTimeRangeValidator = GetDateTimeRangeValidator();
				if (dateTimeRangeValidator != null) {
					dateTimeRangeValidator.MinDate = value;
				}
			}
		}

		public DateTime? DisplayDateEnd
		{
			get
			{
				DateTimeRangeValidator dateTimeRangeValidator = GetDateTimeRangeValidator();
				if (dateTimeRangeValidator != null) {
					return dateTimeRangeValidator.MaxDate;
				}
				else {
					return null;
				}
			}
			set
			{
				DateTimeRangeValidator dateTimeRangeValidator = GetDateTimeRangeValidator();
				if (dateTimeRangeValidator != null) {
					dateTimeRangeValidator.MaxDate = value;
				}
			}
		}

		private DateTimeRangeValidator GetDateTimeRangeValidator()
		{
			DateTimeRangeValidator dateTimeRangevalidator = null;
			if (this.Validator is CompositeValidator<DateTime>) {

				CompositeValidator<DateTime> validatorList = (CompositeValidator<DateTime>)this.Validator;
				foreach (IInputValidator<DateTime> validator in validatorList) {
					if (validator is DateTimeRangeValidator) {
						dateTimeRangevalidator = (DateTimeRangeValidator)validator;
					}
				}
			}
			else if (this.Validator is DateTimeRangeValidator) {

				dateTimeRangevalidator = (DateTimeRangeValidator)this.Validator;
			}
			return dateTimeRangevalidator;
		}
	}
	public class NullableDateValidatedInput : ValidatedInput<DateTime?>
	{
		public DateTime? DisplayDateStart
		{
			get
			{
				NullableDateTimeRangeValidator dateTimeRangeValidator = GetNullableDateTimeRangeValidator();
				if (dateTimeRangeValidator != null) {
					return dateTimeRangeValidator.MinDate;
				}
				else {
					return null;
				}
			}
			set
			{
				NullableDateTimeRangeValidator dateTimeRangeValidator = GetNullableDateTimeRangeValidator();
				if (dateTimeRangeValidator != null) {
					dateTimeRangeValidator.MinDate = value;
				}
			}
		}

		public DateTime? DisplayDateEnd
		{
			get
			{
				NullableDateTimeRangeValidator dateTimeRangeValidator = GetNullableDateTimeRangeValidator();
				if (dateTimeRangeValidator != null) {
					return dateTimeRangeValidator.MaxDate;
				}
				else {
					return null;
				}
			}
			set
			{
				NullableDateTimeRangeValidator dateTimeRangeValidator = GetNullableDateTimeRangeValidator();
				if (dateTimeRangeValidator != null) {
					dateTimeRangeValidator.MaxDate = value;
				}
			}
		}

		private NullableDateTimeRangeValidator GetNullableDateTimeRangeValidator()
		{
			NullableDateTimeRangeValidator dateTimeRangevalidator = null;
			if (this.Validator is CompositeValidator<DateTime?>) {

				CompositeValidator<DateTime?> validatorList = (CompositeValidator<DateTime?>)this.Validator;
				foreach (IInputValidator<DateTime?> validator in validatorList) {
					if (validator is NullableDateTimeRangeValidator) {
						dateTimeRangevalidator = (NullableDateTimeRangeValidator)validator;
					}
				}
			}
			else if (this.Validator is NullableDateTimeRangeValidator) {

				dateTimeRangevalidator = (NullableDateTimeRangeValidator)this.Validator;
			}
			return dateTimeRangevalidator;
		}
	}
}
