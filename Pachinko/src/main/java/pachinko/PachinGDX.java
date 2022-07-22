package pachinko;

import com.badlogic.gdx.backends.lwjgl3.Lwjgl3Application;
import com.badlogic.gdx.backends.lwjgl3.Lwjgl3ApplicationConfiguration;
import com.badlogic.gdx.files.FileHandle;

import java.io.File;
import java.util.Arrays;

// import com.mygdx.game.MyGdxGame;
import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.utils.ScreenUtils;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.backends.lwjgl3.Lwjgl3Graphics;
import com.badlogic.gdx.graphics.GL20;
// import imgui.Context;
import imgui.ImGui;
// import imgui.impl.ImplGL3;
// import imgui.impl.LwjglGlfw;
// import uno.glfw.GlfwWindow;

public class PachinGDX extends ApplicationAdapter {
    
	public static void main (String[] arg) {
		Lwjgl3ApplicationConfiguration config = new Lwjgl3ApplicationConfiguration();
		config.setForegroundFPS(60);
		config.setTitle("My GDX Game");
        config.setWindowIcon("Pachinko/91757405.jpg");
		new Lwjgl3Application(new PachinGDX(), config);
	}

	SpriteBatch batch;
	Texture img;
	
	@Override
	public void create () {
		batch = new SpriteBatch();
        // System.out.println("path : " + new File("").getAbsolutePath());
        // System.out.println("files : " + Arrays.toString(new File("").listFiles()));
        var fh = new FileHandle(new File("Pachinko/91757405.jpg"));
		img = new Texture(fh);
	}

	@Override
	public void render () {
		ScreenUtils.clear(1, 0, 0, 1);
		batch.begin();
		batch.draw(img, 0, 0);
		batch.end();
	}
	
	@Override
	public void dispose () {
		batch.dispose();
		img.dispose();
	}

}
