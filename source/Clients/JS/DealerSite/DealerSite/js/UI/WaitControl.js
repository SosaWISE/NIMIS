/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 2/10/12
 * Time: 9:25 PM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns("SOS.UI");

Ext.define("SOS.UI.WaitControl",
{
	extend: "Ext.Component",

	mixins: {
     floating: 'Ext.util.Floating'
	 },

	/**
	* @cfg {Ext.data.Store} store
	* Optional Store to which the mask is bound. The mask is displayed when a load request is issued, and
	* hidden on either load success, or load fail.
	*/

	/**
	* @cfg {String} msg
	* The text to display in a centered loading message box.
	*/
	msg : 'Loading...',
	/**
	* @cfg {String} [msgCls="x-mask-loading"]
	* The CSS class to apply to the loading message element.
	*/
	msgCls : Ext.baseCSSPrefix + 'mask-loading',

	/**
	* @cfg {Boolean} useMsg
	* Whether or not to use a loading message class or simply mask the bound element.
	*/
	useMsg: true,

	/**
	* Read-only. True if the mask is currently disabled so that it will not be displayed
	* @type Boolean
	*/
	disabled: false,

	baseCls: 'wc-frob', //Ext.baseCSSPrefix + 'mask-msg',

	renderTpl:
		[
		'<div id="wc-container" style="position:relative" class="{msgCls}">',
		'	<div id="wc-spinner">',
		'		<div class="inside"></div>',
		'	</div>',
		'	<div id="wc-center">',
		'		<h2 id="wc-txt"></h2>',
		'	</div>',
		'</div>'
		],

    // Private. The whole point is that there's a mask.
    modal: true,

    // Private. Obviously, it's floating.
    floating: {
        shadow: false
    },

    // Private. Masks are not focusable
    focusOnToFront: false,

	/**
	* Creates new LoadMask.
	* @param {String/HTMLElement/Ext.Element} el The element, element ID, or DOM node you wish to mask.
	* <p>Also, may be a {@link Ext.Component Component} who's element you wish to mask. If a Component is specified, then
	* the mask will be automatically sized upon Component resize, the message box will be kept centered,
	* and the mask only be visible when the Component is.</p>
	* @param {Object} [config] The config object
	*/
	constructor : function(el, config) {
		 var me = this;

		 // If a Component passed, bind to it.
		 if (el.isComponent) {
			 me.ownerCt = el;
			 me.bindComponent(el);
		 }
		 // Create a dumy Component encapsulating the specified Element
		 else {
			 me.ownerCt = new Ext.Component({
				 el: Ext.get(el),
				 rendered: true,
				 componentLayoutCounter: 1
			 });
			 me.container = el;
		 }
		 me.callParent([config]);

		 me.renderData = {
			 msgCls: me.msgCls
		 };
		 me.renderSelectors = {
			msgEl: 'h2',
			ctrEl: 'div#wc-container',
			spinEl: 'div#wc-spinner'
		 };
	},

	bindComponent: function(comp) {
		this.mon(comp, {
			resize: this.onComponentResize,
			scope: this
		});
	},

	afterRender: function() {
		this.setSize(220, 263);
		this.callParent(arguments);
		this.container = this.floatParent.getContentTarget();
	},

	/**
	* @private
	* Called when this LoadMask's Component is resized. The toFront method rebases and resizes the modal mask.
	*/
	onComponentResize: function() {
		var me = this;
		if (me.rendered && me.isVisible()) {
		 me.toFront();
		 me.center();
		}
	},

	onDisable : function() {
		this.callParent(arguments);
		if (this.loading) {
			this.onLoad();
		}
	},

	// private
	onBeforeLoad : function() {
	 var me = this,
		 owner = me.ownerCt || me.floatParent,
		 origin;
	 if (!this.disabled) {
		 // If the owning Component has not been layed out, defer so that the ZIndexManager
		 // gets to read its layed out size when sizing the modal mask
		 if (owner.componentLayoutCounter) {
			 Ext.Component.prototype.show.call(me);
		 } else {
			 // The code below is a 'run-once' interceptor.
			 origin = owner.afterComponentLayout;
			 owner.afterComponentLayout = function() {
				 owner.afterComponentLayout = origin;
				 origin.apply(owner, arguments);
				 if(me.loading) {
					 Ext.Component.prototype.show.call(me);
				 }
			 };
		 }
	 }
	},

	onHide: function(){
		var me = this;
		clearInterval(me._spinTimer);
		me.callParent(arguments);
		me.showOnParentShow = true;
	},

	_spinPos: 0,
	_spinTimer: null,

	onShow: function() {
		var me = this,
		 msgEl = me.msgEl;

		me.callParent(arguments);
		me.loading = true;
		if (me.useMsg) {
		 msgEl.show().update(me.msg);
		} else {
		 msgEl.parent().hide();
		}

		me._spinPos = 0;

		var xformStyle;
		if (Ext.isIE)
		{
			xformStyle = '-ms-transform';
		}
		else if (Ext.isGecko)
		{
			xformStyle = '-moz-transform';
		}
		else if (Ext.isWebkit || Ext.isChrome)
		{
			xformStyle = '-webkit-transform';
		}
		else
		{
			xformStyle = 'transform'; //does nothing
		}

		me._spinTimer = setInterval(function()
		{
			me._spinPos += 3;
			me.spinEl.setStyle(xformStyle, 'rotate(' + me._spinPos + 'deg)')
		}, 35);
	},

	afterShow: function() {
		this.callParent(arguments);
		this.center();
	},

	// private
	onLoad : function() {
		this.loading = false;
		Ext.Component.prototype.hide.call(this);
	},

	UpdateMessage: function(msg)
	{
		this.msg = msg;
		if (this.loading === true)
		{
			this.msgEl.fadeOut();
			this.msgEl.update(msg);
			this.msgEl.fadeIn();
		}
	}
});