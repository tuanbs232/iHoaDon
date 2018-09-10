jQuery(function($) {

	autoNumeric();
	
	$(".select2").select2();

	$(".datepicker").datepicker({
		format: 'dd/mm/yyyy',
		language: "vi-VI",
		todayHighlight: true,
		todayBtn: 'linked',
		autoclose:true
	});
	
	$(".datepicker").attr("autocomplete", "off");
	
	// for ajax modal/form
	$('form[data-async]').live('submit', function(event) {
		var $form = $(this);
		var $target = $($form.attr('data-target'));
		
		$.ajax({
			type: $form.attr('method'),
			url: $form.attr('action'),
			data: $form.serialize(),
			success: function(data, status, jqXHR) {
				$target.html(data);
			},
			complete: function(jqXHR, status) {
				var $next = $form.attr('action-done');
				if (typeof($next) !== "undefined") {
					setTimeout(function(){
						 window.location.href = $next;
                    }, 5000);
				}
			}
		});
		
		event.preventDefault();
	});
		
	// remove content after modal is hided
	$('body').on('hidden', '.modal', function () {
		$(this).removeData('modal');
	});
	
	// Accept only numeric values
	$('.number').keydown(function (e) {
        checkKeyCode(e);
    });
	
	// paste event
	$(".number").bind({
		paste : function(e){
		  var pastedText = getPastedText(e);

		  if (isNaN(pastedText)) {
			  e.preventDefault();
		  }
		}
	});
	
	// add multiple select / deselect functionality
    $("#selectall").click(function () {
          $('.case').attr('checked', this.checked);
    });
 
    // if all checkbox are selected, check the selectall checkbox
    // and viceversa
    $(".case").click(function() {
    	toggleCheckBox();
    });
    
    $(".confirm-delete").click(function(e) {
    	e.preventDefault();
    	if ($("input:checkbox:not('#selectall'):checked").length == 0) {
    		msg = "Vui lòng chọn ít nhất một dòng!";

    		showWarningBox(msg);
    	}else {
    		msg = "Bạn có chắc chắn muốn xóa không?";

	    	showConfirmBox(msg);
    	}
    });
    
    $(".confirm-resetpwd").click(function(e) {
    	e.preventDefault();
    	if ($("input:checkbox:checked").length == 0) {
    		msg = "Vui lòng chọn ít nhất một dòng!";

    		showWarningBox(msg);
    	}else {
    		msg = "Bạn có chắc chắn muốn khởi tạo mật khẩu không?";

	    	showConfirmBox(msg);
    	}
    });
    
	
	// MST 
	$('.taxCode').keydown(function (e) {

		if ( $('#taxCode').is('[readonly]') ) { 
			return
		} else {
			var $taxCode = $(this).val();
			
			if ($taxCode.length == 10 && !isBackSpace(e) && !isShift(e) && isNumberOrMinusSign(e)) {
				$(this).val($(this).val() + "-");
			}
			checkKeyCode(e);
		}
		
	});
	
	// paste event
	$(".taxCode").bind({
		paste : function(e){
			var pastedText = getPastedText(e).trim();
			var msg = "Mã số thuế không hợp lệ!";
			if (!isValidTaxCode(pastedText)) {
				showWarningBox(msg);
				e.preventDefault();
			}
		}
	});
	
	$("input.uppercase").change(function() {
	  $(this).val( $(this).val().toUpperCase() );
	});
	
	$("input.auto-trim").blur(function() {
		$(this).val( $(this).val().trim() );
	});
	
	$("#openInvoicesModal").click(function() {
		searchCertifiedInv();
		$('#invoicesModal').modal('show');
	});

	$('.modal:not(#dsModal)').on('shown', function() {
		$("body").css("overflow", "hidden");
	}).on('hidden', function() {
		$("body").css("overflow", "auto");
		$('.modal-backdrop').hide();
	});
	
	$(window).bind("load", function() {
	   $('form:not(#searchForm) input:enabled:visible:not([disabled]):not([readonly]):not(.datepicker):first').focus();
	});
	
	$('input[type=file]').bootstrapFileInput();
	$('.file-inputs').bootstrapFileInput();
});

$(document).ajaxSuccess(function() {
	autoNumeric();
});

function toggleCheckBox() {
	if ($(".case").length == $(".case:checked").length) {
        $("#selectall").attr("checked", "checked");
    } else {
        $("#selectall").removeAttr("checked");
    }
}

function checkKeyCode(e) {
	var keyCode = e.which || e.keyCode; // Capture the event

	if (isShift(e) && keyCode != 37 && keyCode != 39) {
		e.preventDefault();
	} else if (keyCode != 8 && keyCode != 9 && keyCode != 13 && keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40 && keyCode != 46) {
        if (keyCode < 48) {
            e.preventDefault();
        } else if (keyCode > 57 && keyCode < 96 && !isCopyPaste(e)) {
    		e.preventDefault();
        } else if (keyCode > 105) {
            e.preventDefault();
        }
    }
}

