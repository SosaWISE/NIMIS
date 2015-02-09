using SOS.Data.SosCrm;
using System;

namespace SOS.FunctionalServices.Models.TicketService
{
	public class CloseTicket
	{
		public int Version { get; set; }
		public string CompletedNote { get; set; }
		public string MSConfirmation { get; set; }
		public string DealerConfirmation { get; set; }

		public CloseTicket() { }

		public void ToDb(TS_ServiceTicket item)
		{
			SOS.Data.VersionException.CheckVersions(item.Version, this.Version);
			item.Version++; // increment version

			if (item.CompletedOn.HasValue)
				throw new Exception("Ticket is already closed");
			item.CompletedOn = DateTime.UtcNow;

			item.CompletedNote = this.CompletedNote;
			item.MSConfirmation = this.MSConfirmation;
			item.DealerConfirmation = this.DealerConfirmation;
		}
	}
}
