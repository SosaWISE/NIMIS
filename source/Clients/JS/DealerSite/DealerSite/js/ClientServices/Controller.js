/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/10/12
 * Time: 8:27 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.ClientServices');

if (typeof (SOS.ClientServices.Controller) === "undefined")
{
	SOS.ClientServices.Controller =
	{
		Namespace: function()
		{
			var len1 = arguments.length,
				i = 0,
				len2,
				j,
				main,
				ns,
				sub,
				current;

			for(; i < len1; ++i)
			{
				main = arguments[i];
				ns = arguments[i].split('.');
				current = window[ns[0]];
				if (current === undefined) {
				    current = window[ns[0]] = {};
				}
				sub = ns.slice(1);
				len2 = sub.length;
				for(j = 0; j < len2; ++j) {
				    current = current[sub[j]] = current[sub[j]] || {};
				}
			}
			return current;
		}
	}
}

// wrap everything in a closure, called immediately
(function ($)
{
	// Prevent multiple inclusions
	if (SOS.ClientServices.Controller.__included_rest_client)
		return;
	SOS.ClientServices.Controller.__included_rest_client = true;

	// TODO: Double check how these work.  Are they ignoring UTC offsets?
	SOS.ClientServices.Controller.FromNetDate = function (input, throwOnInvalidInput)
	{
		var pattern = /Date\(([^)]+)\)/;
		var results = pattern.exec(input);
		if (results.length != 2)
		{
			if (!throwOnInvalidInput)
			{
				return s;
			}
			throw new Error(s + " is not .net json date.");
		}
		return new Date(parseFloat(results[1]));
	};

	SOS.ClientServices.Controller.ToNetDate = function (input)
	{
		var d = new Date(input);
		if (isNaN(d))
		{
			throw new Error("input is not a date");
		}
		// here is how we force wcf to parse as UTC and give correct local time serverside
		var date = '\/Date(' + d.getTime() + '-0000)\/';
		return date;
	};


	// This can easily be duck punched to use whatever default behavior you want.
	SOS.ClientServices.Controller.getDefaultErrorCallback = function(msg)
	{
		return function (err) { alert(msg); };
	};


	SOS.ClientServices.Controller.serviceClient = function (serviceUrl, sessionId, customErrorHandler)
	{
		this._sessionId = sessionId || null;

		this._customErrorHandler = customErrorHandler || false;

		if (typeof(SOSConfig) !== "undefined")
		{
			serviceUrl = SOSConfig.REST_BASE + serviceUrl;
		}
		else
		{
			// IE sucks, and fails for urls that start with "//", which should use http or https, depending on what the current page is loaded with.
			if (serviceUrl.substr(0,2) == "//")
			{
				serviceUrl = window.location.protocol + serviceUrl;
			}
		}

		this._serviceUrl = serviceUrl + '/';
	};

	//debugging support - maintains a list of the most recent requests
	SOS.ClientServices.Controller.serviceClient._history = [];
	SOS.ClientServices.Controller.serviceClient._requestCounter = 0;
	SOS.ClientServices.Controller.serviceClient._globalSessionId = null;

	SOS.ClientServices.Controller.serviceClient.AddRequest = function(context)
	{
		var entry =
		{
			RequestId: SOS.ClientServices.Controller.serviceClient._requestCounter++,
			RequestDate: new Date(),
			Method: context.method,
			SessionId: context.sessionId,
			HttpVerb: context.httpVerb,
			ContentType: context.contentType,
			RequestUrl: context.requestUrl,
			RequestData: context.requestData
		};

		var h = SOS.ClientServices.Controller.serviceClient._history;
		if (h.length > 20)
		{
			h.shift();
		}

		h.push(entry);

		return entry;
	};

	SOS.ClientServices.Controller.serviceClient.GetHistory = function()
	{
		return SOS.ClientServices.Controller.serviceClient._history;
	};

	SOS.ClientServices.Controller.serviceClient.prototype =
	{
		Timeout: function()
		{

		},

		OnSuccess: function(context, data, textStatus, xhr)
		{
			// Bare message IS result; don't unwrap
			if (!context.isWrappedResponse)
			{
				if (Ext.isDefined(data.Code))
				{
					if (data.Code === 0)
					{
						if (Ext.isDefined(data.SessionId))
						{
							console.log('returned sid: ' + data.SessionId);
							SOS.ClientServices.Controller.serviceClient._globalSessionId = data.SessionId;
							context.sessionId = data.SessionId;
						}
					}
					if (context.callbackScope !== null)
					{
						context.callback.call(context.callbackScope, data, textStatus, xhr);
					}
					else
					{
						context.callback(data, textStatus, xhr);
					}
				}
				return;
			}

			// Wrapped message contains top level object node (strip it off)
			for (var property in data)
			{
				if (context.callbackScope !== null)
				{
					context.callback.call(context.callbackScope, data[property], textStatus, xhr);
				}
				else
				{
					context.callback(data[property], textStatus, xhr);
				}

				break;
			}
		},

		OnError: function(context, xhr, textStatus, errorThrown)
		{
			var contentType = "";
			var responseHeader = xhr.getResponseHeader("Content-Type");
			if (responseHeader)
			{
				contentType = responseHeader.toLowerCase();
			}

			var responseData;
			var reqUrl = context.RequestUrl + context.method;

			if (context._entry)
			{
				reqUrl = context._entry.FullRequestUrl;
				responseData =
				{
					Type: contentType
				};
				context._entry.ResponseDate = new Date();
				context._entry.ResponseData = responseData;
				context._entry.ResponseStatus = textStatus;
			}

			var isXml = false;
			var isHtml = false;
			var isJson = false;

			if (contentType.indexOf("text/xml") > -1)
			{
				isXml = true;
			}
			else if (contentType.indexOf("text/html") > -1)
			{
				isHtml = true;
			}
			else if (contentType.indexOf("application/json") > -1)
			{
				isJson = true;
			}

			if (isHtml)
			{
				if (context._entry)
				{
					responseData.Data = JSON.stringify(xhr.responseText);
				}
				if (SOS && SOS.UiService)
				{
					SOS.UiService.ShowError({Title: 'Server Error', Html: xhr.responseText, RequestUri: reqUrl});
				}
			}
			else if (isJson)
			{
				try
				{
					// If the error is a RestException, let's parse it
					var jsonData = JSON.parse(xhr.responseText);
					if (context._entry)
					{
						responseData.Data = jsonData;
					}
					if (SOS && SOS.Models.Popups.GeneralModel)
					{
						var dlgOpts =
						{
							Title: 'Server Error',
							RequestUri: reqUrl
						};

						if (jsonData.IsRestException === true)
						{
							dlgOpts.RestException = jsonData;
						}
						else
						{
							dlgOpts.JsonData = jsonData;
						}

						var oMessageItem = new SOS.Models.MessageItem(
							{
								Title: dlgOpts.Title,
								MessageBody: dlgOpts.RestException.Message,
								Exception: dlgOpts.RestException,
								MessageType: 'Fatal'
							});
						//var mdlGeneral = new SOS.Models.Popups.GeneralModel(oMessageItem);
						SOS.Framework.Popup.GeneralPopup(oMessageItem);
						//SOS.UiService.ShowError(dlgOpts);
					}
				}
				catch(ex)
				{
					// Eat exception
				}
			}
			else
			{
				//for now not treating xml errors differently
				if (context._entry)
				{
					responseData.Data = JSON.stringify(xhr.responseText);
				}
				if (SOS && SOS.UiService)
				{
					SOS.UiService.ShowError({Title: 'Server Error', GenericText: xhr.responseText});
				}
			}
		},

		invoke: function (method, httpVerb, data, successCallback,
								callbackScope, isWrappedResponse, isJsonp, contentType)
		{
			callbackScope = callbackScope || null;

			if (data != null && typeof(data) == "object")
			{
				data = JSON.stringify(data);
			}

			var callContext =
			{
				method: method,
				sessionId: this._sessionId,
				httpVerb: httpVerb,
				requestUrl: this._serviceUrl,
				requestData: data,
				contentType: contentType,
				dataType: 'json',
				callbackScope: callbackScope,
				callback: successCallback,
				isWrappedResponse: isWrappedResponse
			};

			if (method.indexOf('Kin2Status') === -1)
			{
				//not tracing status calls
				callContext._entry = SOS.ClientServices.Controller.serviceClient.AddRequest(callContext);
			}

			return this.execute(callContext);
		},

		execute: function(context)
		{
			var me = this;

			var method = context.method;
			var sid = context.sessionId || SOS.ClientServices.Controller.serviceClient._globalSessionId;

			console.log('executing with sid: ' + sid);
			if (sid !== null)
			{
				if (method.indexOf('?') == -1)
				{
					method += "?SessionId=" + sid;
				}
				else
				{
					method += "&SessionId=" + sid;
				}
			}

			if (context._entry)
			{
				context._entry.FullRequestUrl = context.requestUrl + method;
			}

			var options =
			{
				url: context.requestUrl + method,
				data: context.requestData,
				type: context.httpVerb,
				processData: false,
				contentType: context.contentType,
				timeout: 3 * 60 * 1000,
				dataType: context.dataType,
				userContext: context.callbackScope,
				crossDomain: true,
				beforeSend: function(request)
				{
					//does this break jsonp, and if so, do i care?
					request.setRequestHeader("Accept", "application/json");
				},

				success: function (rdata, textStatus, xhr)
				{
					if (context._entry)
					{
						context._entry.ResponseDate = new Date();
						context._entry.ResponseData = rdata;
						context._entry.ResponseStatus = textStatus;
					}

					if (context.callback)
					{
						me.OnSuccess(context, rdata, textStatus, xhr);
					}
				},

				error: function(xhr, textStatus, errorThrown)
				{
					me.OnError(context, xhr, textStatus, errorThrown);
				}
			};

			var thexhr = $.ajax(options);

			return thexhr;
		}
	};

})(jQuery);