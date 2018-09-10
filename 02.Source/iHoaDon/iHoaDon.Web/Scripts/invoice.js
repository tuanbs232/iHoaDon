jQuery(function($) {
	$().maxlength();
	
	$("#currencies").change(function() {

		if ($(this).val() != 'VND') {
			var param = {
					currencyCode : $(this).val()
			};

			$("#exchangeRate").removeAttr("readonly");
			$("#exchangeRate").removeClass("format-currency validate[positive] input-medium");
			$("#exchangeRate").addClass("format-currency validate[required, positive] input-medium");
			$.ajax({
				type : 'GET',
				url : $("#contextPath").val() + "/ajax/getCurrency",
				cache: false,
				data : param,
				dataType : 'json',
				success : function(currency) {
					$("#exchangeRate").autoNumeric('set', currency.sellExchangeRate);
					try {
						setTotalGoodsPrice();
					} catch (e) {
						$("#exchangeRate").val('');
						$("#currencies").select2('val', 'VND');
						$("#exchangeRate").attr("readonly", "readonly");
						$("#exchangeRate").removeClass("format-currency validate[required, positive] input-medium");
						$("#exchangeRate").addClass("format-currency validate[positive] input-medium");
						
						setTotalGoodsPrice();
						showWarningBox($("#errCurrency").val());
					}
					
					$('#exchangeRate').trigger('focusout');
				}
			});
			
		} else {
			$("#exchangeRate").val('');
			$("#exchangeRate").attr("readonly", "readonly");
			$("#exchangeRate").removeClass("format-currency validate[required, positive] input-medium");
			$("#exchangeRate").addClass("format-currency validate[positive] input-medium");
			setTotalGoodsPrice();
		}
	});

	$("select#sellerBankAccount").change(function() {
		$("#sellerBankAccountNumber").val($('select#sellerBankAccount option:selected').data("accountnumber"));
		$("#sellerBankName").val($('select#sellerBankAccount option:selected').data("bank"));
	});
	
	$("#exchangeRate").keyup(function(event) {
		if (event.keyCode != 188) {
			var $exchangeRate = $("#exchangeRate").val();
			var $exchangeRateLength = $exchangeRate.length;
			if (',0' != $exchangeRate.substring(($exchangeRateLength-2), $exchangeRateLength)) {
				try {
					setTotalGoodsPrice();
				} catch (e) {
					if ($exchangeRate  > 1){
						$(this).autoNumeric('set', 1);
					}else{
						$(this).autoNumeric('set', '');
					}
					setTotalGoodsPrice();
					showWarningBox($("#errCurrency").val());
				}
			}
		}
	});
	$("#paymentMethodName").change(function() {
		if ($('#paymentMethodName').select2('val').indexOf('Chuyển khoản') >= 0){
			hasWarningAboutPaymentMethod = false;
		}else if (totalWarningAboutPaymentMethod >= 20000000){
			hasWarningAboutPaymentMethod = true;
		}
	});
});
var totalWarningAboutPaymentMethod = 0;
function checkForWarningAboutPaymentMethod(total){
	totalWarningAboutPaymentMethod = total;
	hasWarningAboutPaymentMethod = false;
	if (total >= 20000000 && document.getElementById('paymentMethodName')){
		if ($('#paymentMethodName').select2('val') != 'Chuyển khoản'){
			hasWarningAboutPaymentMethod = true;
		}
	}
}

function changeUnitName(it, nameOfUnit) {
	$("input[name='" + nameOfUnit + "']").val( $("#" + it.id + " option:selected").attr("unit-name") ); 
}

function deleteRow(tableID, row) {

	var table = document.getElementById(tableID);
	var rowCount = table.rows.length;
	var colCount = $("#" + tableID).find('tr')[0].cells.length;

	if (rowCount != 2) { // must have at least one item

		removeFormError();

		table.deleteRow(row);
		for(var i = (row); i < table.rows.length; i++) {
			var oldRowNum = (i + 1);
			var newRowNum = (i);
			
			// cell[0]: first column
			table.rows[i].cells[0].innerHTML = table.rows[i].cells[0].innerHTML.replace('value="'+ (oldRowNum) + '"', 
																						'value="'+ (newRowNum) + '"');
			
			// cell[colCount]: last column
			table.rows[i].cells[(colCount-1)].innerHTML = table.rows[i].cells[(colCount-1)].innerHTML.replace("deleteRow" + oldRowNum, 
																									          "deleteRow" + newRowNum);
			setOnDeleteRowClick(newRowNum);
		}
	}
	setTotalGoodsPrice();
}

