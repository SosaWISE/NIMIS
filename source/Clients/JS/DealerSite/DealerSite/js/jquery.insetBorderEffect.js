(function($) {
	
	$.fn.insetBorder = function(options) {
		
		if ((options!=undefined) && (options.inset!=undefined))
		{
			if (options.insetleft==undefined) { options.insetleft = options.inset; }
			if (options.insetright==undefined) { options.insetright = options.inset; }
			if (options.insettop==undefined) { options.insettop = options.inset; }
			if (options.insetbottom==undefined) { options.insetbottom = options.inset; }
		}
		
		// defaults
		options = $.extend({
			speed : 250,
			insetleft : 10,
			insetright : 10,
			insettop : 10,
			insetbottom : 10,
			borderColor : '#e4e4e4',
			borderType: "solid",
			outerClass : "ibe_outer",
			innerClass : "ibe_inner"
		}, options);
		
		// run plugin on entire jQuery set
		return this.each(function(i) {
				
      var			
  			$el = $(this),
  			ibe_height = $el.outerHeight(),
			  ibe_width = $el.outerWidth();
			
  		var
			  wrapper = $("<div />", {
  			  "class": options.outerClass,
  			  css  : {
    				width: ibe_width,
    				overflow: "hidden",
    				top: 0,
    				left: 0,
    				position: "relative"
  				},
    		  mouseenter: function() {
    				  $el
    					 .find('>div')
    					 .animate({
    					   top:-options.insettop, 
    					   left:-options.insetleft, 
    					   height: ibe_height, 
    					   width: ibe_width
    					 }, {
    					   duration: options.speed, 
    					   queue: false,
    					   complete: function() {
    					   		
    					   }
    					 });
  					 
  				  // on mouseleave
  					},
  					mouseleave: function() {
  					  
  					  $el
  					     .find('>div')
  						   .animate({
    					   top:0, 
    					   left:0, 
    					   height: ibe_height - (options.insettop + options.insetbottom), 
    					   width: ibe_width - (options.insetleft + options.insetright)
    					 }, {
    					   duration: options.speed, 
    					   queue: false,
    					   complete: function() {}});
  						  
  					} 
  				}),
			   
			 append = $("<div />", {
  			  "class": options.innerClass,
  			  css  : {
    				height: (ibe_height - (options.insettop + options.insetbottom)) + "px",
    				width: (ibe_width - (options.insetleft + options.insetright)) + "px",
    				border: options.insetleft+"px "+options.borderType+" "+options.borderColor,
    				position: "absolute",
    				top: 0,
    				left: 0
			    }
			   });

			$el.wrap(wrapper).append(append);
			 
		});
				
	};
	
})(jQuery);
$(window).load(function(){
	$('.hover').insetBorder({
        inset: 3
      });
})