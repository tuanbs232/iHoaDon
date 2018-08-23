var listErrorJSON =
    {
        "MA==": "Hết hạn bản quyền SecureBB",//0
        "MQ==": "Ký thành công",//1
        "Mg==": "Lỗi đọc dữ liệu đầu vào. Bạn vui lòng kiểm tra lại dữ liệu đầu vào.",//2
        "Mw==": "Chứng thư số chưa được đăng ký với Cơ quan Thuế. Bạn vui lòng bấm <a style='color:blue;text-decoration:underline;' href='https://etax.noptokhai.vn/Form/ChangeCAPlugin' target='_blank'><b>vào đây</b></a> để thực hiện thay đổi Chứng thư số. Bạn có thể xem hướng dẫn xử lý chi tiết <a style='color:blue;text-decoration:underline;' href='http://noptokhai.vn/ke-khai-thue-qua-mang/huong-dan-xu-ly-loi-chung-thu-so-chua-duoc-dang-ky-voi-co-quan-thue/507' target='_blank'><b>tại đây</b></a>.",//3
        "NA==": "File pdf đầu vào đã bị mã hóa không thể ký.",//4
        "NQ==": "Lỗi trong quá trình ký : khi plugin bị crash.",//5
        "Ng==": "Lỗi lưu kết quả.",//6
        "Nw==": "Serial chữ ký số đầu vào sai quy định.",//7
        "MTE=": "File đầu vào lớn hơn quy định(file đầu vào lớn hơn 20Mb).",//11
        "MTI=": "Không có chữ ký số đã đăng ký trong token."//12
    };

function plugin0() {
    return document.getElementById('plugin0');
}

plugin = plugin0;

function addEvent(obj, name, func) {
    if (obj.attachEvent) {
        obj.attachEvent("on" + name, func);
    } else {
        obj.addEventListener(name, func, false);
    }
}

function load() {
    addEvent(plugin(), 'test', function () {
        if (!checkIeBrowser()) {
            console.log("Received a test event from the plugin.");
            console.log("Plugin loaded!");
        }
    });
}



//Kiểm tra xem plugin đã được cài đặt chưa
function pluginValid() {
    return plugin().valid;
}

function ReadPDFBase64ToText(base64FileIn) {
    var content;
    try {
        content = plugin().ReadPDFBase64(base64FileIn);
        return content;
    } catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }
}

// check cert index.
// neu=-1 thi la khong co cert.
function GetCertIndex(serial) {
    var index;
    if (serial != null && serial.length > 0) {
        try {
            index = plugin().GetCert(serial);
            return index;
        } catch (e) {
            if (!checkIeBrowser()) {
                console.log(e);
            }
        }
    }
    return -1;
}

function GetCertExtension(serial) {
    try {
        BkavExtensionSigner.GetCertIndex(serial);
    }
    catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }
}

//chọn file tờ khai
function chooseFile() {
    try {
        var iCheck = checkBrowser();
        if (iCheck == 1) {
            BkavExtensionSigner.FileBrowser("XML");
        } else {
            var rsPlugin = plugin().FileBrowser("XML");
            document.getElementById('hrSignedData').value = rsPlugin;
            var event = document.createEvent('Event');
            event.initEvent('ProcessCallback', true, true);
            document.dispatchEvent(event);
        }
    } catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }
}

//gủi url của file đã chọn 
function sendFilePath(path) {
    try {
        if (path != '') {
            var iCheck = checkBrowser();
            if (iCheck == 1) {
                BkavExtensionSigner.ReadFileToBase64(path);
            } else {
                var base64 = plugin().ReadFileToBase64(path);
                document.getElementById('hrSignedData').value = base64;
                var event = document.createEvent('Event');
                event.initEvent('ProcessCallback', true, true);
                document.dispatchEvent(event);
            }
        } else {
            $("#thongbaoloi").html("Máy tính của bạn chưa có đầy đủ môi trường kê khai.<br/>Bạn hãy bấm vào nút <b>Hướng dẫn cài đặt</b> dưới đây để tải và cài đặt phần mềm hỗ trợ kê khai.<br/>Sau khi cài đặt xong, bạn hãy đóng trình duyệt và đăng nhập lại để kê khai thuế qua mạng.");
        }
    } catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }
}

function ClearTimeOut(timeOut) {
    clearTimeout(timeOut);
}

////Lấy thông tin chữ ký số của khách hàng
//function GetAllCert() {
//    showProcesdialog("Đang lấy thông tin chứng thư số");
//    setTimeout(function () { caculatorProgress(87, 10) }, 10);
//    var filter = "SerialNumber";
//    var value = "";
//    //  clearTimeout(timeLoad);
//    try {
//        if (checkBrowser() == 1) {
//            BkavExtensionSigner.GetAllCert(filter, "1");
//        } else {
        