function showConfirmBox(message) {
	$('#confirmModal').modal('show');
	$('#confirmModal').find($('p')).text(message);
	
	$('#btnYes').click(function() {
		$('#confirmModal').modal('hide');
		$("[name=cancel]").val("true");
		$('.confirm-form').submit();
	});
}

function showWarningBox(message) {
	$('#warningModal').modal('show');
	$('#warningModal').find($('p')).text(message);
}

function showPleaseWaitDialog() {
	$('#pleaseWaitDialog').modal('show');
}

function hidePleaseWaitDialog() {
	$('#pleaseWaitDialog').modal('hide');
}

function setSelect2Validation(selectId, validationClass) {
    $('#' + selectId).select2('container').attr('id', selectId + "Validate").addClass(validationClass);
	 
	 $.fn.setTypeForHook = function () {
        this.each(function () {
            this.type = selectId;
        });
        return this;
    };
	    
    $('#'+selectId).select2('container').setTypeForHook();
    
    $.valHooks[selectId] = {
        get: function (el) {
           return $('#'+selectId).select2("val");
        },
        set: function (el, val) {
            $('#'+selectId).select2("val", val);
        }
    };
}

function chainSelector(subOption, servletUrl, selectedValue) {
	
	$.ajax({
		type : 'GET',
		url : servletUrl,
		cache : false,
		dataType : 'json',
		success : function(data) {
			var returnList = data.returnList;
			$(subOption).empty();
			$(subOption).append($('<option/>', {
				value : '',
				text : '--- Chọn ---'
			}));
			$.each(returnList, function(key, value) {

				$(subOption).append($('<option/>', {
					value : value,
					text : value
				}));
				
				if ((value == selectedValue) && (selectedValue != '')){
					$(subOption).val(selectedValue);
					$(subOption).select2().select2('val', selectedValue);
				}
			});
		}
	});
}

function autoNumeric() {
	formatCurrency();
	
	$('.format-number').autoNumeric('init', {
		mDec  : '0',
		aSep  : '.',
		aDec  : ',',
		vMax  : '9999999999999',
		lZero : 'deny'
	});
	
	$('.format-float').autoNumeric('init', {
		aPad  : false,
		aSep  : '.',
		aDec  : ',',
		mDec : '2',
		vMax  : '9999999999999.99',
		lZero : 'deny'
	});
	
	$('.format-percentage').autoNumeric('init', {
		aPad  : false,
		aSign : ' %',
		pSign : 's',
		aSep  : '.',
		aDec  : ',',
		vMax  : '999.99',
		lZero : 'deny'
	});
	
	$('.number-only').autoNumeric('init', {
		mDec  : '0',
		aSep  : '',
		aDec  : ',',
		vMax  : '999999999999999',
		lZero : 'deny'
	});
}

function formatCurrency() {

	$('.format-currency').autoNumeric('init', {
		aPad  : false,
		aSep  : '.',
		aDec  : ',',
		mDec: '2',
		vMax  : '9999999999999.99',
		pSign : 's',
		lZero : 'deny'
	});
}

// This function is used by the login screen to validate user/pass
// are entered.
function validateRequired(form) {
    var bValid = true;
    var focusField = null;
    var i = 0;
    var fields = new Array();
    oRequired = new required();

    for (x in oRequired) {
        if ((form[oRequired[x][0]].type == 'text' || form[oRequired[x][0]].type == 'textarea' || form[oRequired[x][0]].type == 'select-one' || form[oRequired[x][0]].type == 'radio' || form[oRequired[x][0]].type == 'password') && form[oRequired[x][0]].value == '') {
           if (i == 0)
              focusField = form[oRequired[x][0]];

           fields[i++] = oRequired[x][1];

           bValid = false;
        }
    }

    if (fields.length > 0) {
       focusField.focus();
       alert(fields.join('\n'));
    }

    return bValid;
}

// This function is a generic function to create form elements
function createFormElement(element, type, name, id, value, parent) {
    var e = document.createElement(element);
    e.setAttribute("name", name);
    e.setAttribute("type", type);
    e.setAttribute("id", id);
    e.setAttribute("value", value);
    parent.appendChild(e);
}

function confirmDelete(obj) {
    var msg = "Are you sure you want to delete this " + obj + "?";
    ans = confirm(msg);
    return ans;
}

// 18n version of confirmDelete. Message must be already built.
function confirmMessage(obj) {    
	var $mes = obj + "?";
    ans = confirm($mes);
    return ans;
}

