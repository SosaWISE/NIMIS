using System.Globalization;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models
{
	public class FnsOptionItem : IFnsOptionItem
	{
		#region .ctor

		public FnsOptionItem (QL_LeadSource oSource)
		{
			Value = oSource.LeadSourceID.ToString(CultureInfo.InvariantCulture);
			Text = oSource.LeadSource;
		}

		public FnsOptionItem(QL_LeadDisposition oDisposition)
		{
			Value = oDisposition.LeadDispositionID.ToString(CultureInfo.InvariantCulture);
			Text = oDisposition.LeadDisposition;
		}

		public FnsOptionItem(CA_AppointmentType oAptType)
		{
			Value = oAptType.AppointmentTypeID;
			Text = oAptType.AppointmentType;
		}

		public FnsOptionItem(CA_ReminderDelyType oRemDelayType)
		{
			Value = oRemDelayType.ReminderDelyTypeID;
			Text = oRemDelayType.ReminderDelyType;
		}

		public FnsOptionItem(CA_ReminderMediaType oRemMediaType)
		{
			Value = oRemMediaType.ReminderMediaTypeID;
			Text = oRemMediaType.ReminderMediaType;
		}

		#endregion .ctor

		#region Implementation of IFnsOptionItem

		public string Value { get; set; }
		public string Text { get; set; }

		#endregion Implementation of IFnsOptionItem
	}
}