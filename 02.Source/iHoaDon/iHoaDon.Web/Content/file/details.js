$(document).ready(function () {
    $("#qrcodeContent").val($("#SellerTaxCode").val()
			+ ";" + $("#BuyerTaxCode").val()
			+ ";" + $("#InvoiceIssuedDate").val()
			+ ";" + $("#OriginalInvoiceId").val()
			+ ";" + $("#TotalVATAmount").val()
            + ";" + $("#AmountForPayment").val()
            + ";" + $("#SellerSignature").val()
            + ";" + $("#InvoiceID").val()
            + ";" + $("#CertifiedDate").val()
            + ";" + $("#AntiFraudCode").val()
            + ";" + $("#PsdPublickey").val());
    
    $("#qrcodeTable").qrcode({
        text: $("#qrcodeContent").val(),
        typeNumber: 12,
        moduleCount : '101x101',
        errorCorrectLevel: 'M',
        width: 100,
        height: 100
    });
 
});