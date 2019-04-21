---
layout: post
title: Fast 2D Collision Detection in XNA
author: Håvard Kindem
---
As shown in the YouTube video in the post <a title="Fast collision detection in XNA" href="{{ site.url }}/2013/04/23/fast-collision-detection-in-xna/">Fast collision detection in XNA</a>, I made a quick way of doing near pixel perfect collision detection in XNA that does not get slowed down by rotating the object. This algorithm first checks the bounds of the object that scales based on the rotation of the content(as shown in <a title="Re-scaling a rectangle based on the rotation of it's content" href="{{ site.url }}/2013/04/23/re-scaling-a-rectangle-based-on-the-rotation-of-its-content/">Re-scaling a rectangle based on the rotation of it's content</a>), before checking for line intersection (see <a title="Determining if two lines intersect" href="{{ site.url }}/2013/06/24/determining-if-two-lines-intersect/">Determining if two lines intersect</a>).

The whole idea is to generate lines around the object as this is much faster then checking pixel by pixel and can easily be rotated as they only consist of two points. This is done only once when first loading the image.

{% include helpers/image.html name="fast-line-intersection-1.png" caption="Tracing the outlining of the object using 5 segments" align="center" %}

Once the outlining has been traced, we generate lines between them to give us the approximate outlining. In this case 5 segments has been used to better illustrate what is being done. The mesh we end up with should look something like the following.
<!--more-->

{% include helpers/image.html name="fast-line-intersection-2.png" caption="Generating lines between each outlining point" align="center" %}

When we have these lines, it is then checked against each other if the bounding boxes should overlap. Based on the object, the number of points traced can be adjusted to suit your needs. The code includes functions for generating both the outlining and the inner grid (only one is needed). When the object is rotated, the lines are rotated around the object origin to stay concurrent. This recalculation is only needed if the bounding boxes overlap of course. 

This solution works like a charm and is much faster than traditional pixel-perfect collision and can achieve nearly the same results by specifying more segments. It can be optimized however: Instead of tracing vertically and horizontally, the lines can be traced from the same outer points but towards the origin instead.

{% include helpers/image.html name="fast-line-intersection-3.png" caption="Tracing against the origin" align="center" %}

As always, feel free to use, modify and distribute this code in any project. 

Source: [XNA-Collition-Detection.zip](/assets/files/XNA-Collition-Detection.zip)
