/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/10/12
 * Time: 8:11 AM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS');

SOS.Errors =
{
	Success: 0,

	/** Memember Functions. */
	Successful: function (result)
	{
		if (!Ext.isDefined(result) ||
			!Ext.isDefined(result.Code))
		{
			return false;
		}

		/** Return if successful. */
		return result.Code == SOS.Errors.Success;
	}
};