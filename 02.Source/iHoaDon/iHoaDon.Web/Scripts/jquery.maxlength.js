/*
 * jQuery Maxlength plugin 1.0.0
 *
 * Copyright (c) 2013 Viral Patel
 * http://viralpatel.net
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 */

;(function ($) {

	$.fn.maxlength = function(){
		 
		$("textarea[maxlength]").blur(function(event){ 
			var key = event.which;
			 
			var maxLength = parseInt($(this).attr("maxlength"));
			if (!isNaN(maxLength)){
				var text = this.value;
				
				var newLines = text.match(/(\r\n|\n|\r)/g);
		        var addition = 0;
		        if (newLines != null) {
		            addition = newLines.length;
		        }
				
		        var length = this.value.length + addition;
		        
		        var delta = length - maxLength;
				if(delta > 0) {
					this.value = text.substring(0, this.value.length - delta); 
				}
			}	
		});
		
		$("textarea[maxlength]").keypress(function(event){ 
			var key = event.which;
			 
			//all keys including return.
			if(key >= 33 || key == 13 || key == 32) {
				var maxLength = parseInt($(this).attr("maxlength"));
				if (!isNaN(maxLength)){
					var text = this.value;
					
					var newLines = text.match(/(\r\n|\n|\r)/g);
			        var addition = 0;
			        if (newLines != null) {
			            addition = newLines.length;
			        }
					
			        var length = this.value.length + addition;
			        
			        var delta = length - maxLength;
					if(delta >= 0) {
						event.preventDefault();
					}
				}	
			}
		});
	}

})(jQuery);