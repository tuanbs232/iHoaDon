
$(function () {
    if ($('input[name=CustopmerType]').get(4).checked) {
        $('#trVisible1').show();
        try{$('#trAdded').show();}catch(e){}
    } else {
        $('#trVisible1').hide();
        try{$('#trAdded').hide();}catch(e){}
    }

    $('input[name=hm]').live('change', function () {
        if ($(this).val() == 1) {
            $('#MarketingInfo_GoldLimit').attr('disabled', true);
            $('#haveLimitGold').attr("checked", false);
        } else {
            $('#MarketingInfo_GoldLimit').attr('disabled', false);
            $('#haveLimitGold').attr("checked", true);
        }
    });

    $('input[name=channel]').live('change', function () {
        var lng = $('input[name=channel]').length;
        var _id = parseInt(this.id.split('_')[1]);
        var lstTmp = $('#lstChannelMrk').val().split(',');
        var isCheck = this.checked;
        if (isCheck)
            lstTmp[_id] = "1";
        else lstTmp[_id] = "0";
        var rvl = "";
        for (var i = 0; i < lstTmp.length; i++) {
            if (i == (lstTmp.length - 1)) rvl = rvl + lstTmp[i];
            else rvl = rvl + lstTmp[i] + ',';
        }
        $('#lstChannelMrk').val(rvl);
    });

    $('input[name=CustopmerType]').live('change', function () {
        var lng = $('input[name=CustopmerType]').length;
        var _id = parseInt(this.id.split('_')[1]);
        var lstTmp = $('#lstCustomerTypeMrk').val().split(',');
        var isCheck = this.checked;
        if (isCheck)
            lstTmp[_id] = "1";
        else lstTmp[_id] = "0";
        var rvl = "";
        for (var i = 0; i < lstTmp.length; i++) {
            if (i == (lstTmp.length - 1)) rvl = rvl + lstTmp[i];
            else rvl = rvl + lstTmp[i] + ',';
        }
        $('#lstCustomerTypeMrk').val(rvl);
        if ($('input[name=CustopmerType]').get(4).checked) {
            $('#trVisible1').show();
            try { $('#trAdded').show(); } catch (e) { }
        } else {
            $('#trVisible1').hide();
            try { $('#trAdded').hide(); } catch (e) { }
        }
    });

    $('input[name=chkUserAdd]').live('change', function () {
        var lstUserAdded = "";
        lstUserAdded = $('#lstMrkCusSearchBeAdd').val();
        var lstUserRemove = "";
        lstUserRemove = $('#lstMrkCusSearchBeRemove').val();
        var _id = parseInt(this.id.split('_')[1]);
        if (this.checked) {
            lstUserRemove = "," + lstUserRemove + ",";
            var tmpT = "," + _id + ",";
            if (lstUserRemove.indexOf(tmpT) >= 0) {
                while (lstUserRemove.indexOf(tmpT) != -1) {
                    lstUserRemove = lstUserRemove.replace(tmpT, ',');
                }
                if (lstUserRemove[0] == ',') {
                    lstUserRemove = lstUserRemove.substring(1, lstUserRemove.length - 1);
                }
                if (lstUserRemove[lstUserRemove.length - 1] == ',') {
                    lstUserRemove = lstUserRemove.substring(0, lstUserRemove.length - 2);
                }
                $('#lstMrkCusSearchBeRemove').val(lstUserRemove);
            }
            else {
                if (lstUserAdded == '')
                    lstUserAdded += _id;
                else {
                    lstUserAdded = "," + lstUserAdded + ",";
                    var tmpT = "," + _id + ",";
                    if (lstUserAdded.indexOf(tmpT) == -1)
                        lstUserAdded += _id + ",";
                    if (lstUserAdded[0] == ',') {
                        lstUserAdded = lstUserAdded.substring(1, lstUserAdded.length - 1);
                    }
                    if (lstUserAdded[lstUserAdded.length - 1] == ',') {
                        lstUserAdded = lstUserAdded.substring(0, lstUserAdded.length - 2);
                    }
                };
                $('#lstMrkCusSearchBeAdd').val(lstUserAdded);
            }
            //Fuck
        }
        else {
            lstUserAdded = "," + lstUserAdded + ",";
            var tmpT = "," + _id + ",";
            if (lstUserAdded.indexOf(tmpT) == -1) {
                if (lstUserRemove == '')
                    lstUserRemove += _id;
                else {
                    lstUserRemove = "," + lstUserRemove + ",";
                    if (lstUserRemove.indexOf(tmpT) == -1)
                        lstUserRemove += _id + ",";
                    if (lstUserRemove[0] == ',') {
                        lstUserRemove = lstUserRemove.substring(1, lstUserRemove.length - 1);
                    }
                    if (lstUserRemove[lstUserRemove.length - 1] == ',') {
                        lstUserRemove = lstUserRemove.substring(0, lstUserRemove.length - 2);
                    }
                }
                $('#lstMrkCusSearchBeRemove').val(lstUserRemove);
            }
            else {
                while (lstUserAdded.indexOf(tmpT) != -1) {
                    lstUserAdded = lstUserAdded.replace(tmpT, ',');
                }
                if (lstUserAdded[0] == ',') {
                    lstUserAdded = lstUserAdded.substring(1, lstUserAdded.length - 1);
                }
                if (lstUserAdded[lstUserAdded.length - 1] == ',') {
                    lstUserAdded = lstUserAdded.substring(0, lstUserAdded.length - 2);
                }
                $('#lstMrkCusSearchBeAdd').val(lstUserAdded);
            }
        }
    });

    $('.inputH').live('keyup keydown', function () {
        if ($(this).val() == '0') $(this).val('');
        if (isNaN($(this).val().trim())) {
            $(this).val('0');
        } else {
            var val = $(this).val().trim();
            if (val.length > 1 && val.toString()[0] == "0") {
                $(this).val('0');
            }
            if (val > 23) $(this).val('23');
            if (val < 0) $(this).val('0');
        }
    });
    $('.inputH').live('blur', function () {
        if ($(this).val() == '') $(this).val('0');
    });
    $('.inputM').live('keyup keydown', function () {
        if ($(this).val() == '0') $(this).val('');
        if (isNaN($(this).val().trim())) {
            $(this).val('0');
        } else {
            var val = $(this).val().trim();
            if (val.length > 1 && val.toString()[0] == "0") {
                $(this).val('0');
            }
            if (val > 59) $(this).val('0');
            if (val < 0) $(this).val('0');
        }
    });
    $('.inputM').live('blur', function () {
        if ($(this).val() == '') $(this).val('0');
    });
    $('.inputPercent').live('keyup keydown', function () {
        if ($(this).val() == '0') $(this).val('');
        if (isNaN($(this).val().trim())) {
            $(this).val('0');
        } else {
            var val = $(this).val().trim();
            if (val.length > 1 && val.toString()[0] == "0") {
                $(this).val('0');
            }
            if (val > 100) $(this).val('100');
            if (val < 0) $(this).val('0');
        }
    });
    $('.inputPercent').live('blur', function () {
        if ($(this).val() == '') $(this).val('0');
    });

    $('.soduong').live('keyup keydown', function () {
        if ($(this).val() == '0') $(this).val('');
        if (isNaN($(this).val().trim())) {
            $(this).val('0');
        } else {
            var val = $(this).val().trim();
            if (val.length > 1 && val.toString()[0] == "0") {
                $(this).val('0');
            }
            if (val < 0) $(this).val('0');
        }
    });
    $('.soduong').live('blur', function () {
        if ($(this).val() == '') $(this).val('0');
    });
});

function SearchUser() {
    $('#trVisible2').show();
    $('#trAdded').show();
    var strInput = $('#strInputSearch').val();
    var _data = { type: 'SearchUserAddToMarketing', strInput: strInput, MarketingId: $('#MarketingInfo_ID').val() };
    $.ajax({
        beforeSend: function () {
        },
        type: 'POST',
        dataType: 'text',
        url: "/AjaxHandler/MarketingCustomer.ashx",
        data: _data,
        success: function (text) {            
            $('#tblDataSearch').html(text);            
        }
    });
} 