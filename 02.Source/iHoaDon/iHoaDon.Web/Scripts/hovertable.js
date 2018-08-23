$(document).ready(function () {
    HoverTable.Initialize();
});

(function ($) {
    HoverTable = {
        Initialize: function () {
            style = [];
            style.push('<style type="text/css">');
            style.push('tr.hover-row {');
            style.push('background-color: #FFFFDD;');
            style.push('cursor:pointer;');
            style.push('color:#696969;}');

            style.push('tr.highlight-row {');
            style.push('background-color: #507AAA;');
            style.push('color:#FFFFFF;}');

            style.push('tr.highlight-row a {');
            style.push('color:#fff;}');

            $('head:first').append(style.join(''));
        },

        AddHover: function (id) {
            var tr = $('#' + id + ' tbody tr');
            if (tr.length > 0) {
                tr.hover(function(){
		            $(this).addClass("hover-row");
	            }, function(){
		            $(this).removeClass("hover-row");
	            });

                tr.click(function(){
			        tr.removeClass("highlight-row");
			        $(this).addClass("highlight-row");
	            });

                $('#' + id + ' tbody tr input, #' + id + ' tbody tr a, #' + id + ' tbody tr button').click( function(e) {
                    e.stopPropagation();
                    return true;
                });
            }
        }
    };
})(jQuery);