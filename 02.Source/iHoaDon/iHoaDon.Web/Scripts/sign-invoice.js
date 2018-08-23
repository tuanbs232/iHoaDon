/*
 * call applet for getting certificates from token
 */

/***
 * callApplet: gọi khi nhấn nút [Ký và xác thực]
 * @param confirmMes: msg confirm thực thi action
 */
function callApplet(confirmMes) {
	var form = '#' + $("#formId").val();
	
	if ($("#formId").val() == "certForm" || $("#formId").val() == "invoiceTemplateform"  || $("#formId").val() == "voidInvoiceTemplateform") {
		showDivApplet("DigitalSignatureApplet");
	} else if ( validateForm() ) {
		var $buyerTaxCode = $("input[name='buyerTaxCode']").val();
		if (typeof (hasWarningAboutPaymentMethod) !== 'undefined' && hasWarningAboutPaymentMethod && (typeof (isDeletionInvoice) !== 'undefined' && !isDeletionInvoice)){
	    	if ($("#taxCode").val() != '') {
    	    	$('#warningModal').find($('p')).text(warningAboutPaymentMethod);
    	    	$('#warningModal').modal('show');
			} else {
    	    	$('#confirmModal').find($('p')).text(warningAboutPaymentMethod).append(warningAboutPaymentMethodConfirmSave);
    			$("#btnYes").html($("#yesLabel").val());
    			$("#btnNo").html($("#noLabel").val());
    	    	
    			$('#confirmModal').modal('show');

    	    	$('#btnYes').off("click").click(function() {
    	    		$('#confirmModal').modal('hide');
    	    		showPleaseWaitDialog();
        			$('#<c:out value="${param.formId}"/>').submit();
    	    	});
			}
		} else if ((typeof (isDeletionInvoice) !== 'undefined' && !isDeletionInvoice) && $buyerTaxCode != '' && $buyerTaxCode != undefined && isWarningBuyerTotalAmountWithinDay($buyerTaxCode)) {
			$('#confirmModal').find($('p')).text(warningMessageTB0012).append(warningAboutPaymentMethodConfirmSave);
			$("#btnYes").html($("#yesLabel").val());
			$("#btnNo").html($("#noLabel").val());
	    	
			$('#confirmModal').modal('show');
		} else{
			$('#confirmModal').modal('show');
			$('#confirmModal').find($('p')).text(confirmMes);
			$("#btnYes").html($("#yesLabel").val());
			$("#btnNo").html($("#noLabel").val());
		}
		
		$('#btnYes').off("click").click(function() {
			$('#confirmModal').modal('hide');

			if ($("#formId").val() == "bulkAdjustmentForm") {
				$('#btnSign').hide();
				$('#btnSave').hide();
			}
			
			showDivApplet("DigitalSignatureApplet");			
		});
	}
}

/***
 * Hiển thị applet ra trình duyệt, chỉ dành để đọc chứng thư số
 * @param appletName: tên java class
 */

function showDivApplet(appletName) {
	// display loading image for ajax
	if (appletName == "DigitalSignatureApplet") {
		showPleaseWaitDialog();
	}
		
	// get certificate from token
	var appletStr = "<applet id=\"dsApplet\" code=\"com.speedframework.digitalsignature.applet." + appletName + "\" height=\"1\" width=\"1\" mayscript ";
	appletStr += "archive=\""+ $("#contextPath").val() +"/scripts/SignApplet40.jar\">";
	appletStr += "</applet>";
	$("#divApplet").html(appletStr);
}

/*
 * send usb's certificate from applet to server for verification
 */

/***
 * verifyCertificate
 * @param usbCert: string: số serialnumber, called by applet
 */
function verifyCertificate(usbCert) {	
	// for seller's signature
	if($("#formId").val() == "certForm") {
		displayOptionsToSign($("#serialNos").val().split(","));
	} else if (usbCert !== "") {
		$.ajax({
			type: "get",			
			url: $("#contextPath").val() + "/ajax/verifyCertificate",
			dataType: "json",
			data: {
				certificate : usbCert
			},
			success: function(data, status, jqXHR) {

				if (typeof(data.cert)  !== "undefined" && data.cert !== "") {
					displayOptionsToSign(data.cert.split(","));
				} else { // error
					showMessage( $("#errActivedCert").val() );					
				}
			}, error: function (xhr, status) {
		    	showMessage( $("#errCertify").val() );		    	
		    }
		});
	} else { // error
		showMessage( $("#errUsb").val() ); // báo lỗi là chưa cắm
	}
}

