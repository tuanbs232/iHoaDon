function addListener(suffix) {

	autoNumeric();
	setOnItemCodeChanged(suffix);
	calTotalAmount(suffix);
	//$("#itemCode"+suffix).select2({formatSelection: itemCodeFormatSelection});
	$("#itemCode"+suffix).select2('val', '');
	
	//$("#itemUnit"+suffix).select2();
	$("#itemUnit"+suffix).select2('val', ' ');
	
	$("#itemUnit" + suffix).customSelectCombo({
		  customInputTarget: 'invoiceItemList['+suffix+'].unitName',
		  inputDimensions: ['85px', '20px'],
		  setInputValueOnTrigger: false });
	$("#itemUnit" + suffix).trigger('change');
	
	$("#itemVat"+suffix).select2();
	$("#itemVat"+suffix).select2('val', '');
	if ($('#itemPromotion' + suffix).val() != '1') {
        setSelect2Validation("itemVat" + suffix, 'validate[required]');
        $('.select2-container').removeClass('validate[required]');
	} else {
		setSelect2Validation("itemVat" + suffix, '');
    }
    $('.select2-container').addClass('select2-offscreen');
    $("#itemVat" + suffix).removeClass('select2-offscreen');
	
	$("#itemName"+suffix).removeAttr("readonly"); 

	//$("#itemPromotion"+suffix).select2();
	
	$('#itemUnit' + suffix + ' option:last-child').attr('unit-name', '');
}
function toggleItemTotal(suffix, reloadPrice) {
	doToggleItemTotal(suffix, true);
}

function doToggleItemTotal(suffix, reloadPrice) {
	removeFormError();
	if ($('#enableItemTotal' + suffix).is(":checked")) {
		
		$( "#itemTotal" + suffix ).addClass( "validate[required, positive]");
		$( "#itemTotal" + suffix ).removeClass( "disable");
		$( "#itemTotal" + suffix ).attr("readonly", false);
//		$( "#itemTotal" + suffix ).val('');
		
		$( "#itemQty" + suffix ).removeClass( "validate[required, positive]");
		$( "#itemQty" + suffix ).addClass( "disable");
		$( "#itemQty" + suffix ).attr("readonly", true);
		$( "#itemQty" + suffix ).val('');
		
		if (!($("#itemPrice" + suffix).is('[readonly]'))){
			if ($( "#itemPrice" + suffix ).hasClass("validate[required, positive]")){
				$( "#itemPrice" + suffix ).removeClass( "validate[required, positive]");
			}
			if ($( "#itemPrice" + suffix ).hasClass("validate[required, funcCall[notNegative]]")){
				$( "#itemPrice" + suffix ).removeClass( "validate[required, funcCall[notNegative]]");
			}
		}
		$( "#itemPrice" + suffix ).addClass( "disable");
		$( "#itemPrice" + suffix ).attr("readonly", true);
		$( "#itemPrice" + suffix ).val('');
	} else {

		$( "#itemTotal" + suffix ).removeClass( "validate[required, positive]");
		$( "#itemTotal" + suffix ).addClass( "disable");
		$( "#itemTotal" + suffix ).attr("readonly", true);
		$( "#itemTotal" + suffix ).val('');
		
		$( "#itemQty" + suffix ).addClass( "validate[required, positive]");
		$( "#itemQty" + suffix ).removeClass( "disable");
		$( "#itemQty" + suffix ).attr("readonly", false);
		
		if ($('#itemPromotion' + suffix).val() == '1'){
			$( "#itemPrice" + suffix ).addClass( "validate[required, funcCall[notNegative]]");
		}else {
			$( "#itemPrice" + suffix ).addClass( "validate[required, positive]");
		}
		$( "#itemPrice" + suffix ).removeClass( "disable");
		$( "#itemPrice" + suffix ).attr("readonly", false);
		
		if ($('#itemPromotion' + suffix).val() != '1'){
			$( "#itemVat" + suffix ).addClass( "validate[required]");
		}
		
		if (reloadPrice){
			if  ($("#itemCode" + suffix).val()){
				var selectedVal = $("#itemCode" + suffix).val();
				var param = {
						itemCode : selectedVal
				};
				
				$.ajax({
					type : 'GET',
					url: '/Invoice/Products',
					cache: false,
					data : param,
					dataType : 'json',
					success : function(itemMaster) {
					    $("#itemName" + suffix).val(itemMaster.ProductName);
					   if (itemMaster.UnitId != null) {					      
                             $("#itemPrice" + suffix).autoNumeric('set', itemMaster.Price);
						} else {
							$("#itemPrice" + suffix).autoNumeric('set', '');
						}
						setTotalGoodsPrice();
					}
				
				});
			}
		}
	}
	setTotalGoodsPrice();

}

