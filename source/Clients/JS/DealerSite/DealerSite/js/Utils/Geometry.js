/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 2/3/12
 * Time: 8:04 AM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
Ext.ns("SOS.Utils");

Ext.define("SOS.Utils.Geometry", {
	singleton: true,

	Circle: function(options)
	{
		/** Initialize. */
		var canvas = document.getElementById(options.Cmp);
		// Set the deminsions of the canvas.
		canvas.setAttribute('width', options.Width);
		canvas.setAttribute('height', options.Height);
		var context = canvas.getContext(options.Context); // i.e. "2d"
		var centerX = canvas.width / options.CenterX;  // i.e. 2
		var centerY = canvas.height / options.CenterY; // i.e. 2
		var radius = options.Radius;  // i.e. 70

		context.beginPath();
		context.arc(centerX, centerY, radius, 0, 2 * Math.PI, false);
		context.fillStyle = options.FillStyle; // i.e. "#8ED6FF"
		context.fill();
		context.lineWidth = options.LineWidth; // i.e. 5
		context.strokeStyle = options.StrokeStyle; // i.e. "black";
		context.stroke();
	}
});