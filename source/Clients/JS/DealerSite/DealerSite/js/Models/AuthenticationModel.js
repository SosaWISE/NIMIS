/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/16/12
 * Time: 10:10 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Models');

Ext.define('SOS.Models.AuthenticationModel',
{
	extend: 'SOS.Models.BaseModel',

	fields:
	[
		{ name: 'SessionId', type: 'number'},
		{ name: 'DealerId', type: 'number'},
		{ name: 'Username', type: 'string'},
		{ name: 'Password', type: 'string'}
	]/*,

	validations:
	[{

	}]*/

});