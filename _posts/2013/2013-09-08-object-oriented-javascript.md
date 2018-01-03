---
layout: post
title: Object-oriented JavaScript
author: Håvard Kindem
---
Any project using JavaScript tend to quickly grow out of proportions and often results in very messy code. There are some good tools out there for making it easier to write and maintain JS code, one of which I like the best: <a title="CoffeScript.org" href="https://coffeescript.org/" target="_blank">CoffeScript</a>. Using these kind of tools helps a lot, but today I'll present one way of making JavaScript more readable.

By applying object-oriented programming to JavaScript, it's readability, modularity and reusability are greatly improved. Object-oriented languages has been around since Simula appeared in 1967, and is used by some of the most common languages like C++, C# and Java.

As JavaScript does not have native support for classes, but is able to use anything as as variable there are a couple different methods of making classes in JS. I'll present the one that I think is most intuitive. Let's get to it.

<h2>Namespace definition</h2>
Using namespaces in JavaScript allows us to further separate the classes and avoids variable duplication. To do this, we need to define the namespaces as objects. To define the namespace "net.netxcius", the following code is required:
{% highlight javascript %}
var net = {
    nexcius: {}
};
{% endhighlight %}

<h2>Class example</h2>
The following code defines our variable as a function. This is what will contain everything that we would normally place inside a class. The outermost function is called a self invoking function, this means that is calls itself upon creation. This is not required, but it has one great advantage: it can contain area specific global variables (Sarfraz Ahmed has a good blog post on this: <a href="https://sarfraznawaz.wordpress.com/2012/01/26/javascript-self-invoking-functions/" title="Javascript Self Invoking Functions" target="_blank">Javascript Self Invoking Functions</a>).
<!--more-->
What is also worth mentioning is that the <em>return</em> object is what contains the public functions. These functions can of course access the local variables and functions. Here is the complete code for our class:
{% highlight javascript %}
(function() {
    net.nexcius.MyClass = function(var1, var2) {
        /** Private variables **/
        var _localString = var1;
        var _localNumber = var2;
    
        /** Named constructor **/
        (function TestClass() {
            console.log("Inside constructor");
        }) ();
    
        /** Private function **/
        var _addNumbers = function(num1, num2) {
            return num1 + num2;
        };
    
        /** Contains the public functions **/
        return {
            getLocalNumber : function() {
                return _localNumber;
            },
            
            getLocalString : function() {
                return _localString;
            },
            
            addNumbers : function(n1, n2) {
                return _addNumbers(n1, n2);
            }
        };
    };
}) ();
{% endhighlight %}

<h2>Usage</h2>
The final step is to create an instance of the class and check if it works. Following is the code used to call it and the results from the console:
{% highlight javascript %}
var myClass =  new net.nexcius.MyClass("This is the string", 13); // Inside constructor
console.log(myClass.getLocalNumber()); // 13
console.log(myClass.getLocalString()); // This is the string
console.log(myClass.addNumbers(3, 4)); // 7
{% endhighlight %}

JavaScript projects gets cluttered very quick, hopefully this can aid you in keeping on top of your code. As always: If you have any questions or thoughts, please leave a comment and I'll answer as soon as I can.