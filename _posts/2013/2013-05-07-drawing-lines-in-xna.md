---
layout: post
title: Drawing lines in XNA
author: Håvard Kindem
---
As there is no built in way of drawing lines in XNA(which is really weird), I thought that I should share this little snippet. 

{% highlight csharp %}
public void drawLine(SpriteBatch spriteBatch, Vector2 p1, Vector2 p2, Texture2D texture)
{
    float length = (float)Math.Sqrt((p2x - p1x) * (p2x - p1x) + (p2y - p1y) * (p2y - p1y));
    spriteBatch.Draw(texture, new Vector2(p1.X, p1.Y), null, Color.White,
        (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X),
        new Vector2(0f, 1.0f),
        new Vector2(length, 1f),
        SpriteEffects.None, 0f);
}
{% endhighlight %}

This function draws a line between <em>p1</em> and <em>p2</em>, using the texture defined(yes, it needs to be a texture). The <em>Color.White</em> in the function, just specifies the clear color. I post this now, as it can be used with the upcoming how-to: Line intersection.
