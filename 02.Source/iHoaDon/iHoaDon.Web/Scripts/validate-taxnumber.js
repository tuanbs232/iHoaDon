(function ($) {
    $.validator.addMethod('taxNumber',
                          function (value, element, params) {
                              var val = $('#' + params['propertyname']).val();
                              return f.validation.checkTaxNumberFormat(val);
                          });

    $.validator.unobtrusive.adapters.add('taxnumber', ['propertyname'], function (options) {
        options.rules['taxNumber'] = options.params;
        if (options.message) {
            options.messages['taxNumber'] = options.message;
        }
    });
} (jQuery));