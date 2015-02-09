using SubSonic;
using AR = NXS.Data.Letters.LD_Field;
using ARCollection = NXS.Data.Letters.LD_FieldCollection;
using ARController = NXS.Data.Letters.LD_FieldController;

namespace NXS.Data.Letters.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class LD_FieldControllerExtensions
	{
		public static ARCollection LoadAll(this ARController controller)
		{
			Query qry = AR.Query();
			return controller.LoadCollection(qry);
		}
		public static ARCollection LoadAllForTemplate(this ARController controller, int templateID)
		{
			SqlQuery qry = Select.AllColumnsFrom<AR>()
				.Where(AR.Columns.TemplateID).IsEqualTo(templateID);

			return controller.LoadCollection(qry);
		}
	}
}
