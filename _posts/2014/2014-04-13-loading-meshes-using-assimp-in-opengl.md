---
layout: post
title: Loading meshes using Assimp in OpenGL
author: Håvard Kindem
---
I have been catching up on OpenGL 4.4 lately and realized that there are few working examples on the newer versions of OpenGL. The following code uses <a title="assimp.sourceforge.net" href="https://assimp.sourceforge.net/" target="_blank">Assimp</a>, <a title="glfw.org" href="https://www.glfw.org/" target="_blank">GLFW</a> and <a title="glew.sourceforge.net" href="https://glew.sourceforge.net/" target="_blank">GLEW</a> to load and render all supported Assimp formats and requires OpenGL 3.0 or above.

You will be required to set up an OpenGL context, I will presume that you have the knowledge to do that (if not, I can recommend <a title="Tutorial 1 : Opening a window" href="https://www.opengl-tutorial.org/beginners-tutorials/tutorial-1-opening-a-window/" target="_blank">this tutorial from opengl-tutorial.org</a>).

To use the Mesh class, this is all that is required:
{% highlight cpp %}
Mesh *mesh = new Mesh("filename.dae");
while (!glfwWindowShouldClose(window)) {
	mesh->render();
}
delete m;
{% endhighlight %}
The combined source and example shaders can be downloaded at the bottom of this page.
<!--more-->
<h2>Source code</h2>
<strong>Mesh.h</strong>
{% highlight cpp %}
#pragma once

#include "include/GL/glew.h"
#include "include/GL/glfw3.h"

#include "include\assimp\scene.h"
#include "include\assimp\mesh.h"

#include 

class Mesh
{
public :
	struct MeshEntry {
		static enum BUFFERS {
			VERTEX_BUFFER, TEXCOORD_BUFFER, NORMAL_BUFFER, INDEX_BUFFER
		};
		GLuint vao;
		GLuint vbo[4];

		unsigned int elementCount;

		MeshEntry(aiMesh *mesh);
		~MeshEntry();

		void load(aiMesh *mesh);
		void render();
	};

	std::vector<MeshEntry*>; meshEntries;

public:
	Mesh(const char *filename);
	~Mesh(void);

	void render();
};
{% endhighlight %}

<strong>Mesh.cpp</strong>
{% highlight cpp %}#include "Mesh.h"

#include "include\assimp\Importer.hpp"
#include "include\assimp\postprocess.h"


