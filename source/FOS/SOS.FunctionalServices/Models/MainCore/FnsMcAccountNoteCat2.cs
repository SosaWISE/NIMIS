using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.MainCore;

namespace SOS.FunctionalServices.Models.MainCore
{
	public class FnsMcAccountNoteCat2 : IFnsMcAccountNoteCat2
	{
		#region ctor

		public FnsMcAccountNoteCat2(MC_AccountNoteCat2 cat2)
		{
			NoteCategory2ID = cat2.NoteCategory2ID;
			NoteCategory1Id = cat2.NoteCategory1Id;
			Category = cat2.Category;
			Description = cat2.Description;
			CreatedBy = cat2.CreatedBy;
			CreatedOn = cat2.CreatedOn;
		}

		#endregion ctor

		#region Properties
		public int NoteCategory2ID { get; private set; }
		public int NoteCategory1Id { get; private set; }
		public string Category { get; private set; }
		public string Description { get; private set; }
		public string CreatedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }
		#endregion Properties
	}
}
