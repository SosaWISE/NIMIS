// WCF Proxy from http://www.west-wind.com/weblog/posts/324917.aspx

// Create the RestCake namespace if it doesn't exist
// TODO: Currently, this only works if the namespace is a single token.  Any subnamespaces will break this.  Need to create a createNamespace() method.
if (typeof (RestCake) === "undefined")
	RestCake = {};

// wrap everything in a closure, called immediately
(function ($)
{
	// Prevent multiple inclusions
	if (RestCake.__included_WcfClient)
		return;
	RestCake.__included_WcfClient = true;

	// TODO: Double check how these work.  Are they ignoring UTC offsets?
	RestCake.dateFromWcf = function (input, throwOnInvalidInput)
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

	RestCake.dateToWcf = function (input)
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
	RestCake.getDefaultErrorCallback = function(msg)
	{
		return function (err) { alert(msg); };
	};


	RestCake.serviceClient = function (serviceUrl)
	{
		// IE sucks, and fails for urls that start with "//", which should use http or https, depending on what the current page is loaded with.
		if (serviceUrl.substr(0,2) == "//")
			serviceUrl = window.location.protocol + serviceUrl;

		this._serviceUrl = serviceUrl;

		this.invoke = function (method, httpVerb, data, successCallback, errorCallback, userContext, isWrappedResponse, isJsonp)
		{
			userContext = userContext || null;

			// This was buggy.  If single strings were passed in, they got wrapped in quotes.
			// I really just want to stringify object types.
			//if (data != null && data != "")
				//data = JSON.stringify(data);

			// New way (testing...)
			if (data != null && typeof(data) == "object")
				data = JSON.stringify(data);

			// http://api.jquery.com/jQuery.ajax/
			var options = {
				url: this._serviceUrl + method,
				data: data,
				type: httpVerb,
				processData: false,
				contentType: "application/json",
				timeout: 30000,
				dataType: isJsonp ? "jsonp" : "json",
				userContext: userContext,
				success: function (data, textStatus, xhr)
				{
					if (!successCallback)
						return;

					// Bare message IS result; don't unwrap
					if (!isWrappedResponse)
					{
						successCallback(data, userContext, textStatus, xhr);
						return;
					}

					// Wrapped message contains top level object node (strip it off)
					for (var property in data)
					{
						successCallback(data[property], userContext, textStatus, xhr);
						break;
					}
				},
				error: function (xhr, textStatus, errorThrown)
				{
					if (!errorCallback) return;

					var contentType = "";
					var responseHeader = xhr.getResponseHeader("Content-Type");
					if (responseHeader)
						contentType = responseHeader.toLowerCase();

					var isXml = false;
					var isHtml = false;
					var isJson = false;
					
					if (contentType.indexOf("text/xml") > -1)
						isXml = true;
					else if (contentType.indexOf("text/html") > -1)
						isHtml = true;
					else if (contentType.indexOf("application/json") > -1)
						isJson = true;

					if (isXml)
					{
						errorCallback(null, userContext, xhr, textStatus, errorThrown, "xml");
					}
					else if (isHtml)
					{
						// TODO: What?  Try to parse the error out of it??
						errorCallback(null, userContext, xhr, textStatus, errorThrown, "html");
					}
					else if (isJson)
					{
						var restException = null;
						try
						{
							// If the error is a RestException, let's parse it
							var restException = JSON.parse(xhr.responseText);
						}
						catch(ex)
						{
							// Eat exception
						}

						errorCallback(restException, userContext, xhr, textStatus, errorThrown, "json");
					}
					else
					{
						var err = xhr.responseText;
						errorCallback(err, userContext, xhr, textStatus, errorThrown, contentType);
					}
				}
			};

			if (isJsonp)
				options.jsonp = "jsoncallback";

			var thexhr = $.ajax(options);
			return thexhr;
		} // end invoke()

	}; // end class

})(jQuery);   // end of wrapping closure (called immediately)