function displayOptionsToSign(cert) {

	var $certificates = document.dsApplet.getCertificates();
	var cnt = 0;
	var currentCert = "";
	var htmlStr = "";
	for (var i=0; i < cert.length; i++) {
		for (var j = 0; j < $certificates.size(); j++) {
		    if (cert[i] == $certificates.get(j).getSerialNumber()) {
		    	cnt++;
		    	currentCert = $certificates.get(j).getAlias();
                // trong trường hợp 1 user nhiều chứng thư số và các TOKEN cùng đang được cắm vào máy
                // thì khởi tạo luôn msg chọn chứng thư số
		    	htmlStr += '<label class="radio inline"><input type="radio" value="'
		    			+ $certificates.get(j).getAlias()
		    			+ '" name="cert"><label>'
		    			+ $certificates.get(j).getSubject()
		    			+ '</label></label>';
		    }						   
		}
	}
	
	if (cnt > 1) { // display list of certificate
		hidePleaseWaitDialog();

		$("#dsModal").find('.cert-list').html(htmlStr);
		$("#dsModal").modal("show");
	} else { // if there is only 1 certificate, call applet to signing directly
		signInvoice(currentCert);
	}

}

/***
 * callSignatureApplet: hiển thị applet dùng để ký điện tử
 * @param appletName
 * @param signId
 * @param content
 * @param invId
 * @param selectedCertificate
 */
