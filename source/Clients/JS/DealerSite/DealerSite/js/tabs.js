$(function(){
	$('.tabs').each(function(){$(this).find('.tab-content').not($(this).find('ul.nav .selected a').attr('href')).hide();
	$(this).find('ul.nav a').click(function(){
		if($(this).parent().hasClass('selected')){return false}
		else{
			$(this).parent().addClass('selected').siblings().removeClass('selected');$(this).parents('.tabs').height($('ul.nav').outerHeight()+$($(this).attr('href')).outerHeight()).find('.tab-content').hide();$($(this).attr('href')).fadeIn(300);$(this).parents('.tabs').height('auto');return false}
		})
	})
})	