(function($){$.fn.superfish=function(op){var sf=$.fn.superfish,c=sf.c,$arrow = $(['<span class="',c.arrowClass,'"></span>'].join('')),over=function(){var $$=$(this),menu=getMenu($$);clearTimeout(menu.sfTimer);$$.showSuperfishUl().siblings().hideSuperfishUl()},out=function(){var $$=$(this),menu=getMenu($$),o=sf.op;clearTimeout(menu.sfTimer);menu.sfTimer=setTimeout(function(){o.retainPath=($.inArray($$[0],o.$path)>-1);$$.hideSuperfishUl();if(o.$path.length&&$$.parents(['li.',o.hoverClass].join('')).length<1){over.call(o.$path)}},o.delay)},getMenu=function($menu){var menu=$menu.parents(['ul.',c.menuClass,':first'].join(''))[0];sf.op=sf.o[menu.serial];return menu},addArrow=function($a){$a.addClass(c.anchorClass).append($arrow.clone())};return this.each(function(){var s=this.serial=sf.o.length;var o=$.extend({},sf.defaults,op);o.$path=$('li.'+o.pathClass,this).slice(0,o.pathLevels).each(function(){$(this).addClass([o.hoverClass,c.bcClass].join(' ')).filter('li:has(ul)').removeClass(o.pathClass)});sf.o[s]=sf.op=o;$('li:has(ul)',this)[($.fn.hoverIntent&&!o.disableHI)?'hoverIntent':'hover'](over,out).each(function(){if(o.autoArrows)addArrow($('>a:first-child',this))});var $a=$('a',this);$a.each(function(i){var $li=$a.eq(i).parents('li');$a.eq(i)})}).each(function(){var menuClasses=[c.menuClass];$(this).addClass(menuClasses.join(' '))})};var sf=$.fn.superfish;sf.o=[];sf.op={};sf.c={bcClass:'sf-breadcrumb',menuClass:'sf-js-enabled',anchorClass:'sf-with-ul',arrowClass:'sf-sub-indicator',shadowClass:'sf-shadow'};sf.defaults={hoverClass:'sfHover',pathClass:'overideThisToUse',pathLevels:1,delay:800,animation:{height:'show',opacity:'show'},speed:'normal',autoArrows:false,disableHI:false};$.fn.extend({hideSuperfishUl:function(){var o=sf.op,not=(o.retainPath===true)?o.$path:'';o.retainPath=false;var $ul=$(['li.',o.hoverClass].join(''),this).add(this).not(not).removeClass(o.hoverClass).find('>ul').hide();
if($(this).parent().parent().is('li')){
					$(this).find('a').stop().animate({backgroundPosition:'-141px 0'})
				}
				else {
					$(this).find('>a').stop().animate({backgroundPosition:'0 -94px'})
					$(this).find('>ul a').css({backgroundPosition:'-141px 0'})
				};return this},showSuperfishUl:function(){var o=sf.op,$ul=this.addClass(o.hoverClass).find('>ul:hidden');$ul.animate(o.animation,o.speed);return this}})})(jQuery);
//$(function(){
//	$('.sf-menu').superfish().find('li').not('.current')
//	.hover(
//		function(){
//			$(this).find('>a').stop().animate({backgroundPosition:'0 0'})
//		},
//		function(){
//			if($(this).hasClass('sfHover'))return
//			else{
//				if($(this).parent().parent().is('li')){
//					$(this).find('>a').stop().animate({backgroundPosition:'-141px 0'})
//				}
//				else {$(this).find('>a').stop().animate({backgroundPosition:'0 -94px'})}
//			}
//		}
//	)
//});