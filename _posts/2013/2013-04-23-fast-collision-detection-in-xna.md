---
layout: post
title: Fast collision detection in XNA
author: Håvard Kindem
---
I have been helping a friend of mine with a way of doing quick 2D collision detection in XNA. One of the common problems with collision detection in 2D is when you rotate it. Most functions do pixel perfect collisions by parsing the whole images, as it can be tricky to get the correct pixels to compare with each other when you rotate the image. Modifying the texture itself is way too slow and should be avoided at all costs and I feel the same about checking every pixel.

My solution parses the texture beforehand, generating either bounding lines or a line grid above the texture. When this is used with rectangle intersections as well, before doing the fine tests, it runs both fast and precise. The resolution of the lines can easily be specified for each texture and they work no matter what resolution you might use.

I'll get around to posting both the math and the code (when I get it cleaned up), but for now: here is a preview of the working concept.

<div class="videowrapper">
    <iframe class="lazy" data-src="https://www.youtube.com/embed/wUt_IFA-Q3g" frameborder="0" allowfullscreen></iframe>
</div>
