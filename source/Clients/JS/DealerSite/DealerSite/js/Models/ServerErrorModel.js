/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/13/12
 * Time: 12:24 AM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Models');

SOS.Models.DefineModel('SOS.Models.ServerErrorModel',
{
	fields:
	[
		{ name: 'RequestUri', type: 'string' },
		{ name: 'Title', type: 'string' },
		{ name: 'CompleteStackTrace', type: 'string' },
		{ name: 'ErrorType', type: 'string' },
		{ name: 'ExtraInfo', type: 'string' },
		{ name: 'IsRestException', type: 'bool' },
		{ name: 'Message', type: 'string' }
	]
});