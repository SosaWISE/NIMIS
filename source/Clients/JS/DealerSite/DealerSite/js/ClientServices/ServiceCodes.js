/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 2/8/12
 * Time: 11:43 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.ClientServices');

Ext.define('SOS.ClientServices.ServiceCodes',
{
	singleton: true,

	Success: 0,

	LoginFailure: 10000,
	UsernameInvalid: 10010,
	PasswordInvalid: 10020,
	DealerIdInvalid: 10030,
	CookieInvalid: 10040,

	GeneralError: 20000

});
