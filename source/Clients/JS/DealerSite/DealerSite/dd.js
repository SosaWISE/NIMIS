/**********************************************************************************************************************
 * @fileOverview Created by JetBrains WebStorm.
 * Date: 1/27/12
 * Time: 10:42 AM
 * @author: <a href="mailto:andres.sosa@onegreatfamily.com">Andres Sosa</a>
 * @description This file contains the definition of an instance of a KiNection.
 *
 /********************************************************************************************************************/
/**
  * Custom Drag & Drop Tutorial
  * by Jozef Sakalos, aka Saki
  * http://extjs.com/learn/Tutorial:Custom_Drag_and_Drop_Part_1
  */

// reference local blank image
Ext.BLANK_IMAGE_URL = '../extjs/resources/images/default/s.gif';

Ext.namespace('Tutorial');

// create application
Tutorial.dd = function() {

    // private space


    return {

        // public methods
        init: function() {

            var dd11 = Ext.get('dd1-item1');
            dd11.dd = new Ext.dd.DDProxy('dd1-item1', 'group');

            var dd12 = Ext.get('dd1-item2');
            dd12.dd = new Ext.dd.DDProxy('dd1-item2', 'group');

            var dd13 = Ext.get('dd1-item3');
            dd13.dd = new Ext.dd.DDProxy('dd1-item3', 'group');

        }
    };
}(); // end of app

// end of file
