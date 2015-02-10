(function ($) {
	$.fn.extend({
		forms: function (opt) {
			if (opt === undefined)
				opt = {};
			this.each(function () {
				var th = $(this),
				    data = th.data('forms'),
				    _ = {
				    	errorCl: 'error',
				    	emptyCl: 'empty',
				    	invalidCl: 'invalid',
				    	successCl: 'success',
				    	failureCl: 'failure',
				    	successShow: '10000',
				    	mailHandlerURL: opt.controller || '/ContactUs/SubmitForm',
				    	ownerEmail: 'andres@wisearchitects.com',
				    	stripHTML: true,
				    	smtpMailServer: 'localhost',
				    	targets: 'input,textarea',
				    	controls: 'a[data-type=reset],a[data-type=submit]',
				    	validate: true,
				    	rx: {
				    		".name": { rx: /^[a-zA-Z'][a-zA-Z-' ]+[a-zA-Z']?$/, target: 'input' },
				    		".lastname": { rx: /^[a-zA-Z'][a-zA-Z-' ]+[a-zA-Z']?$/, target: 'input' },
				    		".address": { rx: /^.{1,50}$/, target: 'input' },
				    		".city": { rx: /^[a-zA-Z'][a-zA-Z-' ]+[a-zA-Z']?$/, target: 'input' },
				    		".email": { rx: /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i, target: 'input' },
				    		".state": { rx: /^[a-zA-Z'][a-zA-Z-' ]+[a-zA-Z']?$/, target: 'input' },
				    		".postal": { rx: /^\d{5}$|^\d{5}-\d{4}$/, target: 'input' },
				    		".phone": { rx: /^\+?(\d[\d\-\+\(\) ]{5,}\d$)/, target: 'input' },
				    		".fax": { rx: /^\+?(\d[\d\-\+\(\) ]{5,}\d$)/, target: 'input' },
				    		".message": { rx: /.{20}/, target: 'textarea' }
				    	},
				    	preFu: function () {
				    		_.labels.each(function () {
				    			var label = $(this),
				    			    inp = $(_.targets, this),
				    			    defVal = inp.val(),
				    			    trueVal = (function () {
				    			    	var tmp = inp.is('input') ? (tmp = label.html().match(/value=['"](.+?)['"].+/), !!tmp && !!tmp[1] && tmp[1]) : inp.html()
				    			    	return defVal == '' ? defVal : tmp
				    			    })();
				    			trueVal != defVal
					    			&& inp.val(defVal = trueVal || defVal);
				    			label.data({ defVal: defVal });
				    			inp
					    			.bind('focus', function () {
					    				inp.val() == defVal
						    				&& (inp.val(''), _.hideEmptyFu(label), label.removeClass(_.invalidCl))
					    			})
					    			.bind('blur', function () {
					    				!inp.val()
					    					? inp.val(defVal)
					    					: (_.isValid(label)
					    						? _.showErrorFu(label)
					    						: _.hideErrorFu(label)),
					    				(_.isEmpty(label)
					    					? _.showEmptyFu(label)
					    					: _.hideEmptyFu(label));
					    			})
					    			.bind('keyup', function () {
					    				label.hasClass(_.invalidCl)
						    				&& _.isValid(label)
					    					? _.showErrorFu(label)
					    					: _.hideErrorFu(label);
					    			});
				    			label.find('.' + _.errorCl + ',.' + _.emptyCl).css({ display: 'block' }).hide();
				    		});
				    		_.success = $('.' + _.successCl, _.form).hide();
				    		_.failure = $('.' + _.failureCl, _.form).hide();
				    	},
				    	isValid: function (el) {
				    		var ret = true,
				    		    empt = _.isEmpty(el);
				    		if (empt)
				    			ret = false,
				    			el.addClass(_.invalidCl);
				    		else
				    			$.each(_.rx, function (k, d) {
				    				if (el.is(k))
				    					d.rx.test(el.find(d.target).val())
				    						? (el.removeClass(_.invalidCl), ret = false)
				    						: el.addClass(_.invalidCl);
				    			})
				    		return ret
				    	},
				    	isEmpty: function (el) {
				    		var tmp
				    		return (tmp = el.find(_.targets).val()) == '' || tmp == el.data('defVal')
				    	},
				    	validateFu: function () {
				    		_.labels.each(function () {
				    			var th = $(this);
				    			_.isEmpty(th)
				    				? _.showEmptyFu(th)
				    				: _.hideEmptyFu(th);
				    			_.isValid(th)
				    				? _.showErrorFu(th)
				    				: _.hideErrorFu(th);
				    		})
				    	},
				    	submitFu: function () {
				    		_.validateFu();
				    		if (!_.form.has('.' + _.invalidCl).length)
				    			$.ajax({
				    				type: "GET",
				    				dataType: "json",
				    				contentType: 'application/json; charset=utf-8',
				    				url: _.mailHandlerURL,
				    				data: {
				    					name: $('.name input', _.form).val() || 'nope',
				    					lastname: $('.lastname input', _.form).val() || 'nope',
				    					address: $('.address input', _.form).val() || 'nope',
				    					city: $('.city input', _.form).val() || 'nope',
				    					postal: $('.postal input', _.form).val() || 'nope',
				    					email: $('.email input', _.form).val() || 'nope',
				    					phone: $('.phone input', _.form).val() || 'nope',
				    					fax: $('.fax input', _.form).val() || 'nope',
				    					state: $('.state input', _.form).val() || 'nope',
				    					sourceId: $('#sourceId', _.form).val() || '11',
				    					message: $('.message textarea', _.form).val() || 'nope',
				    					owner_email: _.ownerEmail,
				    					dealerTypeId: $('#dealerTypeId', _.form).val() || 'nope',
				    					stripHTML: _.stripHTML
				    				},
				    				success: function (oData, szTextStatus, jqXHR) {
				    					if (oData.Code !== 0) {
				    						//console.log(oData);
				    						_.showFail(oData);
				    					} else {
				    						switch (oData.Message) {
				    							case "Success on NewLeadSubmitForm":
				    								_.showFu(false, true);
				    								_.showDownloadBrochure();
				    								dataLayer = [{
				    									'pageCategory': 'DownloadBrochure',
				    									'visitorType': 'high-value',
				    									'price': 2.02
				    								}];
				    								break;
				    							case "Successful on NewDealerLeadSubmitForm":
				    								_.showFu(false, true);
				    								var oDealerTypeIdEl = $("#dealerTypeId");
				    								_.showDownloadDealerBrochure(oDealerTypeIdEl.val());
				    								dataLayer = [{
				    									'pageCategory': 'DownloadNewDealerBrochure',
				    									'visitorType': 'high-value',
				    									'price': 2.02
				    								}];

				    								break;
				    							default:
				    								_.showFu();
				    								break;
				    						}
				    					}
				    				},
				    				error: function (jqXhr, msg, exception) {
				    					//console.log(jqXhr, msg, exception);
				    					alert("Error:" + msg + "| Exception: " + exception);
				    				}
				    			});
				    	},
				    	showDownloadBrochure: function () {
				    		/** Initialize. */
				    		var oDownloadModalDlg = $("div.downloadModalDlg");

				    		/** Show the right divs. */
				    		if (oDownloadModalDlg.length > 0) oDownloadModalDlg.show();
				    	},
				    	showDownloadDealerBrochure: function (dealerTypeId) {
				    		/** Initialize. */
				    		var oDownloadModalDlg = $("div.downloadModalDlg");
				    		var oAnchorEl = $("#downloadBrochureAnchor");

				    		/** Check the dealerTypeId. */
				    		if (dealerTypeId === "salesRep")
				    			oAnchorEl.attr("href", "/DownloadBrochure.ashx?brochureName=SalesOpp");
				    		else
				    			oAnchorEl.attr("href", "/DownloadBrochure.ashx?brochureName=DealerOpp");

				    		/** Show the right divs. */
				    		if (oDownloadModalDlg.length > 0) oDownloadModalDlg.show();
				    	},
				    	showFu: function (dontReset, dontSlideUp) {
				    		_.success.slideDown(function () {
				    			setTimeout(function () {
				    				if (!dontSlideUp) _.success.slideUp();
				    				if (!dontReset) _.form.trigger('reset');
				    			}, _.successShow);
				    		});
				    	},
				    	showFail: function (szMsg) {
				    		_.failure.slideDown(function () {
				    			setTimeout(function () {
				    				_.failure.slideUp();
				    				//_.form.trigger('reset');
				    			}, _.successShow);
				    		});
				    	},
				    	controlsFu: function () {
				    		$(_.controls, _.form).each(function () {
				    			var th = $(this);
				    			th
					    			.bind('click', function () {
					    				_.form.trigger(th.data('type'));
					    				return false;
					    			});
				    		});
				    	},
				    	showErrorFu: function (label) {
				    		label.find('.' + _.errorCl).slideDown();
				    	},
				    	hideErrorFu: function (label) {
				    		label.find('.' + _.errorCl).slideUp();
				    	},
				    	showEmptyFu: function (label) {
				    		label.find('.' + _.emptyCl).slideDown();
				    		_.hideErrorFu(label);
				    	},
				    	hideEmptyFu: function (label) {
				    		label.find('.' + _.emptyCl).slideUp();
				    	},
				    	init: function () {
				    		_.form = this;
				    		_.labels = $('label', _.form);

				    		_.preFu();

				    		_.controlsFu();

				    		_.form
					    		.bind('submit', function () {
					    			if (_.validate)
					    				_.submitFu();
					    			else
					    				_.form[0].submit();
					    			return false;
					    		})
					    		.bind('reset', function () {
					    			_.labels.removeClass(_.invalidCl);
					    			_.labels.each(function () {
					    				var th = $(this);
					    				_.hideErrorFu(th);
					    				_.hideEmptyFu(th);
					    			});
					    		});
				    		_.form.trigger('reset');
				    	}
				    };
				if (!data)
					(typeof opt == 'object' ? $.extend(_, opt) : _).init.call(th),
					th.data({ cScroll: _ }),
					data = _;
				else
					_ = typeof opt == 'object' ? $.extend(data, opt) : data;
			});
			return this;
		}
	});
})(jQuery);
$(function () {
	if ($('#contact-form').length) $('#contact-form').forms();
	if ($('#newsletter-alt-form').length) $('#newsletter-alt-form').forms();
	if ($('#newsletter-form').length) $('#newsletter-form').forms();
	if ($('#enroll-form').length) $('#enroll-form').forms({ controller: '/ContactUs/NewLeadSubmitForm' });
	if ($('#lead-form').length) $('#lead-form').forms({ controller: '/ContactUs/NewLeadSubmitForm'});
	if ($('#careers-form').length) $('#careers-form').forms({ controller: '/Careers/NewDealerLeadSubmitForm'});



	/** Bind actions */
	 var oCloseModalDlg = $("div.closeModalDlg");
	/** Bind close button. */
	if (oCloseModalDlg.length > 0)
	{
		var oNewLeadModalDlg = $("div.newLeadModalDlg");
		var oDownloadModalDlg = $("div.downloadModalDlg");
		var oNewDealerModalDlg = $("div.newDealerModalDlg");

		oCloseModalDlg.bind("click", function () {
			if (oNewLeadModalDlg.length > 0) oNewLeadModalDlg.hide();
			if (oDownloadModalDlg.length > 0) oDownloadModalDlg.hide();
			if (oNewDealerModalDlg.length > 0) oNewDealerModalDlg.hide();
		});
	}

})