function addRow(tableID, invType) {
	
	removeFormError();

	var table = document.getElementById(tableID);

	var rowCount = table.rows.length;

	var oldSuffix = parseInt(table.rows[(rowCount - 1)].getAttribute('suffix'));
	var newSuffix = oldSuffix + 1;
	
    var idxCol = "<td><input style=\"text-align: center; border: none\" id='lineNumber" + newSuffix + "' class='disable form-control' type='text' name='invoiceItemList[" + newSuffix + "].lineNumber' readonly='true' value='" + rowCount + "'></td>";
	var promotionCol = "<td style=\"text-align:center;\"><input type='checkbox' id='itemPromotion" + newSuffix +"' name='invoiceItemList[" + newSuffix + "].promotion' onchange='changeItemPromotion(" + newSuffix + ");' />"		                
			           + "</td>";
	
	var itemCodeCol = cloneItemCodeCol(tableID, newSuffix);
	
    var itemNameCol = "<td><input type='text' id='itemName" + newSuffix + "' name='invoiceItemList[" + newSuffix + "].itemName' class='validate[required] form-control' style='min-width: 150px;' maxlength='255'></td>";
	var unitCol = cloneItemUnitCol(tableID, newSuffix);
	
    var qtyCol = "<td><input type='text' id='itemQty" + newSuffix + "' name='invoiceItemList[" + newSuffix + "].quantity' class='validate[required, positive] form-control format-float' maxlength='20'></td>";
	
	var itemVatCol = "";
	var enableItemTotalCol = "";
	var itemPriceCol = "";
	if (invType == '01GTKT') {
		itemVatCol = cloneItemVatCol(tableID, newSuffix);
	}
	if (invType == '01GTKT' || invType == '02GTTT' || invType == '07KPTQ') {
        enableItemTotalCol = "<td style='text-align: center;'><input id='enableItemTotal" + newSuffix + "' type='checkbox' onClick='toggleItemTotal(" + newSuffix + ");' title='" + $("#enableItemTotal" + oldSuffix).attr("title") + "' class='js-switch1 js-switch-" + newSuffix +"' style='width: 60px;'/></td>";
        itemPriceCol = "<td><input type='text' id='itemPrice" + newSuffix + "' name='invoiceItemList[" + newSuffix + "].unitPrice' class='validate[required, positive] format-currency' maxlength='15'></td>";
	}else{
		promotionCol = "";
		
        itemPriceCol = "<td><input type='text' id='itemPrice" + newSuffix + "' name='invoiceItemList[" + newSuffix + "].unitPrice' class='validate[positive] format-currency form-control' style='min-width: 60px;' maxlength='15'></td>";
	}
	var itemTotalCol = "<td><input type='text' id='itemTotal" + newSuffix + "' name='invoiceItemList[" + newSuffix + "].itemTotalAmountWithoutVat' class='format-currency form-control disable' readonly='readonly'></td>";
    var delBtnCol = "<td style='text-align: center;'><i id='deleteRow" + rowCount + "' title='" + $("#deleteRow1").attr("title") + "' class='fa fa-trash-o remove-row'></i></td>";
	
	$('#' + tableID + ' tr:last').after("<tr suffix='" + newSuffix + "'>"
										   + idxCol
										   + promotionCol
										   + itemCodeCol
										   + itemNameCol
										   + unitCol
										   + qtyCol
										   + itemPriceCol
										   + itemVatCol
										   + itemTotalCol
										   + enableItemTotalCol
										   + delBtnCol
			                           + "</tr>");
	
	addListener(newSuffix);
	setOnDeleteRowClick(rowCount);
	if (hasDiscountItem()){
		resetVatValue(newSuffix);
	}
}

