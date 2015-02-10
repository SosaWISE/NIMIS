/**
 * Created with JetBrains PhpStorm.
 * User: Andres Sosa
 * Date: 7/1/12
 * Time: 9:21 AM
 * To change this template use File | Settings | File Templates.
 */

/** Define namespaces. */
if (typeof SOS === 'undefined') { SOS = {}; }


/** Create Static AppService. */
SOS.AppService = (function()
{
	/** Private member proprieties. */
//	var _inDevMode = true;

	/** Return singleton class. */
	return {
		/***************************************************************************************************************
		 * Member Properties.
		 **************************************************************************************************************/
//		get InDevMode() { return _inDevMode; }

		/***************************************************************************************************************
		 *  Member functions.
		 **************************************************************************************************************/
		/***************************************************************************************************************
		 * This function will set up the download brochure links to ask the user for information.
		 * @constructor */
		InitDownloadBrochure: function()
		{
			/** Initialize. */
			var oLinkEl = $("#lnkBrochure");
			var oSignupButtonEl = $(".signup_button");

			/** Bind click event. */
			oLinkEl.click(function ()
			{
				$("div.newLeadModalDlg").show();
			});

			oSignupButtonEl.click(function ()
			{
				$("div.newLeadModalDlg2").show();
			});
		}
		, InitRepAndDealerBrochure: function ()
		{
			/** Initialize. */
			var oSalesBrochureEl = $("#linkSalesBrochure");
			var oDealrBrochureEl = $("#linkDealrBrochure");
			var oDealerTypeIdEl = $("#dealerTypeId");

			oSalesBrochureEl.click(function() {
				oDealerTypeIdEl.val("salesRep");
				$("div.newDealerModalDlg").show();
			});

			oDealrBrochureEl.click(function() {
				oDealerTypeIdEl.val("dealer");
				$("div.newDealerModalDlg").show();
			});
		}
	};
})();