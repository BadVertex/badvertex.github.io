---
layout: post
title: Setting up a Git client with SSH keys
author: Håvard Kindem
---
<p>As a developer, version control systems are one of the daily routines. Surprisingly there are a lot of people who does not know how to use them even though there are a lot of good tutorials out there. Today we are adding a new one, trying to explain the setup process as simple as possible. For now, we will stick to using Git with Windows and a GUI.</p>
<p>Start off by downloading the following files:</p>

* <a title="msysgit" href="https://code.google.com/p/msysgit/downloads/list" target="_blank">msysgit</a>
* <a title="TortoiseGit" href="https://code.google.com/p/tortoisegit/downloads/list" target="_blank">TortoiseGit</a>
* <a title="plink" href="https://www.chiark.greenend.org.uk/~sgtatham/putty/download.html" target="_blank">Plink</a>
* <a title="puttygen.exe" href="https://www.chiark.greenend.org.uk/~sgtatham/putty/download.html" target="_blank">PuTTYgen</a>

<h2>The installation proccess</h2>
<p>To be sure, I'll go through every program to make sure that everything gets set up correctly.</p>

<h3>Installing Plink and puTTYgen</h3>
<p>These programs have no installer, so simply place them in a directory that is not in your way, as you will at least need the Plink for later. The newer installations of TortoiseGit does not require these as they are included in the installer, so this is just mentioned to be sure.</p>

<h3>Installing msysgit</h3>
<p>A very straight forward install, select<em> "Use Git Bash only"</em> when asked for adjusting your PATH environment and make sure that you set the line endings to <em>"Checkout Windows-style, commit Unix-style line endings" </em>if someone in your team uses Linux. I would never recommend using the other ones, unless you are specifically told to do so or if you are running your Git server on a Windows machine. If you want to add the Windows Explorer integration, go ahead, but it is really not needed as we are using TortoiseGit for this instead.</p>
<!--more-->

<h3>Installing TortoiseGit</h3>
<p>Only one thing to note here, you have to specify <em>"TortoisePlink, coming from Putty"</em> when choosing your SSH Client, OpenSSH will not work with SSH keys.</p>

<h2>Creating your SSH key pair</h2>
<p>Start up PuTTYgen, either from where you placed it or from the bin folder in your TortoiseGit installation directory if you got them through that installer. Press the <em>Generate</em> button and move your mouse over the grey area to create some randomness.</p>

{% include helpers/image.html name="puttygen.png" caption="After generating the RSA key in PuTTYgen" align="center" %}

<p>Make sure that you save your private key as you will be needing this to connect to the Git server. Note that this is your private key, not to be shared by anyone! Now copy your public key (the key that is visible in the PuTTYgen window) and give it to your Git admin or if you are using GitHub, GitLab, etc, add it to your SSH keys under your <em>Account Settings</em> » <em>SSH Keys.</em></p>

<h2>Connecting to the Git server</h2>
<p>So for the epic conclusion, connecting. Right click in any folder or on your desktop and select <em>Git Clone</em>.</p>

{% include helpers/image.html name="gitclone.png" caption="Tortoise Git Clone window" align="center" %}

<p>Add the URL of the Git repository and change the directory if you please. Finally, select <em>"Load Putty Key"</em> and browse to your private SSH key that you saved earlier. Press OK and you are good to go!</p>