function setTotalGoodsPrice() {
	var total = 0;
	var totalVat = 0;
	var totalPaid = 0;
	
	var isDiscountInv = hasDiscountItem();
	var vatPercentDiscountInv = null; 
	var vatLabel = null;
	
	$("#dataTable tbody > tr").each(function (i, v) {
		var suffix = $(this).attr('suffix');
		var itemTotal = $("#itemTotal" + suffix).autoNumeric('get');
		var itemVatPercent = $("#itemVat" + suffix).val();
		if (vatLabel == null){
			vatLabel = $('#itemVat' + suffix  + ' option:selected').text();
		}
		var itemIsPromotion = false;
		if (document.getElementById("itemPromotion" + suffix)){
			itemIsPromotion = ($('#itemPromotion' + suffix).val() == '1');
		}
		var itemVatPercent = $("#itemVat" + suffix).val();
		if (!itemVatPercent || isNaN(itemVatPercent)) {
			itemVatPercent = '0';
		}
		itemVatPercent = parseFloat(itemVatPercent);
		if (itemVatPercent < 0){
			itemVatPercent = 0;
		}
		if (itemTotal != '') {
			if (!itemIsPromotion){
				total += parseFloat(itemTotal);
			}
			if (isDiscountInv){
				if (itemVatPercent > 0){
					vatPercentDiscountInv = itemVatPercent;
					if (!itemIsPromotion){
						vatLabel = $('#itemVat' + suffix  + ' option:selected').text();
					}
				}
			}else{
				if (itemVatPercent != null &&  itemVatPercent != '') {
					totalVat += itemTotal * itemVatPercent / 100;
				}else{
					totalVat += 0;
				}
			}
		}
    });
	
	if (isDiscountInv){
		var amountDiscount = parseFloat($("#discountAmount").autoNumeric('get'));
		total -= amountDiscount;
		
		var totalForVatVnd = total; 
		if (document.getElementById('currencies') && $("#currencies").val() != 'VND') {
			var exchangeRate = parseFloat($("#exchangeRate").autoNumeric('get'));
			if (isNaN(exchangeRate)){
				exchangeRate = 0;
			}
			totalForVatVnd = total*exchangeRate;
			if (totalForVatVnd > 10000000000000) {
				totalForVatVnd = 10000000000001;
			}
		}
		
		if (vatPercentDiscountInv){
			var newRow = "";
			if (totalForVatVnd > 0){
				var totalVatVnd = totalForVatVnd * vatPercentDiscountInv / 100;
				totalVat = total * vatPercentDiscountInv / 100;
				newRow = $("<tr class='vatCategory'>" +
						"<td class='text-align-left'><input type='hidden' name='invoiceTaxBreakdownList[0].vatCategoryPercentage' value='" + vatPercentDiscountInv + "'/>" + vatLabel + " </td>" +
						"<td></td>" +
						"<td><input type='text' value='" + totalVatVnd + "' name='invoiceTaxBreakdownList[0].vatCatTaxAmt' class='format-currency disable' data-a-sign=' VND' readonly='true'></td>" +
						"<td><input type='text' value='" + totalForVatVnd + "' name='invoiceTaxBreakdownList[0].vatCatTaxableAmt' class='format-currency disable' data-a-sign=' VND' readonly='true'></td>" +
				"</tr>");
			}else{
				totalVat = 0;
				newRow = $("<tr class='vatCategory'>" +
						"<td class='text-align-left'><input type='hidden' name='invoiceTaxBreakdownList[0].vatCategoryPercentage' value='" + vatPercentDiscountInv + "'/>" + vatLabel + " </td>" +
						"<td></td>" +
						"<td><input type='text' value='" + 0 + "' name='invoiceTaxBreakdownList[0].vatCatTaxAmt' class='format-currency disable' data-a-sign=' VND' readonly='true'></td>" +
						"<td><input type='text' value='" + 0 + "' name='invoiceTaxBreakdownList[0].vatCatTaxableAmt' class='format-currency disable' data-a-sign=' VND' readonly='true'></td>" +
				"</tr>");
			}
			$('#summaryTable tr:not([class!="vatCategory"])').remove ();
			$("#totalVatAmountRow").before(newRow);
		}else{
			totalVat = 0;
			$('#summaryTable tr:not([class!="vatCategory"])').remove ();
		}
		autoNumeric();
	}
	
	if (document.getElementById('currencies') && $("#currencies").val() != 'VND') {
		var exchangeRate = $("#exchangeRate").autoNumeric('get');
		var totalVnd = total*exchangeRate;
		if (totalVnd > 10000000000000) {
			totalVnd = 10000000000001;
		}

		if (total < 0){
			$("#validTotalGoods").val('false');
			$("#totalForeign").autoNumeric('set', 0);
			$("#totalVnd").autoNumeric('set', 0);
		}else{
			$("#validTotalGoods").val('true');
			$("#totalForeign").autoNumeric('set', total);
			$("#totalVnd").autoNumeric('set', totalVnd);
		}
		
		totalVat *= exchangeRate;
		totalPaid = totalVnd + totalVat;

		var totalFrn = total;
		if (exchangeRate != 0){
			totalFrn = totalPaid/parseFloat(exchangeRate);
		}
		totalPaid = Math.round(totalPaid);
		
		var currency =  $("#currencies").val();
		var currencyLabel =  " " + BFHCurrenciesList[currency].label;
		
		if (totalFrn < 0){
			$("#totalAmtWithVatInWords").val(DocTienBangChu(0) + currencyLabel);
			$("#amountForPaymentForeign").autoNumeric('set', 0);
		}else{
			$("#totalAmtWithVatInWords").val(DocTienBangChu(totalFrn) + currencyLabel);
			$("#amountForPaymentForeign").autoNumeric('set', totalFrn);
		}
	} else {
		$("#totalForeign").val('');
		if (total < 0){
			$("#validTotalGoods").val('false');
			$("#totalVnd").autoNumeric('set', 0);
		}else{
			$("#validTotalGoods").val('true');
			$("#totalVnd").autoNumeric('set', total);
		}
		$("#amountForPaymentForeign").val('');
		totalPaid = Math.round(total + totalVat);
		
		var currencyLabel =  " đồng";
		if (totalPaid < 0){
			$("#totalAmtWithVatInWords").val(DocTienBangChu(0) + currencyLabel);
		}else{
			$("#totalAmtWithVatInWords").val(DocTienBangChu(totalPaid) + currencyLabel);
		}
	}
	if (document.getElementById('totalVatAmount')){
		$("#totalVatAmount").autoNumeric('set', Math.round(totalVat));
	}
	if (document.getElementById('amountForPaymentVnd')){
		if (totalPaid < 0){
			$("#amountForPaymentVnd").autoNumeric('set', 0);
		}else{
			$("#amountForPaymentVnd").autoNumeric('set', totalPaid);
		}
	}else{
		if (totalPaid < 0){
			$("#totalVnd").autoNumeric('set', 0);
		}else{
			$("#totalVnd").autoNumeric('set', totalPaid);
		}
	}
	checkForWarningAboutPaymentMethod(totalPaid);
	setTaxBreakdown();		
}

