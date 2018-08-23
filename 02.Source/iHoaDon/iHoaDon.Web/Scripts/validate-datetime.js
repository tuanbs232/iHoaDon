(function ($) {
    $.validator.addMethod('vnDatetime',
                          function (value, element, params) {
                              if (value == null || value == '') {
                                  return true;
                              }
                              var val = $('#' + params['propertyname']).val();
                              $.global.preferCulture("vi-VN");
                              if ($.global.parseDate(val)) {
                                  return true;
                              }
                              return false;
                          });

    $.validator.unobtrusive.adapters.add('vndatetime', ['propertyname'], function (options) {
        options.rules['vnDatetime'] = options.params;
        if (options.message) {
            options.messages['vnDatetime'] = options.message;
        }
    });
} (jQuery));