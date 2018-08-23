var element = 'loading';
var ie45,ns6,ns4,dom;
if (navigator.appName=="Microsoft Internet Explorer") ie45=parseInt(navigator.appVersion)>=4;
else if (navigator.appName=="Netscape"){  ns6=parseInt(navigator.appVersion)>=5;  ns4=parseInt(navigator.appVersion)<5;}
dom=ie45 || ns6;

var timershow = false;
var curx=-200;
var cury=350;
// var win_w=window.innerWidth ? window.innerWidth : document.body.offsetWidth;

var win_w = 0;
  if( typeof( window.innerWidth ) == 'number' ) {
    //Non-IE
    win_w = window.innerWidth;
  } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
    //IE 6+ in 'standards compliant mode'
    win_w = document.documentElement.clientWidth;
  } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
    //IE 4 compatible
    win_w = document.body.offsetWidth;
  }
  
var mid_w=win_w/2;
var timershow1=window.setInterval("stayMiddle()",1);

function getobj(id) {
el = document.all ? document.all[id] :   dom ? document.getElementById(id) :   document.layers[id];
return el;
}

function show_Loading() {
    doituong = getobj(element);
    if (timershow) window.clearTimeout(timershow);
    timershow=window.setInterval("nshow()",1);
}

function hide_Loading() {
    doituong = getobj(element);
    if (timershow) window.clearTimeout(timershow);
    timershow=window.setInterval("nhide()",1);
}

function moveobj(doituong,x,y) {
    doituong.style.left=x+"px"
    doituong.style.top=y+"px"
    curx=x;
    cury=y;
}

function stayMiddle() {
    if (document.documentElement && document.documentElement.scrollTop)
        var pY =  document.documentElement.scrollTop;
    else if (document.body)
        var pY =  document.body.scrollTop;

    doituong = getobj(element);
    newy = cury+((pY-cury)/16)+12;
    moveobj(doituong,curx,newy);
}

function nshow() {
    doituong = getobj(element);
    newx = curx+((mid_w-curx)/16)-7;
    moveobj(doituong,newx,cury);
}
function nhide() {
    doituong = getobj(element);
    newx = curx+((0-curx)/16)-15;
    moveobj(doituong,newx,cury);
}
// End
show_Loading();