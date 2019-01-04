---
layout: post
title: Re-scaling a rectangle based on the rotation of its content
author: Håvard Kindem
---
As you might have noticed in the video in my earlier post, the bounding box of the sprite is scaling based on the rotation. This is useful as it ensures that even if the content is rotated, it is always within the bounds of the rectangle. To illustrate this, take a look at the following photos:

<table style="border:0;margin:0 auto;"><tr>
<td><img class="lazy" data-src="/assets/posts/{{ page.date | date: "%Y-%m-%d" }}-{{ page.title | slugify }}/Square.png" title="A 6 by 6 square without rotation" /></td>
<td><img class="lazy" data-src="/assets/posts/{{ page.date | date: "%Y-%m-%d" }}-{{ page.title | slugify }}/SquareRotated.png" title="The same 6 by 6 square, but now rotated by 45 degrees" /></td>
</tr></table><br />

When we rotate the blue square, its bounds increases to the square in green. This is what we are trying to compensate for. The following formula gives us the delta x and y from the origin point.

$$
    dx'= |cos \theta| * w/2 + |sin \theta| * h/2
    \\
    dx'= |sin \theta| * w/2 + |cos \theta| * h/2
$$

Using this formula, we find that <em>dx' = 4.24</em> and<em> dy' = 4.24</em>. Please note that this formula will work for any sizes, I'm just using a square for simplicity. By applying this to code, we get the following in C#:

{% highlight csharp %}
public Rectangle getBoundsWithRotation(Rectangle rect, float angle)
{
    Vector2 origin = new Vector2((float)rect.X + rect.Width / 2.0f, 
        (float)rect.Y + rect.Height / 2.0f);

    float dx = (float)Math.Abs(Math.Cos(angle)) * (rect.Width / 2.0f) + 
        (float)Math.Abs(Math.Sin(angle)) * (rect.Height / 2.0f);
    float dy = (float)Math.Abs(Math.Sin(angle)) * (rect.Width / 2.0f) + 
        (float)Math.Abs(Math.Cos(angle)) * (rect.Height / 2.0f);

    int x = (int)Math.Round(origin.X - dx);
    int y = (int)Math.Round(origin.Y - dy);
    int w = (int)Math.Round(dx * 2.0f);
    int h = (int)Math.Round(dy * 2.0f);

    return new Rectangle(x, y, w, h);
}
{% endhighlight %}
