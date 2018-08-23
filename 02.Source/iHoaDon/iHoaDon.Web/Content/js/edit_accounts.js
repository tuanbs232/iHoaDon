
$(document).ready(function () {
    var stateId = $('#ProvinceHideId').val();
    var disId = $('#District').val();
    LoadData(stateId, disId);

    document.getElementById('ProvinceId').value = stateId;

    //
    editFormValdate();
});

/**
 * 
 */
function FillData() {
    var stateId = $('#ProvinceId').val();
    var e = document.getElementById("ProvinceId");
    document.getElementById("Province").value = e.options[e.selectedIndex].text;

    var disId = "";
    LoadData(stateId, disId);
}

/**
 * 
 */
function FillText() {
    var e = document.getElementById("TaxAgencyCode");
    document.getElementById("TaxAgencyName").value = e.options[e.selectedIndex].text;
}

/**
 * 
 * @param {any} data
 * @param {any} id
 */
function LoadData(data, id) {

    $.ajax({
        url: '/Accounts/ListProvince',
        type: "GET",
        dataType: "JSON",
        data: { newsId: data },
        success: function (lstItem) {
            $("#TaxAgencyCode").html(""); // clear before appending new list 
            $.each(lstItem, function (i, ls) {
                console.log(ls.Value + ": " + ls.Text);
                if (id != '' && id == ls.Value) {
                    $("#TaxAgencyCode").append($('<option selected="selected"></option>').val(ls.Value).html(ls.Text));
                } else {
                    $("#TaxAgencyCode").append($('<option></option>').val(ls.Value).html(ls.Text));
                }

            });
        }
    });
}

/**
 * 
 */
function editFormValdate() {
    $('#edit-form').formValidation({
        framework: "bootstrap4",
        locale: 'vi_VN',
        icon: null,
        button: {
            selector: '#edit-form-submit',
            disabled: 'disabled'
        },
        fields: {
            CompanyCode: {
                validators: {
                    notEmpty: {
                        message: 'Mã số thuế là trường bắt buộc'
                    }
                }
            },
            CompanyName: {
                validators: {
                    notEmpty: {
                        message: 'Tên công ty là trường bắt buộc'
                    }
                }
            },
            Address: {
                validators: {
                    notEmpty: {
                        message: 'Địa chỉ là trường bắt buộc'
                    }
                }
            },
            ProvinceId: {
                validators: {
                    notEmpty: {
                        message: 'Tỉnh thành là trường bắt buộc'
                    }
                }
            },
            TaxAgencyCode: {
                validators: {
                    notEmpty: {
                        message: 'Cơ quan thuế quản lý là trường bắt buộc'
                    }
                }
            },
            BankAccount: {
                validators: {
                    notEmpty: {
                        message: 'Số tài khoản ngân hàng là trường bắt buộc'
                    }
                }
            },
            Representative: {
                validators: {
                    notEmpty: {
                        message: 'Người đại diện theo pháp luật là trường bắt buộc'
                    }
                }
            },
            Phone: {
                validators: {
                    notEmpty: {
                        message: 'Số điện thoại là trường bắt buộc'
                    }
                }
            },
            Email: {
                validators: {
                    notEmpty: {
                        message: 'Địa chỉ Email là trường bắt buộc'
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
        var fields = $("form#edit-form :input:not(:hidden)");
        if (!required(fields)) {
            $('#edit-form-submit').attr('disabled', 'disabled');
        }
    });
}

/**
 * 
 * @param {any} fields
 */
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