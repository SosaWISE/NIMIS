if($.browser.mozilla||$.browser.opera){document.removeEventListener("DOMContentLoaded",$.ready,false);document.addEventListener("DOMContentLoaded",function(){$.ready()},false)}$.event.remove(window,"load",$.ready);$.event.add( window,"load",function(){$.ready()});$.extend({includeStates:{},include:function(url,callback,dependency){if(typeof callback!='function'&&!dependency){dependency=callback;callback=null}url=url.replace('\n','');$.includeStates[url]=false;var script=document.createElement('script');script.type='text/javascript';script.onload=function(){$.includeStates[url]=true;if(callback)callback.call(script)};script.onreadystatechange=function(){if(this.readyState!="complete"&&this.readyState!="loaded")return;$.includeStates[url]=true;if(callback)callback.call(script)};script.src=url;if(dependency){if(dependency.constructor!=Array)dependency=[dependency];setTimeout(function(){var valid=true;$.each(dependency,function(k,v){if(!v()){valid=false;return false}});if(valid)document.getElementsByTagName('head')[0].appendChild(script);else setTimeout(arguments.callee,10)},10)}else document.getElementsByTagName('head')[0].appendChild(script);return function(){return $.includeStates[url]}},readyOld:$.ready,ready:function(){if($.isReady) return;imReady=true;$.each($.includeStates,function(url,state){if(!state)return imReady=false});if(imReady){$.readyOld.apply($,arguments)}else{setTimeout(arguments.callee,10)}}});
$.include('/js/superfish.js');
$.include('/js/FF-cash.js');
$.include('/js/jquery.easing.1.3.js');
$.include('/js/jquery.cycle.all.min.js');
$.include('/js/jquery.color.js');
$.include('/js/jquery.backgroundPosition.js');
$.include('/js/jquery.insetBorderEffect.js');
$.include('/js/jquery.jcarousel.js');
$.include('/js/jquery.maskedinput-1.3.js');
$(function ()
{
	if ($('#coin-slider').length) $.include('/js/coin-slider.js');
	if ($('.tabs').length) $.include('/js/tabs.js');
	if ($('.fixedtip').length || $('.clicktip').length || $('.normaltip').length) $.include('/js/jquery.atooltip.pack.js');
	if ($('#contact-form').length || $('#newsletter-alt-form').length || $('#newsletter-form').length) $.include('/js/forms.js');
	if ($('.top1').length || $('.layouts-nav li a').length) $.include('/js/scrollTop.js');
	if ($('.kwicks').length) $.include('/js/kwicks-1.5.1.pack.js');
	if ($("#thumbs").length) { $.include('/js/jquery.galleriffic.js'); }
	if ($(".lightbox-image").length) $.include('/js/jquery.prettyPhoto.js');
	if ($("#twitter").length) $.include('/js/jquery.twitter.js');
	if ($('#countdown_dashboard').length) $.include('/js/jquery.lwtCountdown-1.0.js');
	$('.top1').click(function (e) { $('html,body').animate({ scrollTop: '0px' }, 800); return false; });
	$('.layouts-nav li a').click(function () { var offset = $($(this).attr('href')).offset(); $('html,body').animate({ scrollTop: offset.top }, 800); return false });
	$("#accordion dt").click(function () { $(this).next("#accordion dd").slideToggle("slow").siblings("#accordion dd:visible").slideUp("slow"); $(this).toggleClass("active"); $(this).siblings("#accordion dt").removeClass("active"); return false });
	$(".slideDown dt").click(function () { $(this).toggleClass("active").parent(".slideDown").find("dd").slideToggle(); });
	$(".code a.code-icon").toggle(function () { $(this).find("i").text("-"); $(this).next("div.grabber").slideDown(); }, function () { $(this).find("i").text("+"); $(this).next("div.grabber").slideUp(); });
	$('.list-1 li').prepend('<span></span>').find('>a').wrap('<div></div>').hover(function () { $(this).parent().prev().animate({ height: 0, marginTop: 10, backgroundColor: '#858585' }, 100, function () { $(this).animate({ height: 19, marginTop: 0, backgroundColor: '#000' }, 100); }); }, function () { $(this).parent().prev().stop().animate({ height: 0, marginTop: 10, backgroundColor: '#858585' }, 100, function () { $(this).animate({ height: 19, marginTop: 0, backgroundColor: '#fff' }, 100) }) })
	$('header .links li a').append('<span></span>').hover(
		function()
		{
			$(this).stop().animate(
				{ backgroundPosition: '0 0', color: '#fff' }
					, { duration: 200, queue: false })
				.find('>span').animate({ backgroundColor: '#8d6f1d', marginLeft: 0, width: 0 }
					, { duration: 100, queue: false, complete: function()
					{
						$(this).animate({ backgroundColor: '#242323', marginLeft: -12, width: 23 }, 100);
					}
					});
		}
		, function() { $(this).stop().animate({ backgroundPosition: '0 -156px', color: '#CF0C0D' }, { duration: 200, queue: false }).find('>span').animate({ backgroundColor: '#8d6f1d', marginLeft: 0, width: 0 }, { duration: 100, queue: false, complete: function() { $(this).animate({ backgroundColor: '#CF0C0D', marginLeft: -12, width: 23 }, 100); } }); });
	$('.banners a').hover(function () { $(this).find('>img').stop().animate({ opacity: .5 }); }, function () { $(this).find('>img').stop().animate({ opacity: 1 }); });
});
/** Checks to see which root menu item should be selected. */
$(function ()
{
	/** Find Section. */
	function findSection()
	{
		/** Initialize. */
		var result = 'Home';
		var pathName = window.location.pathname;

		/** Check for what section. */
		if (pathName.indexOf('Home') > 0) return 'Home';
		if (pathName.indexOf('Services') > 0) return 'Services';
		if (pathName.indexOf('Technology') > 0) return 'Technology';
		if (pathName.indexOf('Support') > 0) return 'Support';
		if (pathName.indexOf('Dealers') > 0) return 'Dealers';
		if (pathName.indexOf('ContactUs') > 0) return 'ContactUs';

		return result;
	}

	/** Initialize. */
	/** Find what root location we are in. */
	var sectionName = findSection();

	switch (sectionName)
	{
		case 'Home':
			$('#rootHome').addClass('current');
			break;
		case 'Services':
			$('#rootServices').addClass('current');
			break;
		case 'Technology':
			$('#rootTechnology').addClass('current');
			break;
		case 'Support':
			$('#rootSupport').addClass('current');
			break;
		case 'Dealers':
			$('#rootDealers').addClass('current');
			break;
		case 'ContactUs':
			$('#rootContactUs').addClass('current');
			break;
		default:
			$('#rootHome').addClass('current');
			break;
	}
});
function onAfter(curr, next, opts, fwd){var $ht=$(this).height();$(this).parent().animate({ height: $ht });}