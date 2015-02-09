using System.Collections.Generic;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Validation
{
	public class InputGroupMandatoryObjectValidator<T> : IInputValidator<T>
	{
		public List<IValidatedInput> InputFields { get; private set; }

		public InputGroupMandatoryObjectValidator()
		{
			this.InputFields = new List<IValidatedInput>();
		}

		public InputGroupMandatoryObjectValidator<T> AddInputField(IValidatedInput inputField)
		{
			InputFields.Add(inputField);
			return this;
		}

		public static InputGroupMandatoryObjectValidator<T> Create()
		{
			return new InputGroupMandatoryObjectValidator<T>();
		}

		#region IInputValidator<object> Members

		public bool Validate(T value)
		{
			bool result = false;

			foreach (IValidatedInput curr in this.InputFields)
			{
				if (curr.InputValue != null)
				{
					if (curr.InputValue is string)
					{
						if (StringUtility.NullIfWhiteSpace(curr.InputValue as string) != null && !StringUtility.AreEqual(curr.InputValue as string, curr.NullValueString, false))
						{
							result = true; // Valid b/c curr item value is a string that is not null and not empty
						}
					}
					else
					{
						result = true; // Valid b/c curr item value is not null (and not a string - so no need to check for emtpy string)
					}
				}
			}

			return result;
		}

		#endregion
	}
}
