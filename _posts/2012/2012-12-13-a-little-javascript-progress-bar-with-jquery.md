---
layout: post
title: A little JavaScript progress bar with JQuery
author: Håvard Kindem
---
<p>As it has been a lot of heavyweight programming lately (as well as the code that I have shared here) I wanted to lighten the mood and do a little JavaScript with JQuery again. This is a very simple progress bar that can be used pretty much anywhere. It looks something like this:</p>
<div id="pbar_outerdiv" style="width: 300px; height: 20px; border: 1px solid grey; z-index: 1; position: relative; border-radius: 5px; -moz-border-radius: 5px;">
<div id="pbar_innerdiv" style="background-color: lightblue; z-index: 2; height: 100%; width: 0%;"></div>
<div id="pbar_innertext" style="z-index: 3; position: absolute; top: 0; left: 0; width: 100%; height: 100%; color: black; font-weight: bold; text-align: center;">0%</div>
</div>
<p><script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script><br />
<script type="text/javascript">
function updateProgress(percentage) {
	$('#pbar_innerdiv').css("width", percentage + "%");
	$('#pbar_innertext').text(percentage + "%");
}
var timer = 0;
var perc = 0;
function animateUpdate() {
	perc++;
	updateProgress(perc);
	if(perc < 100) {
		timer = setTimeout(animateUpdate, 50);
	}
}
$(document).ready(function() {
	$('#pbar_outerdiv').mouseenter(function() {
		clearTimeout(timer);
		perc = 0;
		animateUpdate();
	});
});
</script></p>
<p>Hover over the progress bar to animate it.</p>
<h2>The code:</h2>

{% highlight html %}
<div id="pbar_outerdiv" style="width: 300px; height: 20px; border: 1px solid grey; z-index: 1; position: relative; border-radius: 5px; -moz-border-radius: 5px;">
<div id="pbar_innerdiv" style="background-color: lightblue; z-index: 2; height: 100%; width: 0%;"></div>
<div id="pbar_innertext" style="z-index: 3; position: absolute; top: 0; left: 0; width: 100%; height: 100%; color: black; font-weight: bold; text-align: center;">0%</div>
</div>

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<script type="text/javascript">
var timer = 0;
var perc = 0;

function updateProgress(percentage) {
    $('#pbar_innerdiv').css("width", percentage + "%");
    $('#pbar_innertext').text(percentage + "%");
}

function animateUpdate() {
    perc++;
    updateProgress(perc);
    if(perc < 100) {
        timer = setTimeout(animateUpdate, 50);
    }
}

$(document).ready(function() {
    $('#pbar_outerdiv').mouseenter(function() {
        clearTimeout(timer);
        perc = 0;
        animateUpdate();
	});
});
</script>
{% endhighlight %}