function saveInvoice() {
    if (validateForm()) {
        $('#01GTKT').submit();
    }
}

function setOnDeleteRowClick(rowNum) {
	$("#deleteRow" + rowNum).click(function() {
		deleteRow('dataTable', (rowNum));
	});
}

function setOnItemCodeChanged(suffix) {
	$("#itemCode" + suffix).change(function() {
		
		if (document.getElementById('enableItemTotal' + suffix)){ 
			$("#enableItemTotal" + suffix).removeAttr("checked");
			toggleItemTotal(suffix);
		}
		 
		var selectedVal = $(this).val();
		if ('' == selectedVal) {
			//$("#itemName" + suffix).removeAttr("readonly");
			if ( $('#itemPromotion' + suffix).val() == '1'){
				$("#itemName" + suffix).val('Khuyến mại');
			}else{
				$("#itemName" + suffix).val('');
			}
			$("#itemPrice" + suffix).autoNumeric('set', '');
			$("#itemUnit" + suffix).select2('val', ' ');
			$("#itemUnit" + suffix).trigger('change');

			$("#itemVat" + suffix).select2('val', '');
			$("#itemVat" + suffix).trigger('change');
			
			$("input[name='invoiceItemList[" + suffix + "].unitName']").val('');
			$("input[name='invoiceItemList[" + suffix + "].unitName']").trigger('change');
			$("#itemQty" + suffix).autoNumeric('set', '');
			setTotal(suffix);
			return false;
		} else {
		//	$("#itemName" + suffix).attr("readonly", "readonly");

			var param = {
					itemCode : selectedVal
			};
			$.ajax({
				type : 'GET',
				url: '/Invoice/Products',
				cache: false,
				data : param,
				dataType : 'json',
                success: function (itemMaster) {
                    console.log(itemMaster);
					if ( $('#itemPromotion' + suffix).val() == '1'){
						$("#itemName" + suffix).val('Khuyến mại ' + itemMaster.itemName);
					}else{
					    $("#itemName" + suffix).val(itemMaster.ProductName);
					}
					
					if (itemMaster.Price != null) {
					
					    $("#itemPrice" + suffix).autoNumeric('set', itemMaster.Price);
					} else {
						$("#itemPrice" + suffix).autoNumeric('set', '');
					}
					
					$("#itemVat" + suffix).select2('val', itemMaster.categoryPercentage);
					$("#itemVat" + suffix).trigger('change');
					
					var itemUnit = document.getElementById("itemUnit" + suffix);
					var itemUnitDeleted = true;
					for (var i = 0; i < itemUnit.length; i++) {
						if (null == itemMaster.unitCode || (itemUnit.options[i].text == itemMaster.unitName
								&& itemUnit.options[i].value == itemMaster.unitCode)) {
							
							itemUnitDeleted = false;  
						}
					}
					
					if (itemUnitDeleted) { // if itemmaster.unit has been deleted, add this value to select-box
						var x = document.getElementById("itemUnit" + suffix);
						var option = document.createElement("option");
						option.text = itemMaster.unitName;
						option.value = itemMaster.unitCode;
						option.setAttribute("unit-name", itemMaster.unitName);
						x.add(option);
					}
					if (itemMaster.unitCode){
                        $("#itemUnit" + suffix).select2('val', itemMaster.unitCode);
                        $("#itemUnit" + suffix).val(itemMaster.unitCode);
					}else{
						$("#itemUnit" + suffix).select2('val', ' ');
					}
					$("#itemUnit" + suffix).trigger('change');
					$("input[name='invoiceItemList[" + suffix + "].unitName']").val( $("#itemUnit" + suffix + " option:selected").attr("unit-name") ); 
					
					try {
						setTotal(suffix);
					} catch (e) {
						$("#itemQty" + suffix).autoNumeric('set', 0);
						setTotal(suffix);
						showWarningBox($("#errCurrency").val());
					}
				}
			
			});
		}

	});
}

