$(document).ready(function () {
    CreatControl();
});

function CreatControl() {
    $('#ExtensionPlaceHolder').append("<input type='text' id='holderDataInputToExtension' style='visibility:hidden' style='display:none;' ClientIDMode='Static' name='' ></input>");
    $('#ExtensionPlaceHolder').append("<input type='text' id='hrSignedData' style='visibility:hidden' style='display:none;' ClientIDMode='Static' name=''></input>"); //ResultExtensionPlaceHolder
    $('#ExtensionPlaceHolder').append("<button type='button' id='actionToExtensionProcess' style='visibility:hidden' style='display:none;' ClientIDMode='Static' name=''></button>"); //OnClientClick, onclick = 'WaitingDataresponse();' 
}

function ProcessData(inputData) {
    var dataResponse = "";
    try {
        document.getElementById("holderDataInputToExtension").value = inputData;
        var event = document.createEvent('Event');
        event.initEvent('SendToNativeApp', true, true);
        document.dispatchEvent(event);
    } catch (e) {
        console.log("Lỗi: " + e);
    }
}

function SignXMLTest(parameters) {
    var result = document.getElementById('hrSignedData').value;
   
    console.log(result);
}


//document.addEventListener("ProcessCallback", function (data) {
//    SignXMLProcess();
//});
var BkavExtensionSigner = {
    SignXML: function(fileToSign, fileSigned, tagSigning, nodeToSign, tagSaveResult, timeSign, serialToken) {
        var dataInput = 'SignXML*' + fileToSign + '*' + fileSigned + '*' + tagSigning + '*' + nodeToSign + '*' + tagSaveResult + '*' + timeSign + '*' + serialToken;
        return ProcessData(dataInput);
    },

    SignXMLBase64: function(b64Xml, tagSigning, nodeToSign, tagSaveResult, timeSign, serialToken) {
        //var dataInput = 'SignXML*' + b64Xml + '*' + tagSigning + '*' + nodeToSign + '*' + tagSaveResult + '*' + timeSign + '*' + serialToken;
        var dataInput = 'SignXMLBase64*' + b64Xml + '*' + tagSigning + '*' + nodeToSign + '*' + tagSaveResult + '*' + timeSign + '*' + serialToken;
        return ProcessData(dataInput);
    },

    SignPDF: function(fileInput, fileSigned, timeSign, serialCert) {
        var dataInput = 'SignPDF*' + fileInput + '*' + fileSigned + '*' + timeSign + '*' + serialCert;
        return ProcessData(dataInput);
    },

    SignPDFBase64: function(b64Pdf, signTime, serialCert) {
        var dataInput = "SignPDF*" + b64Pdf + '*' + signTime + '*' + serialCert;
        return ProcessData(dataInput);
    },

    SignOffice: function(fileIn, fileOut, serialCert) {
        var dataInput = "SignOffice*" + fileIn + '*' + fileOut + '*' + serialCert;
        return ProcessData(dataInput);
    },

    SignOfficeBase64: function(b64Office, serialCert) {
        var dataInput = "SignOffice*" + b64Office + '*' + serialCert;
        return ProcessData(dataInput);
    },

    VerifyXML: function(xmlSigned, timeCheck) {
        var dataInput = "VerifyXML*" + xmlSigned + '*' + timeCheck;
        return ProcessData(dataInput);
    },

    VerifyPDF: function(pdfSigned, timeCheck) {
        var dataInput = "VerifyPDF" + pdfSigned + '*' + timeCheck;
        return ProcessData(dataInput);
    },

    VerifyOFFICE: function(officeSigned, timeCheck) {
        var dataInput = 'VerifyOFFICE*' + officeSigned + '*' + timeCheck;
        return ProcessData(dataInput);
    },

    //Utils function
    GetAllCert: function(filter, value) {
        var dataInput = "GetAllCert*" + filter + '*' + value;
        return ProcessData(dataInput);
    },

    CheckOCSPStatus: function(b64Cert, urlOCSP, timeCheck) {
        var dataInput = 'CheckOCSPStatus*' + b64Cert + '*' + urlOCSP + '*' + timeCheck;
        return ProcessData(dataInput);
    },

    CheckCRL: function(b64Cert, timeCheck) {
        var dataInput = 'CheckCRL*' + b64Cert + '*' + timeCheck;
        return ProcessData(dataInput);
    },

    CheckCertValidTime: function(b64Cert, timeCheck) {
        var dataInput = 'CheckCertValidTime*' + b64Cert + timeCheck;
        return ProcessData(dataInput);
    },

    CheckToken: function(serial) {
        var dataInput = 'CheckToken*' + serial;
        return ProcessData(dataInput);
    },

    Base64Encode: function(input) {
    },

    Base64Decode: function(input) {
    },

    //PdfUtils
    ReadPDFBase64ToText: function(pdfBase64) {
        var dataInput = 'ReadPDFBase64ToText*' + pdfBase64;
        return ProcessData(dataInput);
    },

    ReadPDFFileToText: function(pdfFile) {
        var dataInput = 'ReadPDFFileToText*' + pdfFile;
        return ProcessData(dataInput);
    },

    ReadFormFieldsToText: function(pdfFile) {
        var dataInput = 'ReadFormFieldsToText*' + pdfFile;
        return ProcessData(dataInput);
    },

    FileBrowser: function(typeFile) {
        var dataInput = 'FileBrowser*' + typeFile;
        return ProcessData(dataInput);
    },

    SetAESKey: function(keyAES) {
        var dataInput = 'SetAESKey*' + keyAES;
        return ProcessData(dataInput);
    },
    SetUsePKCS11: function(iSetUsePkcs11) {
        var dataInput = 'SetUsePKCS11*' + iSetUsePkcs11;
        return ProcessData(dataInput);
    },
    ReadFileToBase64: function(pathFile) {
        var dataInput = 'ReadFileToBase64*' + pathFile;
        return ProcessData(dataInput);
    },
    GetCertIndex: function(serial) {
        var dataInput = 'GetCertIndex*' + serial;
        return ProcessData(dataInput);
    },
    ExtensionValid: function () {
        var dataInput = 'ExtensionValid*1';
        return ProcessData(dataInput);
    },
    GetDataReturn: function() {
        return document.getElementById("holderDataOutPutFromExtension").value;
    },
    SetDLLName: function (dllName) {
        if (dllName == "") {
            dllName = "BkavCA";
        }
        var dataInput = 'SetDLLName*' + dllName;
        return ProcessData(dataInput);
    }
};
