/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/9/12
 * Time: 10:13 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Framework');

SOS.Framework.TemplateCache = (function ()
{
	/** Initialize member Properties. */
	var _templateRepository = {};
	var _allLoaded = false;
	var _basePath = '/js/Views/';
	var _keyList = [
		'form-validation.html'
		, 'LoginView.html'
		, 'general-message.html'
		, 'leads-panel.html'
		, 'leads-tab.html'
	];
	var _requestList = [];

	/** Build requestList. */
	for(var i=0; i < _keyList.length; i++)
	{
		_requestList.push($.get(_basePath + _keyList[i]));
	}

	/** Load cache of templates. */
	$.when.apply($, _requestList)
		.done(function()
		{
			try
			{
				/** Loops through results. */
				for(var i=0; i<arguments.length; i++)
				{
					_templateRepository[_keyList[i]] = arguments[i][0];
				}
			}
			catch(ex)
			{
				new SOS.Model.MessageItem(
					{
						Title: 'Error Loading Template Cache',
						MessageBody: ex
					}).ConsoleLog();
			}
			/** Tell object it is done loading. */
			_allLoaded = true;
		})
		.fail(function()
		{
			new SOS.Models.MessageItem(
				{
					Title: 'Failed to load Template Cache',
					MessageBody: 'Something failed when loading the Templage Cache.'
				}).ConsoleLog();
		});

	/** Return class. */
	return {
		get AllLoaded()
		{
			return _allLoaded;
		},

		GetTemplate: function(key)
		{
			return _templateRepository[key];
		}
	};
})();