function setTaxBreakdownForNormalInv() {
	$('#summaryTable tr:not([class!="vatCategory"])').remove ();
	var table = document.getElementById('dataTable');
	var rowCount = table.rows.length;
	
	
	for(var i = 1; i < rowCount; i++) {
		var suffix = parseInt(table.rows[i].getAttribute('suffix'));
		if ($("#itemPromotion" + suffix).val() == '1'){
			continue;
		}
		var itemVatPercent = $("#itemVat" + suffix).val();
		var vatLabel = $('#itemVat' + suffix  + ' option:selected').text(); 
		
		var counted = false;
		
		if (!itemVatPercent || isNaN(itemVatPercent)) {
			counted = true;
		} else {
			for(var j = 1; j < i; j++) {
				var suffixPrev = parseInt(table.rows[j].getAttribute('suffix'));
				if ($("#itemPromotion" + suffixPrev).val() == '1'){
					continue;
				}
				var itemVatPercentPrev = $("#itemVat" + suffixPrev).val();
				if (itemVatPercent == itemVatPercentPrev) {
					counted = true;
				}
			}
		}

		if (!counted) {
			var itemTotal = parseFloat($("#itemTotal" + suffix).autoNumeric('get'));
			if (isNaN(itemTotal)){
				itemTotal = 0;
			}
			var itemVatPercentValue = parseFloat(itemVatPercent);
			if (itemVatPercentValue < 0){
				itemVatPercentValue = 0;
			}
			var vat = (itemTotal * itemVatPercentValue / 100);
			for(var k = i+1; k < rowCount; k++) {
				var suffixNext = parseInt(table.rows[k].getAttribute('suffix'));
				if ($("#itemPromotion" + suffixNext).val() == '1'){
					continue;
				}
				var itemVatPercentNext = $("#itemVat" + suffixNext).val();
				
				if (itemVatPercentNext == itemVatPercent) {
					var itemVatPercentValueNext = parseFloat(itemVatPercentNext);
					if (itemVatPercentValueNext < 0){
						itemVatPercentValueNext = 0;
					}
					var itemTotalNext = $("#itemTotal" + suffixNext).autoNumeric('get');
					if (!itemTotalNext || isNaN(itemTotalNext)){
						itemTotalNext = 0;
					}
					vat += (itemVatPercentValueNext * itemTotalNext / 100);
					itemTotal += parseFloat(itemTotalNext);
				}
			}
			
			if (document.getElementById('currencies') && $("#currencies").val() != 'VND') {
				var exchangeRate = $("#exchangeRate").autoNumeric('get');
				vat *= exchangeRate;
				itemTotal *= exchangeRate;
			}
			
			if (itemVatPercent != '') {
				var newRow = $("<tr class='vatCategory'>" +
						"<td class='text-align-left'><input type='hidden' name='invoiceTaxBreakdownList["+(i-1)+"].vatCategoryPercentage' value='" + itemVatPercent + "'/>" + vatLabel + " </td>" +
						"<td></td>" +
						"<td><input type='text' value='" + vat + "' name='invoiceTaxBreakdownList["+(i-1)+"].vatCatTaxAmt' class='format-currency disable' data-a-sign=' VND' readonly='true'></td>" +
						"<td><input type='text' value='" + itemTotal + "' name='invoiceTaxBreakdownList["+(i-1)+"].vatCatTaxableAmt' class='format-currency disable' data-a-sign=' VND' readonly='true'></td>" +
						
				"</tr>");
				$("#totalVatAmountRow").before(newRow);
				autoNumeric();
			}
		}
	}
}