function setTaxBreakdown() {
	var isDiscountInv = hasDiscountItem();
	if (!isDiscountInv){
		setTaxBreakdownForNormalInv();
	}
}

function changeItemPromotion(suffix) {
	removeFormError();
    var promotion = $('#itemPromotion' + suffix).is(":checked");

	if (promotion){
		$("#itemVat" + suffix).removeClass('validate[required]');
		
		var itemName = $("#itemName" + suffix).val();
		if (itemName){
			if (itemName.toLowerCase().indexOf("khuyến mại") == -1){
				$("#itemName" + suffix).val('Khuyến mại ' + itemName);
			}
		}else{
			$("#itemName" + suffix).val('Khuyến mại');
		}
		if (!($("#itemPrice" + suffix).is('[readonly]'))){
			if ($("#itemPrice" + suffix).hasClass('validate[required, positive]')){
				$("#itemPrice" + suffix).removeClass('validate[required, positive]');
			}
			if (!$("#itemPrice" + suffix).hasClass('validate[required, funcCall[notNegative]]')){
				$("#itemPrice" + suffix).addClass('validate[required, funcCall[notNegative]]');
			}
		}
		$("#itemTotal" + suffix).val('');
		$("#itemTotal" + suffix).trigger('change');
		$("#itemTotal" + suffix).hide();
		$("#enableItemTotal" + suffix).removeAttr("checked");
		doToggleItemTotal(suffix, false);
		$("#enableItemTotal" + suffix).hide();
	}else{
        setSelect2Validation('itemVat' + suffix, 'validate[required]');
		var itemName = $("#itemName" + suffix).val();
		if (itemName){
			itemName = itemName.replace(/khuyến mại/ig, '');
			$("#itemName" + suffix).val($.trim(itemName));
		}
		if (!($("#itemPrice" + suffix).is('[readonly]'))){
			if ($("#itemPrice" + suffix).hasClass('validate[required, funcCall[notNegative]]')){
				$("#itemPrice" + suffix).removeClass('validate[required, funcCall[notNegative]]');
			}
			if (!$("#itemPrice" + suffix).hasClass('validate[required, positive]')){
				$("#itemPrice" + suffix).addClass('validate[required, positive]');
			}
		}
		$("#itemTotal" + suffix).show();
		$("#enableItemTotal" + suffix).show();
		doToggleItemTotal(suffix, false);
		setTotal(suffix);
	}
	setTotalGoodsPrice();
}