//            var rsPlugin = plugin().GetAllSerial(filter, value); 
//            document.getElementById('hrSignedData').value = rsPlugin;
//            var event = document.createEvent('Event');
//            event.initEvent('GetAllCert', true, true);
//            document.dispatchEvent(event);
//        }
//    } catch (e) {
//        if (!checkIeBrowser()) {
//            console.log(e);
//        }
//    }
//}

function GetAllCertSign() {
    var filter = "SerialNumber";
    var value = "";
    //  clearTimeout(timeLoad);
    try {
        if (checkBrowser() == 1) {
            BkavExtensionSigner.GetAllCert(filter, "1");
        } else {
            var rsPlugin = plugin().GetAllSerial(filter, value);
            document.getElementById('hrSignedData').value = rsPlugin;
            var event = document.createEvent('Event');
            event.initEvent('ProcessCallback', true, true);
            document.dispatchEvent(event);
        }
    } catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }
}

// Lấy toàn bộ cert trong store dưới dạng chuỗi xml.
function GetAllSerial() {

    var allserial;
    try {
        allserial = plugin().GetAllSerial("SerialNumber", "");
        return allserial;
    } catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }
}


//kiểm tra xem có phải trình duyệt IE không
function checkIeBrowser() {
    return navigator.userAgent.indexOf("MSIE") != -1;
}

//kiểm tra trình duyệt
function checkBrowser() {
    if (navigator.userAgent.indexOf("Chrome") != -1) {
        return 1;
    }
    else if (navigator.userAgent.indexOf("Opera") != -1) {
        return 2;
    }
    else if (navigator.userAgent.indexOf("Firefox") != -1) {
        return 3;
    }
    else if ((navigator.userAgent.indexOf("MSIE") != -1) || (!!document.documentMode == true)) //IF IE > 10
    {
        return 4;
    }
    else {
        return 5;
    }
}
var strDll = "BkavCAv2S,bkavcaetoken,bkavcsp,BkavCA,beidcsp,beidpkcs11D,nca_v4,viettel-ca,vnptca_p11_v6.dll,vnptca_p11_v6_s.dll,vnpt-ca_csp11,vnpt-ca_v34,vnpt-ca_cl_v1,wdsafe3,CA2_v34,CA2_csp11,wdsafe3,st3csp11.dll,viettel-ca_v1,viettel-ca_v2,viettel-ca_v3,viettel-ca_v4,viettel-ca_v5,viettel-ca_v6,viettel-ca_s,viettel-ca_v2_csp,viettel-ca_v2_csp.EN,viettel-ca_v2_csp.VN,viettel-ca_v2_s,fptca_v3,fptca_v3_s,fpt-ca,fpt-ca-stx,CKCA,safe-ca,Vina-CA,Vina-CAv3,Vina-CA_s,Vina-CAv4,Vina-CAv5,NEWTEL-CA,vdctdcsp11,ShuttleCsp11_3003,ngp11v211,st3csp11,gclib,psapkcs,ostc1_csp11,etpkcs11,U1000AUTO,eToken,ostt1_csp11,ostt2_csp11,ostt3_csp11,ostc2_csp11,nca_eps2k2a,nca_eps2k3a";

// Hàm ký gửi tờ khai
function SignXML64(serial, xmlIN) {
    var tagSigning = "HSoKhaiThue";
    var nodeToSign = "_NODE_TO_SIGN";
    var tagSaveResult = "CKyDTu";
    var signTime = "2015/04/08 10:50:11";

    try {
        if (checkBrowser() == 1)
        {
            BkavExtensionSigner.SetUsePKCS11(1);
            BkavExtensionSigner.SetAESKey("IVAN_BKAV090620151712374930101360697-9999");
            BkavExtensionSigner.SetDLLName("BkavCA");
            BkavExtensionSigner.SignXMLBase64(xmlIN, tagSigning, nodeToSign, tagSaveResult, signTime, serial);
        } else {
            var rsPlugin = plugin().SignXMLBase64(xmlIN, tagSigning, nodeToSign, tagSaveResult, signTime, serial);
            document.getElementById('hrSignedData').value = rsPlugin;
            var event = document.createEvent('Event');
            event.initEvent('ProcessCallback', true, true);
            document.dispatchEvent(event);
        }
    } catch (e)
    {
        if (!checkIeBrowser())
        {
            console.log(e);
        }
    }
}

