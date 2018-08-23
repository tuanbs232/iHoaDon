/*
* Working not good
*/
String.prototype.toDate = function (format) {
    var normalized = this.replace(/[^a-zA-Z0-9]/g, '-');
    var normalizedFormat = format.toLowerCase().replace(/[^a-zA-Z0-9]/g, '-');
    var formatItems = normalizedFormat.split('-');
    var dateItems = normalized.split('-');

    var monthIndex = formatItems.indexOf("mm");
    var dayIndex = formatItems.indexOf("dd");
    var yearIndex = formatItems.indexOf("yyyy");
    var hourIndex = formatItems.indexOf("hh");
    var minutesIndex = formatItems.indexOf("ii");
    var secondsIndex = formatItems.indexOf("ss");

    var today = new Date();

    var year = yearIndex > -1 ? dateItems[yearIndex] : today.getFullYear();
    if (!year) {
        year = today.getFullYear();
    }
    var month = monthIndex > -1 ? dateItems[monthIndex] - 1 : today.getMonth() - 1;
    if (!month) {
        month = today.getMonth() - 1;
    }
    var day = dayIndex > -1 ? dateItems[dayIndex] : today.getDate();
    if (!day) {
        day = today.getDate();
    }

    var hour = hourIndex > -1 ? dateItems[hourIndex] : today.getHours();
    var minute = minutesIndex > -1 ? dateItems[minutesIndex] : today.getMinutes();
    var second = secondsIndex > -1 ? dateItems[secondsIndex] : today.getSeconds();

    return new Date(year, month, day, hour, minute, second);
};

$('#inputDateOfBirth').on('dp.change dp.show', function (e) {
    $('#update-profile-form').formValidation('revalidateField', 'DateOfBirth');
});

var required = function (fields) {
    var valid = true;
    fields.each(function () { // iterate all
        var $this = $(this);
        if (($this.is(':checkbox') && !$this.is(":checked")) || // checkbox
            (($this.is(':text') || $this.is('textarea')) && !$this.val()) || // text and textarea
            ($this.is(':radio') && !$('input[name=' + $this.attr("name") + ']:checked').length) ||
            ($this.is(':password') && !$this.val())) {
            valid = false;
        }
    });

    return valid;
}


/*
*
*/
$(document).ready(function () {
    $('#re-issue-form').formValidation({
        framework: "bootstrap4",
        locale: 'vi_VN',
        icon: null,
        button: {
            selector: '#re-issue-form-submit',
            disabled: 'disabled'
        },
        fields: {
            TemplateId: {
                validators: {
                    notEmpty: {
                        message: 'Loại hóa đơn là trường bắt buộc'
                    }
                }
            },
            StartNumber: {
                validators: {
                    notEmpty: {
                        message: 'Số bắt đầu hóa đơn là trường bắt buộc'
                    },
                    greaterThan: {
                        value: -1,
                        message: 'Số bắt đầu hóa đơn không được là số âm'
                    },
                }
            },
            EndNumber: {
                validators: {
                    notEmpty: {
                        message: 'Số kết thúc hóa đơn là trường bắt buộc'
                    }
                }
            },
            StartUsingDate: {
                validators: {
                    notEmpty: {
                        message: 'Ngày bắt đầu hóa đơn là trường bắt buộc'
                    }
                }
            }
        },
        err: {
            clazz: 'invalid-feedback'
        },
        control: {
            // The CSS class for valid control
            valid: 'is-valid',

            // The CSS class for invalid control
            invalid: 'is-invalid'
        },
        row: {
            invalid: 'has-danger'
        }
    }).on('err.field.fv', function (e, data) {
        data.fv.disableSubmitButtons(true);
    }).on('success.field.fv', function (e, data) {
        var fields = $("form#upgrage-account-form :input:not(:hidden)");
        if (!required(fields)) {
            $('#submit-upgrade-account').attr('disabled', 'disabled');
        }
    });
});
