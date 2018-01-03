---
layout: post
title: Printing and limiting FPS using GLUT
author: Håvard Kindem
---
Here is a very basic example on how to limit and printing the FPS when you are using GLUT. Although GLUT is very outdated (FreeGLUT might still be a viable option though), this will point you in the right way when you are trying to achieve this.

### The timer function
The timer function is what selects when to update the frame. It measures the time taken to draw the frame and allows the program to sleep when not drawing. Notice that the int flag variable is unused.

{% highlight cpp %}
void timer(int flag) {
    drawStartTime = clock();
    glutPostRedisplay();
    drawEndTime = clock();
    
    delayToNextFrame =  (CLOCKS_PER_SEC/MAX_FPS) - (drawEndTime-drawStartTime);
    delayToNextFrame = floor(delayToNextFrame+0.5);
    delayToNextFrame < 0 ? delayToNextFrame = 0 : NULL;

    glutTimerFunc(delayToNextFrame, timer, 0);
}
{% endhighlight %}

<!--more-->

### The display function
Here, we are using a very simple display function, the functions called are simple drawing the frame and GUI, while we are sending the processor clock value to the fpsTimer class for processing.

{% highlight cpp %}
void displayFunc() {
    fpsTimer->timeFrame();

    drawFrame();
    drawGUI();

    glutSwapBuffers();
}
{% endhighlight %}

### The fpsTimer class
This class handles the measuring of FPS over an average number of frames and updates the text after that number of frames have passed, to reduce the flicker of the text. What these functions basically do, are to calculate the time since the last frame, average it and return the text string.

**FpsTimer.h**

{% highlight cpp %}
#include <ctime>
#include <deque>

class FpsTimer
{
private :
    std::deque<float> *lastFrameTimes;
    time_t lastFrame, tempTime;
    char *fpsString;
    int averageOfFrames;
    int framesToUpdate;
    float averageFps;
public :
    FpsTimer(int averageOfFrames);
    void timeFrame();
    char *getFps();
};
{% endhighlight %}

**FpsTimer.cpp**

{% highlight cpp %}
#include "FpsTimer.h"

FpsTimer::FpsTimer(int averageOfFrames) { 
    lastFrame = NULL; 
    this->averageOfFrames = averageOfFrames;
    lastFrameTimes = new std::deque<float>(averageOfFrames);
    framesToUpdate = averageOfFrames;
    fpsString = new char[15];
}

void FpsTimer::timeFrame() {
    tempTime = clock();

    if( lastFrame != NULL ) {
        if(lastFrameTimes->size() >= averageOfFrames) {
            lastFrameTimes->pop_back();
        }
        lastFrameTimes->push_front( tempTime - lastFrame );
    }
    lastFrame = tempTime;
}


char *FpsTimer::getFps(){
    framesToUpdate--;
    if(lastFrameTimes->size() < averageOfFrames) {
        return "Calculating";
    }

    if(framesToUpdate <= 0) {
        averageFps = 0;
        for(int i = 0; i < lastFrameTimes->size(); i++) {
            averageFps += lastFrameTimes->at(i);
        }
        averageFps /= lastFrameTimes->size();
        averageFps = CLOCKS_PER_SEC / averageFps;
        sprintf(fpsString, "%4.2f FPS", averageFps);
        framesToUpdate = averageOfFrames;
    }
    return fpsString;
}
{% endhighlight %}

### Printing the FPS on screen
The final step is to print the fps on screen using the following call and function:

{% highlight cpp %}
drawText(0.2f, 0.5, GLUT_BITMAP_HELVETICA_18, fpsTimer->getFps());

void drawText(float x, float y, void *font, char *string) {
    char *c;
    glRasterPos2f(x,y);

    for(c=string; *c != ''; c++) {
        glutBitmapCharacter(font, *c);
    }
}
{% endhighlight %}