// un-format numeric 
function disableAutoNumeric() {

	$('.format-number').each(function(i) {
		var self = $(this);
		destroyAutoNumeric(self);
	});
	
	$('.format-float').each(function(i) {
		var self = $(this);
		destroyAutoNumeric(self);
	});
	
	$('.format-currency').each(function(i) {
		var self = $(this);
		destroyAutoNumeric(self);
	});
	
	$('.format-percentage').each(function(i) {
		var self = $(this);
		destroyAutoNumeric(self);
	});
}

function destroyAutoNumeric(self) {
	try {
		var v = self.autoNumeric('get');
		self.autoNumeric('destroy');
		self.val(v);
	} catch (err) {
		console.log("Not an autonumeric field: " + self.attr("name"));
	}
}

function isValidTaxCode(input) {
	var isValid = false;
	
	if (10 == input.length) {
		isValid = isValidFirstTenNumberOfTaxCode(input);
	} else if (14 == input.length) {
		var firstTenNumbers = input.substring(0, 10);
		var minusSign = input.substring(10, 11);
		var lastThreeNumbers = input.substring(11, 14);

		if (!isNaN(firstTenNumbers) 
				&& !isNaN(lastThreeNumbers) 
				&& ("-" == minusSign)) {
			isValid = isValidFirstTenNumberOfTaxCode(firstTenNumbers);
		}
	}
	return isValid;
}

function checkTaxCode(field, rules, i, options) {
	if ($("#taxCode").is('[readonly]')){
		return;
	}
	var input = field.val();

	if (!isValidTaxCode(input)) {
		return options.allrules.taxCode.alertText;
	}
}

function isValidFirstTenNumberOfTaxCode(taxCode) {
	var n1 = taxCode.substring(0, 1);
	var n2 = taxCode.substring(1, 2);
	var n3 = taxCode.substring(2, 3);
	var n4 = taxCode.substring(3, 4);
	var n5 = taxCode.substring(4, 5);
	var n6 = taxCode.substring(5, 6);
	var n7 = taxCode.substring(6, 7);
	var n8 = taxCode.substring(7, 8);
	var n9 = taxCode.substring(8, 9);
	var n10 = taxCode.substring(9, 10);
	
	if (n10 != (10 - ((n1*31 + n2*29 + n3*23 + n4*19 +n5*17 + n6*13 + n7*7 + n8*5 + n9*3) % 11))) {
		return false;
	} else {
		return true;
	}
}

function isCopyPaste(e) {
	var keyCode = e.which || e.keyCode;
	if (e.ctrlKey && (keyCode == 65 || keyCode == 67 || keyCode == 86 || keyCode == 88)){
		return true;
	} else {
		return false;
	}
}

function getPastedText(e) {
	var pastedText = undefined;
	if (window.clipboardData && window.clipboardData.getData) { // IE
		pastedText = window.clipboardData.getData('Text');
	} else if (e.originalEvent.clipboardData && e.originalEvent.clipboardData.getData) { // other browsers
		pastedText = e.originalEvent.clipboardData.getData('text/plain');
	}

	return pastedText;
}

function isBackSpace(e) {
	var keyCode = e.keyCode || e.which;
	return keyCode == 8;
}

function isShift(e) {
	var isShift;
	if (window.event) {
		isShift = window.event.shiftKey ? true : false;
	} else {
		isShift = e.shiftKey ? true : false;
	}
	return isShift;
}

function isNumberOrMinusSign(e) {
	var keyCode = e.keyCode || e.which;
	isNumber = (keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105);
	isMinusSign = (keyCode == 173 || keyCode == 189 || keyCode == 109);
	return (isNumber || isMinusSign);
}

function roundUp(num) {
	return Math.round(num * 100) / 100;
}

function removeFormError() {
	// Removing everything with the .formError class, kills all popups (if any).
	$(".formError").remove(); 
}

function notNegative(field, rules, i, options) {
	var len = parseFloat(field.autoNumeric('get'));
	if (len < 0 ) {
		var rule = options.allrules.notNegative;
		return rule.alertText;
	}
}

function showExistingBox(reportId) {
	
	$('#existingReportModal').modal('show');
	if (reportId == '-1') {
		$('#existingReportModal').find($('p')).text("Báo cáo với tiêu chí này đã tồn tại và đang được xử lý");
		$('#existingReportModal').find($('#btnViewReport')).remove();
	} else {
		$('#existingReportModal').find($('p')).text("Báo cáo với tiêu chí này đã tồn tại");
		$('#btnViewReport').click(function() {
			$('#existingReportModal').modal('hide');
			window.open($("#ctx").val()+"/reports/view?reportId="+reportId, '_blank');
		});
	}
	$('#btnReRun').click(function() {
		$('#existingReportModal').modal('hide');
		$("#reRun").val('true');
		$('form').submit();
	});
}