(function($){
    $.fn.validationEngineLanguage = function(){
    };
    $.validationEngineLanguage = {
        newLang: function(){
            $.validationEngineLanguage.allRules = {
                "required": { // Add your regex rules here, you can take telephone as an example
                    "regex": "none",
                    "alertText": "* Dữ liệu bắt buộc phải nhập",
                    "alertTextCheckboxMultiple": "* Vui lòng chọn một tùy chọn",
                    "alertTextCheckboxe": "* Checkbox này bắt buộc",
                    "alertTextDateRange": "* Cả hai trường ngày tháng đều bắt buộc"
                },
                "requiredInFunction": { 
                    "func": function(field, rules, i, options){
                        return (field.val() == "test") ? true : false;
                    },
                    "alertText": "* Giá trị của trường phải là test"
                },
                "dateRange": {
                    "regex": "none",
                    "alertText": "* Không đúng ",
                    "alertText2": "Khoảng ngày tháng",
                    "alertText3": "Từ ngày phải nhỏ hơn đến ngày",
                    "alertText4": "Từ ngày và đến ngày phải cùng tháng"
                },
                "dateTimeRange": {
                    "regex": "none",
                    "alertText": "* Không đúng ",
                    "alertText2": "Khoảng thời gian"
                },
                "minSize": {
                    "regex": "none",
                    "alertText": "* Độ dài của trường này tối thiểu là ",
                    "alertText2": " ký tự"
                },
                "maxSize": {
                    "regex": "none",
                    "alertText": "* Độ dài của trường này tối đa là ",
                    "alertText2": " ký tự"
                },
				"groupRequired": {
                    "regex": "none",
                    "alertText": "* Bạn phải điền một trong những trường sau"
                },
                "min": {
                    "regex": "none",
                    "alertText": "* Giá trị nhỏ nhất là "
                },
                "max": {
                    "regex": "none",
                    "alertText": "* Giá trị lớn nhất là "
                },
                "positive": {
                	"regex": "none",
                	"alertText": "* Giá trị phải lớn hơn 0"
                },
                "notNegative": {
                	"regex": "none",
                	"alertText": "* Giá trị không được nhỏ hơn 0"
                },
                "past": {
                    "regex": "none",
                    "alertText": "* Ngày không được sau "
                },
                "future": {
                    "regex": "none",
                    "alertText": "* Ngày phải sau "
                },	
                "certDate": {
                	"regex": "none",
                	"alertText": "* Ngày phải lớn hơn hoặc bằng ngày hóa đơn xác thực gốc "
                },	
                "maxCheckbox": {
                    "regex": "none",
                    "alertText": "* Tối đa ",
                    "alertText2": " số tùy chọn được cho phép"
                },
                "minCheckbox": {
                    "regex": "none",
                    "alertText": "* Vui lòng chọn ",
                    "alertText2": " các tùy chọn"
                },
                "equals": {
                    "regex": "none",
                    "alertText": "* Giá trị các trường không giống nhau"
                },
                "notEquals": {
                    "regex": "none",
                    "alertText": "* Giá trị các trường không được phép giống nhau",
                    "alertText1": "* Mật khẩu mới không được phép trùng với mật khẩu cũ"
                },
                "creditCard": {
                    "regex": "none",
                    "alertText": "* Số thẻ tín dụng sai"
                },
                "phone": {
                    // credit: jquery.h5validate.js / orefalo
                    "regex": /^([\+][0-9]{1,3})?([\(][0-9]{1,6}[\)])?([0-9]{1,32})(([A-Za-z \:]{1,11})?[0-9]{1,4}?)$/,
                    "alertText": "* Số điện thoại sai"
                },
                "fax": {
                    // credit: jquery.h5validate.js / orefalo
                    "regex": /^([\+][0-9]{1,3})?([\(][0-9]{1,6}[\)])?([0-9]{1,32})(([A-Za-z \:]{1,11})?[0-9]{1,4}?)$/,
                    "alertText": "* Số fax sai"                    	
                },
                "email": {
                    // HTML5 compatible email regex ( http://www.whatwg.org/specs/web-apps/current-work/multipage/states-of-the-type-attribute.html#    e-mail-state-%28type=email%29 )
                    "regex": /^[_A-Za-z0-9-\+]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9]+)*(\.[A-Za-z]{2,})$/,
                    "alertText": "* Địa chỉ thư điện tử sai"
                },
                "integer": {
                    "regex": /^[\-\+]?\d+$/,
                    "alertText": "* Không đúng là số nguyên"
                },
                "number": {
                    // Number, including positive, negative, and floating decimal. credit: orefalo
                    "regex": /^[\-\+]?((([0-9]{1,3})([,][0-9]{3})*)|([0-9]+))?([\.]([0-9]+))?$/,
                    "alertText": "* Không đúng là số thập phân"
                },
                "date": {
                    "regex": /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/,
                    "alertText": "* Ngày sai, phải có định dạng DD/MM/YYYY"
                },
                "ipv4": {
                    "regex": /^((([01]?[0-9]{1,2})|(2[0-4][0-9])|(25[0-5]))[.]){3}(([0-1]?[0-9]{1,2})|(2[0-4][0-9])|(25[0-5]))$/,
                    "alertText": "* Địa chỉ IP sai"
                },
                "url": {
                    "regex": /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i,
                    "alertText": "* URL sai"
                },
                "onlyNumberSp": {
                    "regex": /^[0-9\ ]+$/,
                    "alertText": "* Chỉ điền số"
                },
                "onlyLetterSp": {
                    "regex": /^[a-zA-Z\ \']+$/,
                    "alertText": "* Chỉ điền chữ"
                },
                "onlyLetterNumber": {
                    "regex": /^[0-9a-zA-Z]+$/,
                    "alertText": "* Chỉ điền số và chữ cái không dấu"
                },
                "parameterName": {
                	"regex": /^[0-9a-zA-Z]+([_]{0,1})[0-9a-zA-Z]+$/,
                	"alertText": "* Mã tham số không đúng định dạng"
                },
                "excludeSpecialChars": {
                    "regex": /^[^!@#$%^*]+$/,
                    "alertText": "* Không được chứa các ký tự đặc biệt: !@#$%^*"
                },
                "excludeWhitespace": {
                	"regex": /^[^\s]+$/,
                	"alertText": "* Không được chứa khoảng trắng"
                },
                // --- CUSTOM RULES -- Those are specific to the demos, they can be removed or changed to your likings
                "ajaxUserCall": {
                    "url": "ajaxValidateFieldUser",
                    // you may want to pass extra data on the ajax call
                    "extraData": "name=eric",
                    "alertText": "* Tên này được dùng",
                    "alertTextLoad": "* Đang xác nhận, vui lòng chờ"
                },
				"ajaxUserCallPhp": {
                    "url": "phpajax/ajaxValidateFieldUser.php",
                    // you may want to pass extra data on the ajax call
                    "extraData": "name=eric",
                    // if you provide an "alertTextOk", it will show as a green prompt when the field validates
                    "alertTextOk": "* Tên người dùng này có thể dùng được",
                    "alertText": "* Tên người dùng này đã được sử dụng",
                    "alertTextLoad": "* Đang xác nhận, vui lòng chờ"
                },
                "ajaxNameCall": {
                    // remote json service location
                    "url": "ajaxValidateFieldName",
                    // error
                    "alertText": "* Tên này được dùng",
                    // if you provide an "alertTextOk", it will show as a green prompt when the field validates
                    "alertTextOk": "* Tên này có thể dùng",
                    // speaks by itself
                    "alertTextLoad": "* Đang xác nhận, vui lòng chờ"
                },
				 "ajaxNameCallPhp": {
	                    // remote json service location
	                    "url": "phpajax/ajaxValidateFieldName.php",
	                    // error
	                    "alertText": "* Tên này được dùng",
	                    // speaks by itself
	                    "alertTextLoad": "* Đang xác nhận, vui lòng chờ"
	                },
                "validate2fields": {
                    "alertText": "* Vui lòng nhập vào HELLO"
                },
	            //tls warning:homegrown not fielded 
                "dateFormat":{
                    "regex": /^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$|^(?:(?:(?:0?[13578]|1[02])(\/|-)31)|(?:(?:0?[1,3-9]|1[0-2])(\/|-)(?:29|30)))(\/|-)(?:[1-9]\d\d\d|\d[1-9]\d\d|\d\d[1-9]\d|\d\d\d[1-9])$|^(?:(?:0?[1-9]|1[0-2])(\/|-)(?:0?[1-9]|1\d|2[0-8]))(\/|-)(?:[1-9]\d\d\d|\d[1-9]\d\d|\d\d[1-9]\d|\d\d\d[1-9])$|^(0?2(\/|-)29)(\/|-)(?:(?:0[48]00|[13579][26]00|[2468][048]00)|(?:\d\d)?(?:0[48]|[2468][048]|[13579][26]))$/,
                    "alertText": "* Ngày sai"
                },
                //tls warning:homegrown not fielded 
				"dateTimeFormat": {
	                "regex": /^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])\s+(1[012]|0?[1-9]){1}:(0?[1-5]|[0-6][0-9]){1}:(0?[0-6]|[0-6][0-9]){1}\s+(am|pm|AM|PM){1}$|^(?:(?:(?:0?[13578]|1[02])(\/|-)31)|(?:(?:0?[1,3-9]|1[0-2])(\/|-)(?:29|30)))(\/|-)(?:[1-9]\d\d\d|\d[1-9]\d\d|\d\d[1-9]\d|\d\d\d[1-9])$|^((1[012]|0?[1-9]){1}\/(0?[1-9]|[12][0-9]|3[01]){1}\/\d{2,4}\s+(1[012]|0?[1-9]){1}:(0?[1-5]|[0-6][0-9]){1}:(0?[0-6]|[0-6][0-9]){1}\s+(am|pm|AM|PM){1})$/,
                    "alertText": "* Ngày sai hoặc định dạng ngày sai",
                    "alertText2": "Định dạng đúng là: ",
                    "alertText3": "mm/dd/yyyy hh:mm:ss AM|PM hay ", 
                    "alertText4": "yyyy-mm-dd hh:mm:ss AM|PM"
	            },
	            // invoice template code
                 "templateCode": { 
                    "alertText": "* Ký hiệu mẫu hóa đơn không hợp lệ"
                },
	            //Invoice series format
                "invSeries":{
                    "regex": /^((?!F)(?!I)(?!J)(?!O)(?!W)(?!Z)([A-Z]))((?!F)(?!I)(?!J)(?!O)(?!W)(?!Z)([A-Z]))[\/]\d{2}[E]$/,
                    "alertText": "* Ký hiệu hóa đơn không hợp lệ"
                },
	            // check end number
                "endNumber": { 
                  "alertText": "*  Trường Đến số phải lớn hơn hoặc bằng trường Từ số",
                  "alertText1": "*  Trường Từ số phải nhỏ hơn hoặc bằng trường Đến số"
               },
	            // check tax code
               "taxCode": { 
                  "alertText": "* Mã số thuế không hợp lệ"
              },
              // taxCode of seller and buyer must be different
              "sameTaxCode": { 
            	  "alertText": "* MST người mua không được trùng với MST người bán"
              },
              // bankAcc of seller and buyer must be different
              "sameBankAcc": { 
            	  "alertText": "* Tài khoản của người mua và của người bán phải khác nhau"
              },
	            // check currency code
              "currencyCode": { 
                 "alertText": "* Mã ngoại tệ không hợp lệ"
             },
             "inputEditRow": {
                 "regex": "none",
                 "alertText": "Phải có ít nhất một thay đổi trong dòng điều chỉnh"
             }
            };
            
        }
    };

    $.validationEngineLanguage.newLang();
    
})(jQuery);
