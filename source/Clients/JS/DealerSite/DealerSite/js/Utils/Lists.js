/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 6/5/12
 * Time: 9:27 PM
 * To change this template use File | Settings | File Templates.
 */

Ext.ns("SOS.Utils");
Ext.define("SOS.Utils.Lists",
{
		singleton: true,

	BindOptionsToSelectEl: function (sSelector, oOptionsList)
	{
		/** Build List. */
		$.each(oOptionsList, function (index, oItem)
		{
			console.log(index, oItem);
			sSelector.append($("<option></option>")
				.attr("value", oItem.Value)
				.text(oItem.Text));
		});
	}
});

