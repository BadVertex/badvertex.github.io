---
layout: post
title: PHP Server redirect
author: Håvard Kindem
---
After a couple months of moving, putting together IKEA furniture and reconfiguring servers and devices there is still one thing missing; a static IP. I am currently running <a title="OpenProject Website" href="https://www.openproject.org/" target="_blank">OpenProject</a> as my choice of project management tool. The challenge of using this is that it runs on Ruby, so it has to be hosted on one of my servers and these are placed in my apartment at the moment.

Normally, when having a static IP you would simply add a CNAME record for the IP and it would have been working as intended. At the moment this is unfortunately not the case. So how do we get around this?

In my case, only HTTP needs to be supported at the moment so the solution was making a simple PHP script handling the redirection. This stores the server IP on request and redirects everything else. It also handles subdirectories and GET requests correctly.

To use the script, create a folder with the desired name of your server(<em>server1.your-domain.com</em>) would be achieved by creating a folder named <em>server1</em> in the root directory of your web host. Then all you have to do is extract the source in that directory and create a cron job on your server for updating the server IP. <strong>Remember to update your auth token in index.php</strong>.

<h2>Cron Job setup</h2>
Append the following content to your crontab:
{% highlight bash %}
*/10 * * * * wget -q -O /dev/null "https://<folder-name>.your-domain.com/?action=set&auth=<your-auth-code>"
{% endhighlight %}
Save and exit.

<h2>Example usage</h2>
Manually update your server IP to your current public IP<br />
`https://server1.your-domain.com/?action=set&auth=eVZKwTTFYASm6orAgzjm`

Retrieve the server IP in a browser<br />
`https://server1.your-domain.com/?action=get`

Get your server IP in a terminal<br />
`$ echo Server ip is: $(curl -sL https://server1.your-domain.com/?action=get)`

<h2>Get the code</h2>
You can download the source here: [server-redirect.zip]({{ "/assets/server-redirect.zip" | absolute_url }}) . 
