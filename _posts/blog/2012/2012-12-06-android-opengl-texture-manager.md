---
layout: post
title: Android OpenGL Texture Manager
---
At the moment, I am working on a little project for Android. It is going to be a 2D tank war game using the accelerometer in the device for controlling the tank. When doing this I made a little texture manager class so that I can refer to the resource ID instead of the index of the GL Texture, making it a lot easier to use textures in OpenGL. 
Keep in mind that that code does not check for resources that are not loaded, you might want to add that or ensure that the required resources are loaded.

Using this is very simple. Here is an example:

```java
// When initializing
TextureHandler h = new TextureHandler(ctx, gl);
h.addTexture(R.drawable.image1);
h.addTexture(R.drawable.image2);
h.addTexture(R.drawable.image3);
h.loadTextures();


// In the draw loop
h.setTexture(R.drawable.image2);
```

<strong>The Code:</strong>

```java
import java.util.ArrayList;
import javax.microedition.khronos.opengles.GL10;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.opengl.GLUtils;

public class TextureHandler {
    private Context context = null;
    private GL10 gl = null;
    private ArrayList<Integer> textureResources = null;
	
    private int[] textures = null;
	
    public TextureHandler(Context context, GL10 gl) {
        this.context = context;
        this.gl = gl;
        textureResources = new ArrayList<Integer>();
    }
	
    public void addTexture(int resourceID) {
        textureResources.add(resourceID);
    }
	
    public void loadTextures() {
        textures = new int[textureResources.size()];
		
        gl.glGenTextures(textureResources.size(), textures, 0);
		
        for(int i = 0; i < textures.length; i++) {
            Bitmap bmp = BitmapFactory.decodeResource(context.getResources(), textureResources.get(i));	
			
            gl.glBindTexture(GL10.GL_TEXTURE_2D, textures[i]);
			
            gl.glTexParameterf(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_MIN_FILTER, GL10.GL_NEAREST);
            gl.glTexParameterf(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_MAG_FILTER, GL10.GL_LINEAR);
			
            GLUtils.texImage2D(GL10.GL_TEXTURE_2D, 0, bmp, 0);
            bmp.recycle();
        }
    }
	
    public boolean setTexture(int resourceID) {
        for(int i = 0; i < textureResources.size(); i++) {
            if(resourceID == textureResources.get(i)) {		
                gl.glBindTexture(GL10.GL_TEXTURE_2D, textures[i]);
                return true;
            }
        }
        return false;
    }
}
```