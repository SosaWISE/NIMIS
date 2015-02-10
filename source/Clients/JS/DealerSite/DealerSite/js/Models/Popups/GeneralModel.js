/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/12/12
 * Time: 11:24 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Models.Popups');

Ext.define('SOS.Models.Popups.GeneralModel',
{
	extend: 'SOS.Models.Popups.Base',

	fields:
	[
		{ name: 'PopupType', type: 'string', defaultValue: 'Success' },
		{ name: 'Code', type: 'number', defaultValue: 0 }
	],
	validations:
	[{
		type: 'inclusion', field: 'PopupType',
		list: ['Success', 'Good', 'Critical', 'Failure', 'Fatal', 'Warning', 'Info']
	}],

	/**
	 *
	 * @param oMessageItem
	 * @param messageBody
	 * @param popupType
	 * @param code
	 */
	constructor: function(oMessageItem, messageBody, popupType, code)
	{
		/** Call parent. */
		this.callParent();

		/** Initialize. */
		var oFields = this.data;
		oFields.data = {};
		//oFields.baseCls = 'popup-wrapper';
		oFields.styleHtmlContent = true;
		oFields.styleHtmlCls = 'popup-wrapper';
		oFields.width = 'auto';
		oFields.height = 'auto';
		oFields.PopupType = (popupType !== undefined && popupType !== null)
			? popupType
			: oFields.PopupType;
		oFields.Code = (code !== undefined && code !== null)
			? code
			: oFields.Code;
		oFields.events =
			[
				{
					id: 'gen-close-x',
					event: 'click',
					fn: function (me)
					{
						me.close();
					}
				}
				,{
					id: 'gen-close',
					event: 'click',
					fn: function (me)
					{
						me.close();
					}
				}
			];

		/** Validate arguments. */
		if (oMessageItem === undefined || oMessageItem === null)
			throw 'No message or title passed.  Argument oMessageItem is required.';

		/** Create Data to pass to the base. */
		// Check the first argument to be of type MessageItem.
		if (typeof oMessageItem !== 'string')
		{
			/** Create data to pass to the base. */
			switch (oMessageItem.self.getName())
			{
				case 'SOS.Models.MessageItem':
					oFields.Title = oMessageItem.data.Title;
					oFields.MessageBody = oMessageItem.data.MessageBody;
					oFields.PopupType = oMessageItem.data.MessageType;
					oFields.MessageType = oMessageItem.data.MessageType;
					if (oMessageItem.data.Code > 0)
					{
						oFields.Code = oMessageItem.data.Code;
					}
					oFields.data.Button = 'btn-close';
					break;
				default:
					throw Ext.String.format("Invalid model.  Model '{0}' not handled in the constructor of GeneralModel"
						, oMessageItem.self.getName());
					break;
			}
		}
		else
		{
			/** Validate second argument. */
			if (messageBody === undefined || messageBody === null)
				throw 'A messageBody is required.';

			/** Create data to pass to the base. */
			oFields.data.Title = oMessageItem;
			oFields.data.MessageBody = messageBody;
			oFields.data.PopupType = popupType;
			if (oFields.Code !== undefined)
				oFields.data.Code = oFields.Code;

		}

		/** Create your template. */
		oFields.tpl = SOS.UI.Services.GetTemplate('general-message.html');
	}
});