function callSignatureApplet(appletName, signId, content, invId, selectedCertificate) {
	var appletStr = "<applet id=\"signApplet\" code=\"com.speedframework.digitalsignature.applet." + appletName + "\" height=\"1\" width=\"1\" mayscript ";
	appletStr += "archive=\""+ $("#contextPath").val() +"/scripts/SignApplet40.jar\">";
	var signContent = content.replace(/&amp;/g,'&amp;amp;');
	signContent = signContent.replace(/'/g,'&#39;');
	appletStr += "<param name=\"contentToSign\" value='" + signContent + "'>";
	appletStr += "<param name=\"signId\" value='" + signId + "'>";
	appletStr += "<param name=\"signNameSpace\" value='ds'>";

    if (typeof invId != 'undefined' && invId !== null) {
        appletStr += "<param name=\"invoiceId\" value='" + invId + "'>";
    }

	appletStr += "<param name=\"selectedCert\" value='" + selectedCertificate + "'>";
	appletStr += "</applet>";
	$("#divSignApplet").html(appletStr);
}

/*
 * for single invoice.
 */

/***
 * đối với trường hợp form HTML hóa đơn: sinh số + sinh XML
* đối với case khác: chỉ đơn giản là sinh XML
* @param $form
* @param cert
*/
function doSingle($form, cert) {

    // lấy số hóa đơn, sau đó tạo luôn XML trong cùng 1 hàm
	var action = $("#contextPath").val() + '/ajax/getInvoiceNumber';

    // đăng ký mẫu
	if ($("#formId").val() == "invoiceTemplateform") {
		action = $("#contextPath").val() + '/invoiceTemplateform/getXMLData';
	}
    // hủy mẫu
    else if ($("#formId").val() == "voidInvoiceTemplateform") {
		action = $("#contextPath").val() + '/voidInvoiceTemplateform/getXMLData';
	}
    // hủy hóa đơn chưa xác thực từ hệ thống khác
    else if ($("#formId").val() == "voidOutsideInvoiceform") {
		action = $("#contextPath").val() + '/voidOutsideInvoiceform/getXMLData';
	}
    // hủy hóa đơn chưa xác thực trên VANP
    else if ($("#formId").val() == "voidInvoiceResultForm") {
		action = $("#contextPath").val() + '/voidInvoices/getXMLData';
	}
	
	$.ajax({
		type: $form.attr('method'),			
		url: action,
		data: $form.serialize(),
		success: function(data, status, jqXHR) {
            //data là repsonse json
			if (data.valid == "true") {						
				if (data.invNo != "null" && typeof(data.invNo)  !== "undefined") {
					$("#invNumber").val(data.invNo);
					$("#btnSave").remove();
					if ($("#invId").val() == "") {
						$("#btnSign").remove();
					}
				}
				
				// call applet for signing
				$("#invId").val(data.invId);				
                callSignatureApplet("SignSingleApplet", "seller", data.invoice, data.invId, cert);
			} else {	
				showMessage( data.message );
			}					
		}
	});
}

/*
 * for multiple invoice.
 */
/***
 * doMultiple: ký cho nhiều hóa đơn
 * @param $form
 * @param cert
 */
function doMultiple($form, cert) {

    // điều chỉnh định danh theo lô
	if ($("#formId").val() == 'bulkAdjustmentForm') {
		$.ajax({
			type: 'post',			
			url: $("#contextPath").val() + '/bulkAdjustmentform/signInvoices',
			data: $form.serialize(),
			success: function(data, status, jqXHR) {
				if (data.valid == "true") {						
					// call applet for signing
					callSignatureApplet("SignApplet", "seller", data.invoices, data.invIds, cert);
				} else {	
					showMessage( data.message );
				}					
			}
		});
		
	}
    // đăng ký chứng thư số (chọn từ checkbox): trường hợp này ký luôn do nút bấm đã xử lý việc tổ hợp tạo chuỗi xml (bằng ajax) và gán vào xmlCertificates
    // vì action này ko verify cerificate nên chỗ xử lý này khác so với các chỗ còn lại
    else if ($("#formId").val() == 'certForm') {
		callSignatureApplet("SignApplet", "seller", $("#xmlCertificates").val(), $("#serialNos").val(), cert);
	}

    // sinh số hóa đơn cho 1 loạt và tạo luôn XML trong cùng 1 hàm
    else {
		var checkedFlag = true;
		
		$.ajax({
			type: 'get',			
			url: $("#contextPath").val() + '/ajax/getInvoiceNumbers',
			data: {invIds: $("#invIds").val()},
			success: function(data, status, jqXHR) {
				if (data.valid == "true") {						
					// call applet for signing
					callSignatureApplet("SignApplet", "seller", data.invoices, data.invIds, cert);
				} else {	
					showMessage( data.message );
					if (data.templateCode && data.invoiceSeries) {
						$("#invoiceList tbody > tr").each(function() {
							 if ($(this).children('td.templateCode').text().trim() == data.templateCode
									 && $(this).children('td.invSeries').text().trim() == data.invoiceSeries && checkedFlag) {
								$(this).children('td').css('background-color', '#FFE257');
							} 
							if ($(this).find('input.invId').attr('checked')) {
								checkedFlag = false;
							} 
						});
					}
				}					
			}
		});
		
	}
}

/*
 * Send to server for converting invoice data to xml string.
 * Call applet for signing.
 */
/***
 * signInvoice: ký hóa đơn,
 * @param cert
 */
function signInvoice(cert) {

	var $form = $( '#' + $("#formId").val() );
    // <input type="hidden" id="signatureType" value="true"> thì ký lô, false thì ký đơn

	if ($("#signatureType").val()) {
		doMultiple($form, cert);
	} else {
		disableAutoNumeric();
		doSingle($form, cert);
	}	
}

/***
 * Confirm có tiếp tục hay ko?
 */
// đưa event xử lý vào phím btContinue trong trường hợp modal hiển thị danh sách chọn chứng thư số khi cắm nhiều Token
$('#btContinue').click(function(event){
	var selectedCert = $("input[type='radio'][name='cert']:checked");
	if (selectedCert.length > 0) {
		$("#dsModal").modal("hide");
		showPleaseWaitDialog();
	    
		signInvoice(selectedCert.val());		
	} else {
		$('#divErrorCert').css({'display': 'block'});					
		$('#divErrorCert div').html( $("#errNoCert").val() );
	}
});

/***
 * Show message trong modal box
 * @param mes
 */
// hiển thị msg trong trường hợp lỗi
function showMessage(mes) {
	// hide loading image for ajax
	hidePleaseWaitDialog();
	
	$('#divError').css({'display': 'block'});					
	$('#divError div').html( mes );
	$("html, body").animate({ scrollTop: 0 }, "slow");
	
	// format number
	autoNumeric();
}

/***
 * Khi hiển thị kết quả, thì hiển thị button tương ương
 */
function displayButton() {
	if ($('input[name=isAdjusted]').val() == '7') { 
		$("#btCreateInv").attr("href", $("#contextPath").val() + "/deletionform/initialize");
	} else if ($("#formId").val() == "bulkAdjustmentForm") {
		$("#btCreateInv").attr("href", $("#contextPath").val() + "/bulkAdjustmentform/initialize");
	} else {
		$("#btCreateInv").attr("href", $("#contextPath").val() + "/invoiceform/create?method=NewBlankInv&templateCode=" 
				+ $("#templateCode").val() + "&invTypeCode=" + $("#invTypeCode").val());
		
		if ($('input[name=isAdjusted]').val() == 3) {
			$("#btCreateInv").attr("href", $("#contextPath").val() + "/replacementform/initialize");
		} else if ($('input[name=isAdjusted]').val() == 5) {
			$("#btCreateInv").attr("href", $("#contextPath").val() + "/adjustmentform/initialize");
		}else if ($('input[name=isAdjusted]').val() == 9) {
			$("#btCreateInv").attr("href", $("#contextPath").val() + "/discountform/initialize");
		}
	}
}

/***
 * Khi hiển thị kết quả, thì hiển thị kết quả tương ứng, trong trường hợp nhiều hóa đơn
 * @param data: response từ server trả về
 */
function showResult(data) {
	searchForm = $("#searchFormId").val();
	
	$("#mesModal").find('.modal-body').html(data.message);

	if (typeof searchForm != 'undefined' && searchForm !== null) {
		// approve multiple invoice
		$("#mesModal").find('.modal-footer').html('<button class="btn" data-dismiss="modal">' + $('#btExitText').val() + '</button>');		
		
		$('#mesModal').off("hidden").on('hidden', function () {
		    $("body").css("overflow", "auto");
		    $('.modal-backdrop').hide();
		    $("#" + searchForm).submit();
		});
	} else {
		if ($("#formId").val() == "bulkAdjustmentForm") {			
			$("#btViewInv").css({'visibility': 'hidden'});
		}
		
		if (typeof(data.id)  !== "undefined") {				
			href = $("#contextPath").val() + "/viewInvoice?id=" + data.id;
			if (data.status == 2 && $("#hasPermissionToPrintInv").val() == 'true') {
				href += "&printFlag=1";
				$("a#btViewInv").text('Xem in chuyển đổi')
			}
			$("#btViewInv").off("click").click(function(event){
				window.open( href, "", "scrollbars=yes,resizable=yes");
			});				
		}		
	}
}

/***
 * trả kết quả về trong trường hợp 1 hóa đơn
 * @param actionStr
 * @param $form
 */
function doSingleResult(actionStr, $form) {
	$.ajax({
		type: 'post',			
		url: actionStr,
		data: $form.serialize(),	
		success: function(data, status, jqXHR) {	
			showResult(data);
		},
	    error: function (xhr, status) {
	    	$("#mesModal").find('.modal-body').html( $("#errCertify").val() );	        
	    },
	    complete: function(jqXHR, status) {
	    	if($("#hasPermissionToCreateNewInv").val() == "true") {
	    		displayButton();			
	    	} else {
	    		$("#btCreateInv").hide();
	    	}
	    	
	    	// hide loading image for ajax
	    	hidePleaseWaitDialog();
	    	// hide buyer's pop-up
	    	$("#buyerSignModal").modal("hide");
	    	
	    	$("#mesModal").modal("show");
	    }
	});
}

/***
 *  trả kết quả về trong trường hợp nhiều hóa đơn
 * @param actionStr
 * @param $form
 */
function doBatchResult(actionStr, $form) {	
	$.ajax({
		type: 'post',			
		url: actionStr,
		data: $form.serialize(),	
		success: function(data, status, jqXHR) {						
			showResult(data);
		},
	    error: function (xhr, status) {	    	
			$("#mesModal").find('.modal-body').html( $("#errCertify").val() );
	    },
	    complete: function(jqXHR, status) {
	    	if($("#hasPermissionToCreateNewInv").val() == "true") {
	    		displayButton();
	    	} else {
	    		$("#btCreateInv").hide();
	    	}
	    	
	    	// hide loading image for ajax
	    	hidePleaseWaitDialog();	 
	    	
	    	$("#mesModal").modal("show");			
	    }
	});
}

/*
 * Applet calls this after signature is append to content. 
 */

/***
 * Applet calls this after signature is append to content, called by applet
 * @param signedContent, called by Applet
 * @param type
 */
function sendSignedToServer(signedContent, type) {
	//do buyer signature
	if($("#buyerSignature").is(":checked") && type == "seller") {
		doBuyerSignature(signedContent);
	} else {		
		submitSigned(signedContent);
	}	
	
}

/*
 * send signed content to server
 */

/***
 * send signed content to server
 * @param signedContent: xml ký rồi
 */
function submitSigned(signedContent) {
	if ($("#formId").val() == "certForm") {
		$("#signedData").val(signedContent);
		$("#certForm").submit();
	}

    else if ($("#formId").val() == "invoiceTemplateform") {
		$("#signedData").val(signedContent);
		$("#invoiceTemplateform").submit();
	}

    else if ($("#formId").val() == "voidInvoiceTemplateform") {
		$("#signedData").val(signedContent);
		$("#voidInvoiceTemplateform").submit();
	}

    else if ($("#formId").val() == "voidOutsideInvoiceform") {
		$("#signedData").val(signedContent);
		$("#voidOutsideInvoiceform").submit();
	}

	else {
		var $form = $( '#' + $("#formId").val() );
		$("#xmlInvoice").val(signedContent);
		
		var actionStr = $("#contextPath").val() + '/ajax/sendInvoiceToService';
		
		if ($("#formId").val() == "voidInvoiceResultForm") {			
			$('#voidReason').val($('#voidReasonInput').val());
			actionStr = $form.attr('action');
		}
		
		if ($("#signatureType").val()) { // multiple invoice		
			actionStr = $("#contextPath").val() + '/ajax/sendInvoicesToService';
			doBatchResult(actionStr, $form);
		} else { // single invoice
			doSingleResult(actionStr, $form);
		}	
	}
}

/***
 * hiển thị danh sách chứng thư số, called by applet
 */
function signBuyer() {
	
	var $certificates = document.dsApplet.getCertificates();
	var cnt = 0;
	var currentCert = "";
	var htmlStr = "";		
		
	for (j = 0; j < $certificates.size(); j++) {		    
    	cnt++;
    	currentCert = $certificates.get(j).getAlias();
    	htmlStr += '<label class="radio inline"><input type="radio" value="'
    			+ $certificates.get(j).getAlias() + '" name="cert"><label>'
    			+ $certificates.get(j).getSubject() + '</label></label>';	    
	}

	if (cnt >= 1) { // display list of certificate			
		$("#buyerSignModal").find('.cert-list').html(htmlStr);
	}
    else {
		$('#divBuyerError').css({'display': 'block'});					
		$('#divBuyerError div').html( $("#errNoCert").val() );
	}
	
}
/***
 * ký điện tử, called by applet
 * @param signedContent
 */
function doBuyerSignature(signedContent) {
	strLoading = '<div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div>';
	
	$("#btBuyerCancel").off("click").click(function(event){
		submitSigned(signedContent);
		$("#buyerSignModal").html(strLoading);
	});
	
	$("#btnLoadCert").off("click").click(function(event){
		showDivApplet("BuyerSignatureApplet");	
	});
	
	$("#btBuyerSign").off("click").click(function(event){
		var selectedBuyerCert = $("#buyerSignModal").find("input[type='radio'][name='cert']:checked");
		if (selectedBuyerCert.length > 0) {
			callSignatureApplet("SignSingleApplet", "buyer", signedContent, $("#invId").val(), selectedBuyerCert.val());
			$("#buyerSignModal").html(strLoading);
		} else {
			$('#divBuyerError').css({'display': 'block'});					
			$('#divBuyerError div').html( $("#errNoCert").val() ); //báo lỗi nếu chưa chọn chứng thư số người mua
		}			
	});
		
	// hide loading image for ajax
	hidePleaseWaitDialog();
	
	$("#buyerSignModal").modal("show");	
}

/*
 * this'll handle exception from applet
 */
function doException(error) {
	showMessage($("#signErr").val());	
}
