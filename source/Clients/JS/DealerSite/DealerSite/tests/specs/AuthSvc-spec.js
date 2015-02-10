/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/11/12
 * Time: 12:03 AM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
describe("Hanshaking", function()
{
	/** Initialize. */
	var _AuthSvc;
	var sessionInformation = {};

	it("Start Session", function()
	{
		/** Initialize. */
		var isDone = false;
		var isSessionSuccess = false;

		runs(function()
		{
			_AuthSvc = SOS.ClientServices.AuthServices.Create();
			function onSuccess(result)
			{
				/** Initialize. */
				sessionInformation = result.Value;
				expect(result).toBeDefined();
				isSessionSuccess = (result.Code === 0);
				isDone = true;
			}
			function onFailure(oMessageItem)
			{
				/** Initialize. */
				var genMessage = new SOS.Models.Popups.GeneralModel(oMessageItem);
				SOS.Framework.Popups.create(genMessage).show();
				expect(false).toBeTruthy();
				isDone = true;
			}
			_AuthSvc.on(SOS.ClientServices.AuthServices.EVNT_SOSSTART_SUCCESS, onSuccess);
			_AuthSvc.on(SOS.ClientServices.AuthServices.EVNT_SOSSTART_FAILURE, onFailure);
			_AuthSvc.SosStart(SOSConfig.APP_TOKEN, this);
		});

		waitsFor(function()
		{
			return isDone;
		}
		, 3000, 'Unable to create session.  Time expired after 3 seconds.');

		runs(function()
		{
			expect(isSessionSuccess).toBeTruthy();
		});

	});

	it("Test Login user", function()
	{
		/** Initialize. */
		var isDone = false;
		var isAuthSuccessful = false;

		runs(function()
		{
			function onSuccess(result)
			{
				expect(result).toBeDefined();
				isAuthSuccessful = (result.Code === 0);
				isDone = true;
			}
			function onError(oMessageItem)
			{
				/** Initialize. */
				var genMessage = new SOS.Models.Popups.GeneralModel(oMessageItem);
				SOS.Framework.Popup.create(genMessage).show();
				expect(false).toBeTruthy();
				isDone = true;
			}
			_AuthSvc.on(SOS.ClientServices.AuthServices.EVNT_SOSAUTHN_SUCCESS, onSuccess);
			_AuthSvc.on(SOS.ClientServices.AuthServices.EVNT_SOSAUTHN_FAILURE, onError);
			_AuthSvc.SosAuthenticate(sessionInformation.SessionId, 5000, 'SosaWISE', 'Jugete!98');
		});

		/** Closure.*/
		waitsFor(function()
		{
			return isDone;
		}, 3000, "Unable to authenticate user.  Time expired after 3 seconds.");

		runs(function()
		{
			expect(isAuthSuccessful).toBeTruthy();
		});
	});
});
