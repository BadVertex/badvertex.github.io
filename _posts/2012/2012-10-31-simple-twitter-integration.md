---
layout: post
title:  Simple Twitter integration
author: Håvard Kindem
---
**Note:** This code is deprecated as of the Twitter API version 1.1. It should still work if you add in the authentication.

As I completed the next release for the HRU app today, I was in the mood for some more web programming. Figured out that I should make a Twitter integration for my website, as I don’t like the Twitter badges or the WordPress plugins for it. This little snippet calls the Twitter API and displays the 10 latest tweets of the user. This code snippet can be used anywhere that supports JavaScript and HTML.
<!--more-->

To change the account, tweet count or what Twitter messages you want, just edit the address in the getJSON() call.

```html
<style>
div#tweetHeader {font-weight:bold;}
div#tweetContainer {width:200px; padding:0px;}
a.twitterLink {color:black;}
</style>


<div id="tweetContainer"></div>


<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
<script type="text/javascript">

var tweets = new Array();
var currentTweet = 0;
var tweetTimer;

$(document).ready(function() {
    getTweets();
    
    $("#tweetContainer").mouseover(function() {
        clearTimeout(tweetTimer);
    });
    
    $("#tweetContainer").mouseout(function() {
        tweetTimer = setTimeout(function(){
            updateContainer()
        },2000);
    });
});

function getTweets() {
    $.getJSON('https://api.twitter.com/1/statuses/user_timeline/Nexcius.json?count=10&trim_user=true&callback=?', 
        function(data) {
            for(var i in data) {
                var formattedString = data[i].text.replace(/((http|https):\/\/[^ ]+)/g, '<a class="twitterLink" href="$1">$1</a>');
                formattedString = formattedString.replace(/([#@][^ ]+)/g, '<a class="twitterLink" href="https://twitter.com/$1">$1</a>');
                
                tweets[i] = formattedString;
            }
        }).complete(function(){
            updateContainer();
        });
    
}

function updateContainer() {
    $("#tweetContainer").hide("slide", {direction: "left"}, 1000, function() {
        $("#tweetContainer").html(tweets[currentTweet]);
        $("#tweetContainer").show("slide", {direction: "right"}, 1000);
        
        currentTweet = ((currentTweet+1) % tweets.length);
    }); 

    tweetTimer = setTimeout(function(){
        updateContainer()
    },8000);
}

</script>
```