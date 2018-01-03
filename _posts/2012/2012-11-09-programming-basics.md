---
layout: post
title: Programming Basics
author: Håvard Kindem
---
Just re posting a little rant about programming basics that I thought that you might find interesting. This will give you a basic understanding on what is basic for all programming languages. We will be looking into the purpose of a programming language, what happens under the hood and basic data types.


### The purpose of programming languages
Before there were programming languages, all code had to be written on an instruction level. This means that everything was written in pure machine code, which is best described as ones and zeros. What a programming language does is to make this humanly readable so we don’t have to be human calculators.

There are a few different types of programming languages. The most common ones are compiled, interpreted languages and embedded languages. The difference between these are that compiled languages are translated to machine code before it is run. This generally means that it will be faster than many others. Examples of compiled languages are the C languages(C++, C, C#, Objective C) Assembly, and Java(yes this is compiled to Java Virtual Machine code).

Interpreted languages are generally referred to as a language that is not directly compiled, but rather executed by an interpreting program when it is run. This is slower, but has the upside that is can be changed at any time, making it ideal for parts of programs that are modified often. Examples are Lua, VBScript, Python and Lisp.

Embedded languages are code that is embedded into text. The most common usage of this is on websites. It is slow, but usually very simple and easy to understand. Examples are: PHP, JavaScript and ActionScript.
<!--more-->

### So what does programming languages do in practice?
Let us say that we have the following code in C++:

```cpp
int a;
```
What this piece of code does is to allocate space for the integer a. This means that the size of the variable, in this case 4 bytes are allocated in memory and we can now access it. To take this another level down, the best way is to use Assembly to explain. Assembly is the last language before machine code, which means that the compiler is basically just replacing the text with machine instructions. The code fragment above, would look like this in assembly:

```asm
push $0
```
This does the same, it pushes 4 bytes to the stack. The stack, also known as call stack is a data structure that hold the information about the (sub)routines in a program. For now, just think of the stack as memory that holds both data and instructions.

Basic data types and sizes
There are multiple different data types and they tend to vary a bit from language to language (some languages doesn’t even use them in the code, but interprets them on their own). I will stay away from their language specific notations for now. Their differences are mainly their size and how they are interpreted. Given the following Java code:

```java
char x = 97;
System.out.println(a);
```
This code initializes a character type, but sets it s value to an integer(97). When printing this, it will still be interpreted as a character and the output would be [i]a[/i], which is the integer equivalent.

As mentioned, the different data types have different sizes. Here is a list of datatypes by size:

* 1 byte: character, byte, boolean(true or false)
* 2 bytes: short integer,
* 4 bytes: integer, floating point
* 8 bytes: double precision floating point

Note that these are just the basic ones, but it will give you a general idea about the sizes.

What is a bit and a byte?
A bit is the smallest form of data that you can have in a computer (disregarding the theory that no data is data as well =p ). A bit can be either 1 or 0. This means that a byte could look like this: 10111010. To get a better understanding of this, try to think of them as numbers. While we normally count 1,2,3.. The computer is counting like 1, 10, 11..
So instead of having the numbers 1 to 9 before going to 10 (adding another digit), we are now doing this when we hit 1. This means that you can know the value of a 1 by looking at where it is placed. The value of a set bit would be:
128, 64, 32, 16, 8, 4, 2, 1
So of we have the byte 101, this would mean that the number is 4 + 0 + 1 = 5.

I guess it is time to wrap this up. Hope that this has given you a better understanding of what is really going on in a computer. The next step is to learn a language and I will be following up with those guides as soon as possible, most likely starting out with Java.