function setupTooltips() {
	$('a.tooltip').mouseover(function(e) {
		var title = $(this).attr('data-title');
		var content = $(this).attr('data-content');
		
		if(title != null && title.length > 0) {
			$('div#tooltip_content').html("<b>" + title);
			
			if(content != null && content.length > 0) {
				$('div#tooltip_content').append("</b><br /><br />" + content);
			}
			
		} else if(content != null && content.length > 0) {
			$('div#tooltip_content').append(content);
		
		} else {
			return;
		}

		$('div#tooltip_wrapper').css('left', $(this).position().left + ($(this).width() / 4));
		$('div#tooltip_wrapper').css('top', $(this).position().top + $(this).height());
		$('div#tooltip_wrapper').show();
	});
	
	$('a.tooltip').mouseout(function(e) {
		$('div#tooltip_wrapper').hide();
		$('div#tooltip_content').html("");
	});
}