function calTotalAmount(suffix) {
	$("#itemTotal" + suffix).keyup(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		try {
			setTotalGoodsPrice();
		} catch (e) {
			$(this).autoNumeric('set', 0);
			setTotalGoodsPrice();
			showWarningBox($("#errCurrency").val());
		}
	});
	$("#itemTotal" + suffix).change(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		try {
			setTotalGoodsPrice();
		} catch (e) {
			$(this).autoNumeric('set', 0);
			setTotalGoodsPrice();
			showWarningBox($("#errCurrency").val());
		}
	});

	$("#itemPrice" + suffix).keyup(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		if (event.keyCode != 188) {   // keycode 188 = ',': decimal separator

			var $itemPrice = $("#itemPrice" + suffix).val();
			var $itemPriceLength = $itemPrice.length;
			if (',0' != $itemPrice.substring(($itemPriceLength-2), $itemPriceLength)) {
				try {
					setTotal(suffix);
				} catch (e) {
					$(this).autoNumeric('set', 0);
					setTotal(suffix);
					showWarningBox($("#errCurrency").val());
				}
			}
		}
	});
	
	$("#itemPrice" + suffix).change(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		var $itemPrice = $("#itemPrice" + suffix).val();
		var $itemPriceLength = $itemPrice.length;
		if (',0' != $itemPrice.substring(($itemPriceLength-2), $itemPriceLength)) {
			try {
				setTotal(suffix);
			} catch (e) {
				$(this).autoNumeric('set', 0);
				setTotal(suffix);
				showWarningBox($("#errCurrency").val());
			}
		}
	});
	
	$("#itemQty" + suffix).keyup(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		if (event.keyCode != 188) {
			var $itemQty = $("#itemQty" + suffix).val();
			var $itemQtyLength = $itemQty.length;
			if (',0' != $itemQty.substring(($itemQtyLength-2), $itemQtyLength)) {
				try {
					setTotal(suffix);
				} catch (e) {
					$(this).autoNumeric('set', 0);
					setTotal(suffix);
					showWarningBox($("#errCurrency").val());
				}
			}
		}
	});
	
	$("#itemQty" + suffix).change(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		var $itemQty = $("#itemQty" + suffix).val();
		var $itemQtyLength = $itemQty.length;
		if (',0' != $itemQty.substring(($itemQtyLength-2), $itemQtyLength)) {
			try {
				setTotal(suffix);
			} catch (e) {
				$(this).autoNumeric('set', 0);
				setTotal(suffix);
				showWarningBox($("#errCurrency").val());
			}
		}
	});
	
	$("#itemVat" + suffix).change(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		try {
			if (!validateOnchangeItemVat()) {
				showWarningBox("Hóa đơn chỉ được phép có 1 loại thuế suất.");
				resetVatValue(suffix);
			}
			setTotalGoodsPrice();
		} catch (e) {
			$(this).select2('val', '');
			setTotalGoodsPrice();
			showWarningBox($("#errCurrency").val());
		}
	});
	$("#discountAmount").keyup(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		if (!validateOnChangeToDiscount()){
			showWarningBox("Hóa đơn chỉ được phép có 1 loại thuế suất.");
			$(this).val('');
			setTotalGoodsPrice();
			$(this).data('oldVal',  '' );
		}else{
			try {
				setTotalGoodsPrice();
				$(this).data('oldVal',  $(this).val() );
			} catch (e) {
				var oldValue = $(this).data('oldVal');
				//$(this).autoNumeric('set', oldValue);
				$("#discountAmount").val(oldValue);
				setTotalGoodsPrice();
				showWarningBox($("#errCurrency").val());
			}
		}
	});
	$("#discountAmount").change(function(event) {
		if ($(this).is('[readonly]')){
			return;
		}
		if (!validateOnChangeToDiscount()){
			showWarningBox("Hóa đơn chỉ được phép có 1 loại thuế suất.");
			$(this).val('');
			setTotalGoodsPrice();
			$(this).data('oldVal',  '' );
		}else{
			try {
				setTotalGoodsPrice();
				$(this).data('oldVal',  $(this).val());
			} catch (e) {
				var oldValue = $(this).data('oldVal');
				$("#discountAmount").val(oldValue);
				
				setTotalGoodsPrice();
				showWarningBox($("#errCurrency").val());
			}
		}
	});
}