/**
*	Constructor, loading the specified aiMesh
**/
Mesh::MeshEntry::MeshEntry(aiMesh *mesh) {
	vbo[VERTEX_BUFFER] = NULL;
	vbo[TEXCOORD_BUFFER] = NULL;
	vbo[NORMAL_BUFFER] = NULL;
	vbo[INDEX_BUFFER] = NULL;

	glGenVertexArrays(1, &amp;vao);
	glBindVertexArray(vao);

	elementCount = mesh->mNumFaces * 3;

	if(mesh->HasPositions()) {
		float *vertices = new float[mesh->mNumVertices * 3];
		for(int i = 0; i < mesh->mNumVertices; ++i) {
			vertices[i * 3] = mesh->mVertices[i].x;
			vertices[i * 3 + 1] = mesh->mVertices[i].y;
			vertices[i * 3 + 2] = mesh->mVertices[i].z;
		}

		glGenBuffers(1, &amp;vbo[VERTEX_BUFFER]);
		glBindBuffer(GL_ARRAY_BUFFER, vbo[VERTEX_BUFFER]);
		glBufferData(GL_ARRAY_BUFFER, 3 * mesh->mNumVertices * sizeof(GLfloat), vertices, GL_STATIC_DRAW);

		glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, NULL);
		glEnableVertexAttribArray (0);

		delete[] vertices;
	}


	if(mesh->HasTextureCoords(0)) {
		float *texCoords = new float[mesh->mNumVertices * 2];
		for(int i = 0; i < mesh->mNumVertices; ++i) {
			texCoords[i * 2] = mesh->mTextureCoords[0][i].x;
			texCoords[i * 2 + 1] = mesh->mTextureCoords[0][i].y;
		}

		glGenBuffers(1, &amp;vbo[TEXCOORD_BUFFER]);
		glBindBuffer(GL_ARRAY_BUFFER, vbo[TEXCOORD_BUFFER]);
		glBufferData(GL_ARRAY_BUFFER, 2 * mesh->mNumVertices * sizeof(GLfloat), texCoords, GL_STATIC_DRAW);

		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 0, NULL);
		glEnableVertexAttribArray (1);

		delete[] texCoords;
	}


	if(mesh->HasNormals()) {
		float *normals = new float[mesh->mNumVertices * 3];
		for(int i = 0; i < mesh->mNumVertices; ++i) {
			normals[i * 3] = mesh->mNormals[i].x;
			normals[i * 3 + 1] = mesh->mNormals[i].y;
			normals[i * 3 + 2] = mesh->mNormals[i].z;
		}

		glGenBuffers(1, &amp;vbo[NORMAL_BUFFER]);
		glBindBuffer(GL_ARRAY_BUFFER, vbo[NORMAL_BUFFER]);
		glBufferData(GL_ARRAY_BUFFER, 3 * mesh->mNumVertices * sizeof(GLfloat), normals, GL_STATIC_DRAW);

		glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, 0, NULL);
		glEnableVertexAttribArray (2);

		delete[] normals;
	}
	

	if(mesh->HasFaces()) {
		unsigned int *indices = new unsigned int[mesh->mNumFaces * 3];
		for(int i = 0; i < mesh->mNumFaces; ++i) {
			indices[i * 3] = mesh->mFaces[i].mIndices[0];
			indices[i * 3 + 1] = mesh->mFaces[i].mIndices[1];
			indices[i * 3 + 2] = mesh->mFaces[i].mIndices[2];
		}

		glGenBuffers(1, &amp;vbo[INDEX_BUFFER]);
		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, vbo[INDEX_BUFFER]);
		glBufferData(GL_ELEMENT_ARRAY_BUFFER, 3 * mesh->mNumFaces * sizeof(GLuint), indices, GL_STATIC_DRAW);

		glVertexAttribPointer(3, 3, GL_FLOAT, GL_FALSE, 0, NULL);
		glEnableVertexAttribArray (3);

		delete[] indices;
	}
	

	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindVertexArray(0);
}

/**
*	Deletes the allocated OpenGL buffers
**/
Mesh::MeshEntry::~MeshEntry() {
	if(vbo[VERTEX_BUFFER]) {
		glDeleteBuffers(1, &amp;vbo[VERTEX_BUFFER]);
	}

	if(vbo[TEXCOORD_BUFFER]) {
		glDeleteBuffers(1, &amp;vbo[TEXCOORD_BUFFER]);
	}

	if(vbo[NORMAL_BUFFER]) {
		glDeleteBuffers(1, &amp;vbo[NORMAL_BUFFER]);
	}

	if(vbo[INDEX_BUFFER]) {
		glDeleteBuffers(1, &amp;vbo[INDEX_BUFFER]);
	}

	glDeleteVertexArrays(1, &amp;vao);
}

/**
*	Renders this MeshEntry
**/
void Mesh::MeshEntry::render() {
	glBindVertexArray(vao);
	glDrawElements(GL_TRIANGLES, elementCount, GL_UNSIGNED_INT, NULL);
	glBindVertexArray(0);
}

/**
*	Mesh constructor, loads the specified filename if supported by Assimp
**/
Mesh::Mesh(const char *filename)
{
	Assimp::Importer importer;
	const aiScene *scene = importer.ReadFile(filename, NULL);
	if(!scene) {
		printf("Unable to laod mesh: %s\n", importer.GetErrorString());
	}

	for(int i = 0; i < scene->mNumMeshes; ++i) {
		meshEntries.push_back(new Mesh::MeshEntry(scene->mMeshes[i]));
	}
}

/**
*	Clears all loaded MeshEntries
**/
Mesh::~Mesh(void)
{
	for(int i = 0; i < meshEntries.size(); ++i) {
		delete meshEntries.at(i);
	}
	meshEntries.clear();
}

/**
*	Renders all loaded MeshEntries
**/
void Mesh::render() {
	for(int i = 0; i < meshEntries.size(); ++i) { meshEntries.at(i)->render();
	}
}
{% endhighlight %}
