---
layout: post
title: JQuery Tooltip
author: Håvard Kindem
---
Ever in need of more than just the standard <abbr title="Yes, this is the standard HTML tooltip">abbr</abbr> tooltip? A little while back I wrote just that. It can easily be made prettier, but it works as intended. What this does is listening for the <em>mouseover</em> and <em>mouseout</em> events and display a div with absolute positioning at the element's location with the tooltip text.

To add tooltips, the syntax would be like this: 
{% highlight html %}
<a href="#" class="tooltip" data-title="Tooltip title" data-content="Tooltip text">The text you want tooltipped</a>
{% endhighlight %}

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
This is what is looks like in action:

<style type="text/css">
div#tooltip_wrapper {position: absolute; display:none; max-width:300px;}
div#tooltip_content {background-color: white; padding: 10px; border: 1px solid black; box-shadow: 5px 5px 2px #999;}
a.tooltip {text-decoration: none; border-bottom: 1px dotted black; color: black;}
</style>

<div id="tooltip_wrapper"><div id="tooltip_content"></div></div>

<a href="#" class="tooltip" data-title="Tooltip title" data-content="The content comes here, the box grows up to 300 pixels, then wraps the word. It will Only grow to this size of needed.">Tooltip title and text</a>
The JavaScript code that does the work looks like this. As you can see, the listeners are registered and when the mouse hovers the element, it gets the <em>data-title</em> and <em>data-content</em> attributes from the &lt;a&gt; element.

{% highlight js %}
$(document).ready(function() {
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

        $('div#tooltip_wrapper').css('left', $(this).position().left + 
            ($(this).width() / 4));
        $('div#tooltip_wrapper').css('top', $(this).position().top + 
            $(this).height());
        $('div#tooltip_wrapper').show();
    });
	
    $('a.tooltip').mouseout(function(e) {
        $('div#tooltip_wrapper').hide();
        $('div#tooltip_content').html("");
    });
});
{% endhighlight %}

<script type="text/javascript" src="{{ site.url }}/assets/tooltip_js.js"></script>
<script type="text/javascript">
setupTooltips();
</script>
The full source code can be found here: <a href="{{ site.url }}/assets/tooltip_js.js">tooltip_js.js</a>.
As always: If you have any questions, please let me know!