function setTotal(suffix) { // Thanh tien
	if ($("#itemPromotion"+suffix).val() == '1'){
		return;
	}
	if ($("#itemQty" + suffix).is('[readonly]') && $("#itemPrice" + suffix).is('[readonly]')){
		return;
	}
	var itemQty = $("#itemQty" + suffix).autoNumeric('get');
	var itemPrice = $("#itemPrice" + suffix).autoNumeric('get');
	
	if (itemQty && itemPrice){
		var totalAmount = itemQty * itemPrice;
		if (totalAmount > 10000000000000) {
			totalAmount = 10000000000001;
		}

		$("#itemTotal" + suffix).autoNumeric('set', totalAmount);
	}else{
		$("#itemTotal" + suffix).val('');
	}
	
	setTotalGoodsPrice();
	
}

//validate end number for invoice template screen
function checkEndNumber(field, rules, i, options) {
	
	var startNumber = $("#startNumber").val();
	var endNumber = $("#endNumber").val();
	
	if (parseInt(endNumber) < parseInt(startNumber)) {
		if (field.attr("id") == "endNumber") {
			return options.allrules.endNumber.alertText;
		} else if (field.attr("id") == "startNumber") {
			return options.allrules.endNumber.alertText1;
		}
	}
}

// calculate invoice quantity for invoice template screen
function calculateInvQty() {
	var startNumber = $("#startNumber").val();
	var endNumber = $("#endNumber").val();

	var quantity = endNumber - startNumber + 1;
	if (startNumber!=0 && endNumber !=0 && quantity >= 1) {
		$("#quantity").val(quantity);
	} else {
		$("#quantity").val('');
	}
}

//validate positive number	
function checkPositive(field, rules, i, options) {		
	var number = field.val();
	if (number == 0) {
		return options.allrules.positive.alertText;
	}
}

function itemCodeFormatSelection(data) {
	if (data.id.length == 0) {
		return data.text;
	} else {
		return data.id;
	}
}

function formatDate(date){
    var yyyy = date.getFullYear().toString();                                    
    var mm = (date.getMonth()+1).toString(); // getMonth() is zero-based         
    var dd  = date.getDate().toString();             
                        
    return (dd[1]?dd:"0"+dd[0])  + '/' + (mm[1]?mm:"0"+mm[0]) + '/' + yyyy;
}

function checkUnitCode(field, rules, i, options) {
	var suffix = field.closest('tr').attr("suffix");
  	if (suffix){
		if ($('#itemUnit'+ suffix).val() == ' '){
			rules.push("required");
			return options.allrules.required.alertText;
		}
  	} 
}

function cloneItemCodeCol(tableID, suffix) {
	
	var result = "<td><select id='itemCode" + suffix + "' name='invoiceItemList[" + suffix + "].itemCode' style='width: 100%;' class='form-control'>";
	result += $('#' + tableID).find('select[id^="itemCode"]:first').html();
	result += "</select></td>";
	
	return result;
}

function cloneItemVatCol(tableID, suffix) {
	
	var result = "<td><select id='itemVat" + suffix + "' name='invoiceItemList[" + suffix + "].vatCategoryPercentage' class='form-control' style='width: 100px;'>";
    result += $('#' + tableID).find('select[id^="itemVat"]:first').html();
    //document.querySelector('[id^="poll-"]').id;
	result += "</select></td>";
	
	return result;
}

function cloneItemUnitCol(tableID, suffix) {

	var clss = "";
	if ($("#invTypeCode").val() == '03XKNB' || $("#invTypeCode").val() == '04HGDL') {
		clss = "class='validate[funcCall[checkUnitCode]]'";
	}
	
	var result = "<td><select id='itemUnit" + suffix + "' class='form-control' name='invoiceItemList[" + suffix + "].unitCode' style='width: 100%;' onchange='changeUnitName(this, \"invoiceItemList["+suffix+"].unitName\");' "+clss+">";
	result += $('#' + tableID).find('select[id^="itemUnit"]:first').html();
	result += "</select><input type='text' id='invoiceItemList["+suffix+"].unitName' name='invoiceItemList["+suffix+"].unitName' style='display: none' class='validate[required] custom-select-combo-input form-control'  maxlength='20' /></td>";
	
	return result;
}

$(document).ready(function () {
    $('.customer-infos input').keyup(function () {
        removeFormError();
    });
});