// Hàm ký các tờ đăng ký.
function SignDK(serial, xmlIN) {
    var tagSigning = "DKyThue";
    var nodeToSign = "DKyThueDTu";
    var tagSaveResult = "CKyDTu";
    var signTime = "2015/08/08 10:50:11";
    try {
        if (checkBrowser() == 1) {
            BkavExtensionSigner.SetUsePKCS11(1);
            BkavExtensionSigner.SetAESKey("TVAN_BKAV090620151712374930101360697-9999");
            BkavExtensionSigner.SetDLLName(strDll);
            BkavExtensionSigner.SignXMLBase64(xmlIN, tagSigning, nodeToSign, tagSaveResult, signTime, serial);
        } else {
            var rsPlugin = plugin().SignXMLBase64(xmlIN, tagSigning, nodeToSign, tagSaveResult, signTime, serial);
            document.getElementById('hrSignedData').value = rsPlugin;
            var event = document.createEvent('Event');
            event.initEvent('ProcessCallback', true, true);
            document.dispatchEvent(event);
        }
    } catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }
}

//thay đổi chữ ký số
function ChangeCA(json, serial) {
    var selected;
    if (json.cert[0] == undefined) {
        // if there is only one cert in token and it doesn't like cert in database.
        var validBDay = json.cert.validTo.split(" ")[0].split("/")[0];
        var validBMonth = json.cert.validTo.split(" ")[0].split("/")[1];
        var validBYear = json.cert.validTo.split(" ")[0].split("/")[2];
        var validBHour = json.cert.validTo.split(" ")[1].split(":")[0];
        var validBMinute = json.cert.validTo.split(" ")[1].split(":")[1];
        var validBSecond = json.cert.validTo.split(" ")[1].split(":")[2];
        var validB = new Date(validBYear, validBMonth - 1, validBDay, validBHour, validBMinute, validBSecond);
        var currentDate = new Date();
        if (validB - currentDate <= 0) {
            $("#thongbaoloi").append("<span style='line-height:1.5p;'><b>Có lỗi xảy ra</b>: Chữ ký số bạn đang sử dụng đã hết hạn. Bạn vui lòng liên hệ với Bkav CA qua số điện thoại <b>1900 54 54 14</b> để gia hạn Chữ ký số!</span>");
        } else {
            selected = json.cert;
            if (selected.serial != serial) {
                $.post("/Account/DownloadChangeCa", $.param({
                    taxNumber: taxNumber,
                    subject: selected.subject,
                    serial: selected.serial,
                    issuer: selected.issuer,
                    fromDate: selected.validFrom,
                    toDate: selected.validTo
                }, true)).done(function (base64) {
                    SignDK(selected.serial, base64);
                }).fail(function (xhr) {
                    closedialogbox();
                });
            }
        }
    }
    else {
        selected = json.cert[0];

        var initalDay = selected.validTo.split(" ")[0].split("/")[0];
        var initalMonth = selected.validTo.split(" ")[0].split("/")[1];
        var initalYear = selected.validTo.split(" ")[0].split("/")[2];
        var initalHour = selected.validTo.split(" ")[1].split(":")[0];
        var initalMinute = selected.validTo.split(" ")[1].split(":")[1];
        var initalSecond = selected.validTo.split(" ")[1].split(":")[2];
        var initalDate = new Date(initalYear, initalMonth, initalDay, initalHour, initalMinute, initalSecond);
        for (var j = 1; j < json.cert.length; j++) {
            var compareDay = json.cert[j].validTo.split(" ")[0].split("/")[0];
            var compareMonth = json.cert[j].validTo.split(" ")[0].split("/")[1];
            var compareYear = json.cert[j].validTo.split(" ")[0].split("/")[2];
            var compareHour = json.cert[j].validTo.split(" ")[1].split(":")[0];
            var compareMinute = json.cert[j].validTo.split(" ")[1].split(":")[1];
            var compareSecond = json.cert[j].validTo.split(" ")[1].split(":")[2];
            var compareDate = new Date(compareYear, compareMonth - 1, compareDay, compareHour, compareMinute, compareSecond);
            if (initalDate < compareDate) {
                initalDate = compareDate;
                selected = json.cert[j];
            }
        }
        if (selected.serial != serial) {
            $.post("/Account/DownloadChangeCa", $.param({
                taxNumber: taxNumber,
                subject: selected.subject,
                serial: selected.serial,
                issuer: selected.issuer,
                fromDate: selected.validFrom,
                toDate: selected.validTo
            }, true)).done(function (base64) {
                SignDK(selected.serial, base64);
            }).fail(function (xhr) {
                closedialogbox();
            });
        }
    }
}

function SignPDF64(serial, pdfBase64, signingTime) {
    var pdfBase64Out = "";
    var index = -1;
    try {
        index = plugin().GetCert(serial);
    } catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }
    if (index == -1) {
        if (!checkIeBrowser()) {
            console.log("Not found cert!");
        }
    } else {
        if (!checkIeBrowser()) {
            console.log("Start siging base 64 PDF");
        }
        try {
            pdfBase64Out = plugin().SignPDFBase64(pdfBase64, signingTime, serial);
            return pdfBase64Out;
        } catch (e) {
            if (!checkIeBrowser()) {
                console.log(e);
            }
        }
    }
}

