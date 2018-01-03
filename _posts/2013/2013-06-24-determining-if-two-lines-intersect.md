---
layout: post
title: Determining if two lines intersect
author: Håvard Kindem
---
<script src="https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.0/MathJax.js?config=TeX-AMS-MML_HTMLorMML" type="text/javascript"></script>

Today we are going to take a look at intersecting lines. There are some information on this scattered around the internet, but a good example is hard to find. The uses for this is mainly in 2D, it can be applied to lighting, physics and all other applications using rays.

![Intersecting and non-intersecting lines]({{ "/assets/lines.png" | absolute_url }}){: .center-image }

To solve this, we want to convert our lines to the following form:
$$ Ax + By = C $$

This allows us to create an equation that we can solve, with no need for ray tracing or other techniques. Converting our lines to this form is pretty trivial, as shown below. We do this for both lines, giving us the equations for both.

$$ 
A = y_{2} - y_{1}\\B = x_{1} - x_{2}\\C = A \cdot x_{1} + B \cdot y_{1}
$$

$$
A_{1}x + B_{1}y = C_{1}\\A_{2}x + B_{2}y = C_{2}
$$

Now when we have the expression for both lines, first of all we want to find the determinant to check if the lines are exactly parallel (they never intersect). The lines are parallel if the determinant is equal to zero.
<!--more-->
We find the determinant this by solving the following expression:

$$
\theta = A_{1} \cdot B_{2} - A_{2} \cdot B_{1}
$$

If the determinant is equal to zero, then the lines are perfectly parallel and will never intersect. If the lines are not parallel however, we know that they must intersect at some point. The point which the lines intersects are found using the formula below.

$$
x' = \frac{B_{2} \cdot C_{1} - B_{1} \cdot C_{2}}{\theta}\\y' = \frac{A_{1} \cdot C_{2} - a_{2} \cdot C_{1}}{\theta}
$$

If we want to check if the intersection happened on our lines(limited by their length), we can simply check if the intersection point is between the end points of both lines like this:

$$
\min{x_{1}, x_{2}} \leq x' \leq \max{x_{1}, x_{2}}\\\min{y_{1}, y_{2}} \leq y' \leq \max{y_{1}, y_{2}}
$$

The math might look a bit alien, but for all you programmers out there; here is all the code needed for checking if two lines intersect. Please note that the implementation of the Line class already generates the correct expression for the lines. The code for this class is appended below.

{% highlight csharp %}
public static bool intersects(Line a, Line b)
{
    float det = a.a * b.b - b.a * a.b;

    if (fEquals(0.0f, det))
    {
        return false;
    }
    else
    {
        float x = (b.b * a.c - a.b * b.c) / det;
        float y = (a.a * b.c - b.a * a.c) / det;

        if ( !inRange(x, Math.Min(a.x1, a.x2), Math.Max(a.x1, a.x2)) )
        {
            return false;
        }
        else if ( !inRange(y, Math.Min(a.y1, a.y2), Math.Max(a.y1, a.y2)) )
        {
            return false;
        }
        else if ( !inRange(x, Math.Min(b.x1, b.x2), Math.Max(b.x1, b.x2)) )
        {
            return false;
        }
        else if ( !inRange(y, Math.Min(b.y1, b.y2), Math.Max(b.y1, b.y2)) )
        {
            return false;
        }

        return true;
    }
}
{% endhighlight %}

Source: [Line.cs]({{ "/assets/Line.cs" | absolute_url }})
