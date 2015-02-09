using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.MainCore;

namespace SOS.FunctionalServices.Models.MainCore
{
	public class FnsMcAccountNoteCat1 : IFnsMcAccountNoteCat1
	{
		#region .ctor

		public FnsMcAccountNoteCat1(MC_AccountNoteCat1 cat1)
		{
			NoteCategory1ID = cat1.NoteCategory1ID;
			Category = cat1.Category;
			NoteTypeId = cat1.NoteTypeId;
			Description = cat1.Description;
			CreatedBy = cat1.CreatedBy;
			CreatedOn = cat1.CreatedOn;
		}

		#endregion .ctor

		#region Properties
		public int NoteCategory1ID { get; private set; }
		public string Category { get; private set; }
		public string NoteTypeId { get; private set; }
		public string Description { get; private set; }
		public string CreatedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }

		#endregion Properties
	}
}
