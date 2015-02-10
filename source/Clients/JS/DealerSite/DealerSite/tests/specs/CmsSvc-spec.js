/**
 * Created by JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 3/1/12
 * Time: 10:09 PM
 * To change this template use File | Settings | File Templates.
 */

describe("Create basic lead", function()
{
	/** Initialize. */
	var isDone = false;
	var isSuccessful = false;

	it("QlLeadBasicCreate: No arguments.", function()
	{
		runs(function()
		{
			function onSuccess(response)
			{
				console.log(response.Value);
				isSuccessful = true;
				isDone = true;
			}
			function onFailure(oMsgItem)
			{
				console.log(oMsgItem.data.MessageBody, oMsgItem.data);
				isSuccessful = false;
				isDone = true;
			}
			/** Initialize. */
			var oSvc = new SOS.ClientServices.CmsServices();

			try
			{
				oSvc.on(SOS.ClientServices.CmsServices.EVNT_CBSCLEAD_SUCCESS, onSuccess);
				oSvc.on(SOS.ClientServices.CmsServices.EVNT_CBSCLEAD_FAILURE, onFailure);
				oSvc.QlLeadBasicCreate(5000, "Glenn", "Beck"
					, "1184 N 840 E"
					, "OREM"
					, "UT"
					, "84097"
					, "andres@wisearchitect.com"
					, "801 822-0987");
			}
			catch(oEx)
			{
				expect(oEx).toBeDefined();
				expect(oEx === SOS.ClientServices.CmsServices.EXP_MSG_DEALERID_IS_MISSING).toBeTruthy();
				isDone = true;
			}
		});

		waitsFor(function()
		{
			return isDone;
		}, 3000, "Failed to pass expired after 3 seconds.");

		runs(function()
		{
			expect(isSuccessful).toBeTruthy();
		});

	});
});