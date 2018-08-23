function checkAll( check ){
//	alert( $(check).attr('checked') );
	if( $(check).attr('checked') == 'checked' ){
		$('tbody#TableContent :checkbox').attr('checked',true);
		$('#TableContent tr').addClass('selected');
		$(check).parents('.adminlist').addClass('selected').removeClass('NoCheckAll');
	}else{
		$('tbody#TableContent :checkbox').attr('checked',false);
		$('#TableContent tr').removeClass('selected');
		$(check).parents('.adminlist').removeClass('selected');
	}
}

function AlertShow(  msg , error ){
	var MsgClass = '';
	if( error == 'error' ){
		MsgClass = 'error';
	}
	var html = '<div class="fl msg '+MsgClass+'"><span class="ico"></span>'+msg+'<div class="clr"></div></div>';
	$('#AlertMsg').html( html );
}

$(function(){
	
    $('#TableContent :checkbox').live('click',function(){
		if( $(this).attr('checked') == 'checked' ){
			var check_list = $('#TableContent :checkbox');
			var i=0;
			check_list.each(function(){
				if( $(this).attr('checked') == 'checked'  ){
					i++;
					$(this).parents('tr').addClass('selected');
				}
			});
			if( i == check_list.length ){
				$('#check_all').attr('checked',true);
				$('.adminlist').addClass('selected').removeClass('NoCheckAll');
			}else{
				$('.adminlist').addClass('NoCheckAll');
			}
		}else{
			$('#check_all').attr('checked',false);
			$(this).parents('tr').removeClass('selected');
			$(this).parents('.adminlist').removeClass('selected').addClass('NoCheckAll');
			
			if( $('#TableContent :checked').length == 0 )
				$('.adminlist').removeClass('NoCheckAll');
		}
	});
	
	//===========
	
	$('#TableContent tr').live('mouseover mouseleave',function( e ){
		if( e.type == 'mouseover' )
			$(this).addClass('hover');
		else
			$(this).removeClass('hover');
	});
    //===========
	$('.navbar li').mouseover(function(){
		$(this).find('ul:first').stop().show();
		if( !$(this).hasClass('no') ){
			$(this).addClass('h');
			$(this).next().addClass('n');
			$(this).prev().addClass('n');
		}
		var sub_cat = $('ul:first',this);
		if( sub_cat.hasClass('sub') ){
			sub_cat.css( 'left',$(this).outerWidth() );
		}
		
	}).mouseleave(function(){
		$(this).find('ul:first').stop().hide();
		$(this).removeClass('h');
		$(this).next().removeClass('n');
		$(this).prev().removeClass('n');
	});
    //===================================
   /* $('.footer .scrollTop').click(function(){
		$('html,body').animate({scrollTop:0},500);
	});*/
    //===================================	
	/*$('.tborder td').mouseover(function(){	
		
		$(this).css({
			background:'#80b500',
			color:'#fff'
		});
	}).mouseleave(function(){
		var bgColor = '';
		if( $(this).parent().hasClass('tr1') ){
			bgColor = '#F1EFE7';
		}
		else if( $(this).parent().hasClass('tr2') ){
			bgColor = '#FFFCF1';
		}else if( $(this).parent().hasClass('th') ){
			bgColor = '#6a9e02';
		}
		else {
			bgColor = '#fff';
		}
		$(this).css({
			background:bgColor,
			color:''
		});
	});*/
	
	//===========
	
});

