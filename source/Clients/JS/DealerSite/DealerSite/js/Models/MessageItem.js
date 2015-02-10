/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/9/12
 * Time: 11:17 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Models');

Ext.define('SOS.Models.MessageItem',
{
	extend: 'SOS.Models.BaseModel',
	fields:
	[
		{ name: 'Title', type: 'string' },
		{ name: 'MessageBody', type: 'string' },
		{ name: 'MessageType', type: 'string' },
		{ name: 'Exception', type: 'object' },
		{ name: 'Result', type: 'object' },
		{ name: 'Code', type: 'number'}
	],
	validations:
		[{
			type: 'inclusion', field: 'MessageType',
			list: ['Success', 'Good', 'Critical', 'Failure', 'Fatal', 'Warning']
		}],

	Save: function()
	{
		this.ConsoleLog();
	},

	ConsoleLog: function()
	{
		console.log(this.Title);
		console.log(this.MessageBody);
	}
});