function validateOnChangeToDiscount() {
	var taxs = [];
	var tempvat;
	var prev = '';
	var valid = true;
	$("#dataTable tbody > tr:not([increaserow='true']):not([decreaserow='true'])").each(function () {
		var suffix = $(this).attr('suffix');
		tempvat = $("#itemVat" + suffix).val();
		if (tempvat != null && tempvat != '') {
			taxs[taxs.length] = tempvat;
		}
	});

	$.each(taxs, function(index, value) {
		if (prev != '' && prev != value) {
			valid = false;
		}
		prev = value;
	});
	return valid;
}

function validateOnchangeItemVat() {
	var isValid = true;
	if (hasDiscountItem()) {
		var prev = '';
		var current = '';
		$("#dataTable tbody > tr:not([increaserow='true']):not([decreaserow='true'])").each(function () {
			var suffix = $(this).attr('suffix');
			current = $("#itemVat" + suffix).val();
			if (prev != '' && current != '' && prev != current) {
				isValid = false;
			}
			if (current != '') {
				prev = current;
			}
		});
	}

	return isValid;
}

function hasDiscountItem(){
	return $("#discountAmount").val() != null && $("#discountAmount").val() != '';
}

function resetVatValue(s) {
	$("#dataTable tbody > tr:not([increaserow='true']):not([decreaserow='true'])").each(function () {
		var suffix = $(this).attr('suffix');
		if (s != suffix){
			if ($("#itemVat" + suffix).val() != '') {
				$("#itemVat" + s).select2('val', $("#itemVat" + suffix).val());
				return true;
			}
		}
	});
}