---
layout: post
title: 'Learning Programming: Getting started with Java'
author: Håvard Kindem
---
Want to learn how to program? This should be your starting point. With programming you can do anything on a computer, the only limitations are the computers performance, which will not be an issue for now. Be it calculations, mobile applications, games or any idea you might have, this makes it possible. This guide presumes that you have no prior knowledge to programming so for now we will introduce the tools and get started using them.
<h2>Prerequisites</h2>
To compile Java programs we are first of all going to need a compiler. Oracle provides this, the Java Development Kit (better known as the JDK). As this is only the compiler, it gets a bit tricky as we need to use the command line for it so we are going to ignore this and let Eclipse do it for us.

<!--more-->
Eclipse is an Integrated Development Kit (IDE), that means that we get a code editor with syntax highlighting, Intelli Sense (we will get into this later), debugger (for when things are not working) and an integrated compiler. What all of this means is that we don’t need to worry about anything other than the programming.

So: go to the <a href="http://www.eclipse.org/downloads/">Eclipse website</a> to download Eclipse Classic and install it. Note: If you are downloading the .zip file, you only need to extract it to a folder and run it from there.

{% include helpers/image.html name="eclipse_downloads.png" caption="Eclipse downloads list" centered=true %}

<h2>Your first project</h2>
Start by going to File, New, Java Project on the toolbar. Type in MyFirstProject as the project name. You might be asked to set your workspace directory, this is where your project will be saved to. For now, just press Finish and we are good to go.

{% include helpers/image.html name="eclipse_new.png" caption="New project" centered=true %}

You can find your newly created project in the Package Explorer on the left. Open it and right click the src folder, select New, Class. Fill in MyClass as the name and check “public static void main” as marked in the picture below and press Finish.

{% include helpers/image.html name="eclipse_newclass.png" caption="Create a class" centered=true %}

You will now see something like this:

{% include helpers/image.html name="eclipse_window.png" caption="How it should look" centered=true width="80%" %}

<h2>Getting started on the code</h2>
 Before we continue, I’ll introduce some new terms and what they are.
<br /><strong>Variable:</strong> This is data that can be changed, the opposite of constants. This is used to store numbers, strings and objects.
<br /><strong>Argument:</strong> This is basically an input variable for functions.
Function: This is best explained as a collection of actions. It can take arguments as input and it can return variables.
<br /><strong>Class:</strong> This is what is referred to as objects, it is a collection of functions and variables.
<br /><strong>Comments:</strong> This is text used to comment on your code to help you and others understand what it does.
<br /><strong>Data type:</strong> This describes the type of a variable or function. There are four main groups: Strings(text), Integers(whole numbers), Floats(decimal numbers) and Booleans(true or false).
<br /><strong>Visibility:</strong> The access level of classes, functions and variables. With access level it means who can access it. Private means that only members of the same class can access it, public means that all classes can access it and protected means that subclasses can access it, but don’t worry about the latter for now.So, let me explain the code that Eclipse generated for us.

{% highlight java %}
public class MyClass {
    /**
    * @param args
    */
    public static void main(String[] args) {
        // TODO Auto-generated method stub
    }
}
{% endhighlight %}

The first line of code defines the class MyClass as we created earlier. Its access level is set to public, this should be done for all classes. The contents of a class is defined within the curly brackets.The text within the /** */ which should be highlighted blue is used for Java documentation (JavaDoc), this is used to explain to you and others what a function does and describe its arguments, this is used in Eclipse to explain the function when you refer to the function from another place.“Public static void main(String[] args) {“ define a function. from the start: its access level, its type (static void) and its name. What is inside the brackets is the argument (functions can of course have more arguments separated by a comma). As with the class, the code of the function is kept inside the curly brackets.The double slash ( // ) defines a one lined comment. Comments are used to further explain your code. Multi line comments are defined with /* Comment text */ . Please note that this is different from the JavaDoc comments which starts with two stars, not one. Starting now, I want you to comment everything you do! I promise that it will save you loads of time when you are looking at your code, you will undoubtedly as all programmers do, ask yourself all the time: “What the heck did I do here?”.

To kick things off, let’s do a minor change to the code, the famous “Hello World”. Modify your code to look like this:

{% highlight java %}
public class MyClass {
    /**
    * @param args
    */
    public static void main(String[] args) {
        // TODO Auto-generated method stub
        System.out.println("Hello World!");
    }
}
{% endhighlight %}

As you type it, you will see a drop down list with a lot of options, this is the Intelli Sense. It lists all the classes, functions and variables that might match your text. Neat or what? You can access this at any time by pressing Control+Space. The code that we have written first calls to the system, then requests the writing part of the system and then the println function which writes the text on a single line to the console. Don't worry, this will become clearer as we go.Now it is time to compile this, press F11 or the green play button and check out your console.Congratulations, you have your first Java program!&nbsp;
