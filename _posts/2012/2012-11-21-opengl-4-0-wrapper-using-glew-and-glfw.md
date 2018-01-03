---
layout: post
title: OpenGL 4.0 wrapper using GLEW and GLFW
author: Håvard Kindem
---
So after my post yesterday about the GLSL shader loader, I thought that it might be redundant without the actual wrapper so here we go!
<strong>GLWrapper.hpp</strong>

{% highlight cpp %}
#ifndef GLWRAPPER_HPP
#define GLWRAPPER_HPP

#define GLFW_DLL 
#include "GL/glew.h"
#include "GL/glfw.h"

class GLWrapper {
private :
    int width;
    int height;
    char *title;
    double fps;
    void (*renderer)();
    bool running;

public :
    GLWrapper(int width, int height, char *title);
    ~GLWrapper();

    void setFPS(double fps) {
        this->fps = fps;
    }
    void setRenderer(void (*f)());
    void glMainLoop();
};

#endif
{% endhighlight %}

<!--more-->
<strong>GLWrapper.cpp</strong>

{% highlight cpp %}
#include "GLWrapper.hpp"
#include <iostream>

GLWrapper::GLWrapper(int width, int height, char *title) {

    this->width = width;
    this->height = height;
    this->title = title;
    this->fps = 60;
    this->running = true;

    if(!glfwInit()) {
        std::cout << "Failed to initialize GLFW." << std::endl;
        exit(EXIT_FAILURE);
    }

    glfwOpenWindowHint(GLFW_FSAA_SAMPLES, 4);
    glfwOpenWindowHint(GLFW_WINDOW_NO_RESIZE, GL_TRUE);
    glfwOpenWindowHint(GLFW_OPENGL_VERSION_MAJOR, 3);
    glfwOpenWindowHint(GLFW_OPENGL_VERSION_MINOR, 3);
    glfwOpenWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

    if(!glfwOpenWindow(width, height, 0, 0, 0, 0, 32, 0, GLFW_WINDOW)) {
        std::cout << "Could not open GLFW window." << std::endl;
        glfwTerminate();
        exit(EXIT_FAILURE);
    }

    glfwSetWindowTitle(title);

    if(glewInit() != GLEW_OK) {
        std::cout << "Unable to initialize GLEW." << std::endl;
        exit(EXIT_FAILURE);
    }

    glfwEnable(GLFW_STICKY_KEYS);
    glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
}


GLWrapper::~GLWrapper() {
    glfwTerminate();
}


void GLWrapper::setRenderer(void (*func)()) {
    this->renderer = func;
}


void GLWrapper::glMainLoop() {
    double updateInterval = 1.0/fps;
    double startTime;
    double endTime;
    double remainingTime = 0.0;

    do{
        startTime = glfwGetTime();

        this->renderer();

        endTime = glfwGetTime();
        remainingTime = updateInterval - (endTime - startTime);
        if(remainingTime > 0) {
            glfwSleep(remainingTime);
        }
    } while( running && glfwGetWindowParam( GLFW_OPENED ) );
}
{% endhighlight %}


This is fairly easy to use but to make sure, here is an example usage:
{% highlight cpp %}
#pragma comment(lib, "opengl32")
#pragma comment(lib, "glu32")
#pragma comment(lib, "lib/glew32")
#pragma comment(lib, "lib/glfwdll")

#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <string>

#define GLFW_DLL 

#include "GL/glew.h"
#include "GL/glfw.h"
#include "GLWrapper.hpp"
#include "GLShader.hpp"

GLuint vertexbuffer;
GLuint VertexArrayID;
static const GLfloat g_vertex_buffer_data[] = {
    -1.0f, 0.0f, 0.0f,
    1.0f,  0.0f, 0.0f,
    0.0f,  1.0f, 0.0f,
    0.0f,  -1.0f, 0.0f,
};

GLuint vertThingy;
GLuint thingyID;
GLfloat *thingy;


void renderer() {
    glClear( GL_COLOR_BUFFER_BIT );
    glClearColor(0.0f, 0.0f, 0.0f, 1.0f);

    glEnableVertexAttribArray(0);
    glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
    glVertexAttribPointer(
        0,                  // attribute 0. No particular reason for 0, but must match the layout in the shader.
        3,                  // size
        GL_FLOAT,           // type
        GL_FALSE,           // normalized?
        0,                  // stride
        (void*)0            // array buffer offset
    );


    // Draw the triangle !
    glDrawArrays(GL_LINES, 0, 4); // Starting from vertex 0; 3 vertices total -> 1 triangle
 
    glDisableVertexAttribArray(0);

    glEnableVertexAttribArray(0);
    glBindBuffer(GL_ARRAY_BUFFER, vertThingy);
    glVertexAttribPointer(
        0,                  // attribute 0. No particular reason for 0, but must match the layout in the shader.
        3,                  // size
        GL_FLOAT,           // type
        GL_FALSE,           // normalized?
        0,                  // stride
        (void*)0            // array buffer offset
    );
 
    // Draw the triangle !
    glDrawArrays(GL_TRIANGLES, 0, 3); // Starting from vertex 0; 3 vertices total -> 1 triangle
    glDisableVertexAttribArray(0);

    glfwSwapBuffers();
}

void init() {
    glGenVertexArrays(1, &VertexArrayID);
    glBindVertexArray(VertexArrayID);

    // Generate 1 buffer, put the resulting identifier in vertexbuffer
    glGenBuffers(1, &vertexbuffer);
 
    // The following commands will talk about our 'vertexbuffer' buffer
    glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
 
    // Give our vertices to OpenGL.
    glBufferData(GL_ARRAY_BUFFER, sizeof(g_vertex_buffer_data), g_vertex_buffer_data, GL_STATIC_DRAW);

    thingy = new GLfloat(9);
    thingy[0] = -1.0f;
    thingy[1] = 0.0f;
    thingy[2] = 0.0f;

    thingy[3] = 1.0f;
    thingy[4] = 0.0f;
    thingy[5] = 0.0f;

    thingy[6] = 0.0f;
    thingy[7] = 1.0f;
    thingy[8] = 0.0f;

    glGenVertexArrays(1, &thingyID);
    glBindVertexArray(thingyID);

    // Generate 1 buffer, put the resulting identifier in vertexbuffer
    glGenBuffers(1, &vertThingy);
 
    // The following commands will talk about our 'vertexbuffer' buffer
    glBindBuffer(GL_ARRAY_BUFFER, vertThingy);
 
    // Give our vertices to OpenGL.
    glBufferData(GL_ARRAY_BUFFER, sizeof(thingy), thingy, GL_STATIC_DRAW);
}


void GLFWCALL keyCallback(int key, int state) {
    std::cout << "KEY: " << (char)key << std::endl;
}


int main(int argc, char* argv[])
{
    GLWrapper *glw = new GLWrapper(1024, 768, "Heisann");
    glw->setRenderer(renderer);

    init();
    glfwSetKeyCallback(keyCallback);

    GLuint program = LoadShader("shader.vert", "shader.frag");
    glUseProgram(program);

    glw->glMainLoop();

    delete(glw);

    return 0;
}
{% endhighlight %}
