/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/9/12
 * Time: 10:18 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns('SOS.Models');
Ext.ns('SOS.Models.Popups');

Ext.define('SOS.Models.Popups.Base',
{
	extend: 'Ext.data.Model',
	fields:
	[
//		{ name: 'id', type: 'string' },
		{ name: 'ui', type: 'string', defaultValue: 'none' },
//		{ name: 'plain', type: 'bool', defaultValue: true },
		{ name: 'modal', type: 'bool', defaultValue: true },
//		{ name: 'preventHeader', type: 'bool', defaultValue: true },
		{ name: 'minimizable', type: 'bool', defaultValue: false },
//		{ name: 'collapsible', type: 'bool', defaultValue: false },
		{ name: 'closable', type: 'bool', defaultValue: true },
		{ name: 'draggable', type: 'bool', defaultValue: false },
//		{ name: 'resizable', type: 'bool', defaultValue: false },
		{ name: 'shadow', type: 'bool', defaultValue: false },
		{ name: 'border', type: 'bool', defaultValue: false },
//		{ name: 'height', type: 'int' },
//		{ name: 'width', type: 'int' },
//		{ name: 'bodyStyle', type: 'string' },
//		{ name: 'string', type: 'string' },
//		{ name: 'layout', type: 'auto' },
		{ name: 'tpl', type: 'auto' },
//		{ name: 'html', type: 'auto' },
//		{ name: 'data', type: 'auto' },
		{ name: 'events', type: 'auto' }
	]
});