//tra ve text loi tuong ung voi ma loi
function GetErrorWhenSign(pdfBase64Out) {
    var description = listErrorJSON[pdfBase64Out];
    return description;

}

//ham ky base 64 cho file office.
function SignOffice64(serial, strOfficeBase64) {

    try {
        if (checkBrowser() == 1) {
            BkavExtensionSigner.SetUsePKCS11(1);
            BkavExtensionSigner.SetAESKey("TVAN_BKAV090620151712374930101360697-9999");
            BkavExtensionSigner.SetDLLName(strDll);
            BkavExtensionSigner.SignOfficeBase64(strOfficeBase64, serial);
        } else {
            var rsPlugin = plugin().SignOfficeBase64(strOfficeBase64, serial);
            document.getElementById('hrSignedData').value = rsPlugin;
            var event = document.createEvent('Event');
            event.initEvent('ProcessCallback', true, true);
            document.dispatchEvent(event);
        }
    } catch (e) {
        if (!checkIeBrowser()) {
            console.log(e);
        }
    }

}

function ExtractToJson(content) {
    var base64Matcher = new RegExp("^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{4})$");
    var xmlContent;
    var json = null;
    if (base64Matcher.test(content)) {
        xmlContent = Base64.decode(content);
    } else {
        xmlContent = content;
    }
    if (xmlContent != null && xmlContent != '') {
        json = $.xml2json(xmlContent);
    }
    return json;
}

function GetValueDecodeBase64(sing64) {
    var result = Base64.decode(sing64);
    return result;
}

//decode_base64
var Base64 = {
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;
        input = Base64._utf8_encode(input);
        while (i < input.length) {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }
            output = output + this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) + this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);
        }
        return output;
    },

    decode: function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;
        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {
            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);
            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }
        }
        output = Base64._utf8_decode(output);
        return output;
    },

    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            } else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            } else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }

        return utftext;
    },

    _utf8_decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;
        while (i < utftext.length) {
            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            } else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            } else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }
        return string;
    }
};

function showProcesdialog(label) {
    $("#signDiaglog").dialog({
        height: 100,
        width: 300,
        modal: true,
        autoOpen: true,
        closeOnEscape: false,
        resizable: false,
        draggable: false,
        position: ['top', 120],
        open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog || ui).hide(); },
        title: label + "..."
    });
    var progressbar = $("#signProgress");
    var progressLabel = $(".progress-label");
    $("#signProgress").progressbar({
        value: false,
        change: function () {
            progressLabel.css("visibility", "visible");
            progressLabel.text(progressbar.progressbar("value") + "%");
        }
    });
}

function caculatorProgress(stop, spead, callback) {
    var progressbar = $("#signProgress");
    var val = progressbar.progressbar("value") || 0;
    progressbar.progressbar("value", val + 1);
    if (val < stop) {
        if (typeof callback != "undefined")
            setTimeout(function () { caculatorProgress(stop, callback) }, spead);
        else
            setTimeout(function () { caculatorProgress(stop) }, spead);
    }
    if (val == 100) {
        $("#signDiaglog").dialog("close");
        if (typeof callback != "undefined")
            callback();
    }
}
function closedialogbox() {
    $("#signDiaglog").dialog("close");
}
function showProcesdialog1(label) {
    $("#signDiaglog1").dialog({
        height: 100,
        width: 300,
        modal: true,
        autoOpen: true,
        closeOnEscape: false,
        resizable: false,
        draggable: false,
        position: ['top', 120],
        open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog || ui).hide(); },
        title: label + "..."
    });
    var progressbar = $("#signProgress1");
    var progressLabel = $(".progress-label1");
    $("#signProgress1").progressbar({
        value: false,
        change: function () {
            progressLabel.css("visibility", "visible");
            progressLabel.text(progressbar.progressbar("value") + "%");
        }
    });
}
function caculatorProgress1(stop, callback) {
    var progressbar = $("#signProgress1");
    var val = progressbar.progressbar("value") || 0;
    progressbar.progressbar("value", val + 1);
    if (val < stop) {
        if (typeof callback != "undefined")
            setTimeout(function () { caculatorProgress1(stop, callback) }, 50);
        else
            setTimeout(function () { caculatorProgress1(stop) }, 50);
    }
    if (val == 100) {
        $("#signDiaglog1").dialog("close");
        if (typeof callback != "undefined")
            callback();
    }
}

function closedialogbox1() {
    $("#signDiaglog1").dialog("close");
}