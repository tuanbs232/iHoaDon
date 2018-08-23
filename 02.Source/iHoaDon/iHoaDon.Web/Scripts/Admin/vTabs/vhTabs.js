jQuery.fn.vhTabs = function (options) {
	myoptions = jQuery.extend ({
		selected:1,
		ulTab:'ul.tabs',
		class_content:'.tabs_ct',
		change:null
	},options );
	return this.each(function(){
		obj = $(this);
		var tabCt = $(myoptions.class_content, obj);
		tabCt.hide();
		var ul = $(myoptions.ulTab, obj);
		tabs = $(myoptions.ulTab+' li',obj);
		tabs.each(function(i){
			if( myoptions.selected == (i+1)){
			  	$(this).addClass('selected');
				var content_show = $(this).find('a').attr('href');
				obj.find(content_show).show();
			}
		});
		tabs.click(function(){
			ul.find('li').removeClass("selected");
			$(this).addClass("selected");
			tabCt.hide();
			var content_show = $(this).find('a').attr('href');
			$(content_show).show();
			if(myoptions.change)
				myoptions.change.call(this);
			return false;
		});
		tabs.click(function( e ){
			return false;
		});
		
	});
	
}