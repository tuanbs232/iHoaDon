$(window).bind('load', function () {
    loginForm();
});
function showMsg() {
    $('#msg').slideDown();
}
function loginForm() {
    var window_height = $(window).height();
    var hdr = $('.hdr');
    var footer = $('.footer');
    var loginFrm = $('.loginFrm');
    var frmHeight = hdr.outerHeight() + footer.outerHeight();
    var loginFrmHeight = window_height - frmHeight;
    loginFrm.css({
        height: loginFrmHeight
    });
    $('.loginBox').animate({
        top: (loginFrmHeight - $('.loginBox').outerHeight()) / 3
    });
}

function PostOnBegin() {
    $('#msg').slideUp();
    $('#msg').html('');
}
function PostOnComplete() {
    if ($('#msg').html().trim() != '') {
        showMsg();
    }
}
$(function () {
    $("#UserName").focus();
    $("form input").keypress(function (e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            $('#login_btn').click();
            return false;
        } else {
            return true;
        }
    });
});