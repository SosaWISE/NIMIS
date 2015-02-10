/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/9/12
 * Time: 10:48 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Models');

SOS.Models.DefineModel('SOS.Models.User',
{
	extend: 'SOS.Models.BaseModel',
	fields:
	[
		{ name: 'DealerUserID', type: 'int' },
		{ name: 'DealerUserTypeId', type: 'int' },
		{ name: 'DealerId', type: 'int' },
		{ name: 'UserID', type: 'string' },
		{ name: 'FullName', type: 'string' },
		{ name: 'Email', type: 'string' },
		{ name: 'PhoneWork', type: 'string' },
		{ name: 'PhoneCell', type: 'string' },
		{ name: 'ADUsername', type: 'string' },
		{ name: 'Username', type: 'string' },
		{ name: 'Password', type: 'string' },
		{ name: 'LastLoginOn', type: 'string' }
	]
});
