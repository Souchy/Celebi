package pachinko;

import com.badlogic.gdx.backends.lwjgl3.Lwjgl3Application;
import com.badlogic.gdx.backends.lwjgl3.Lwjgl3ApplicationConfiguration;
import com.badlogic.gdx.files.FileHandle;

import java.io.File;
import java.util.Arrays;

import org.lwjgl.glfw.GLFW;

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
import imgui.flag.ImGuiConfigFlags;
import imgui.gl3.ImGuiImplGl3;
import imgui.glfw.ImGuiImplGlfw;
import pachinko.windows.ActionWindow;
import pachinko.windows.CreatureWindow;
import pachinko.windows.SpellModelTable;
import pachinko.windows.SpellModelWindow;

public class PachinGDX extends ApplicationAdapter {
    
	public static void main (String[] arg) {
		Lwjgl3ApplicationConfiguration config = new Lwjgl3ApplicationConfiguration();
		config.setForegroundFPS(60);
		config.setTitle("My GDX Game");
        config.setWindowIcon("Pachinko/91757405.jpg");
		new Lwjgl3Application(new PachinGDX(), config);
	}

    
    private final ImGuiImplGlfw imGuiGlfw = new ImGuiImplGlfw();
    private final ImGuiImplGl3 imGuiGl3 = new ImGuiImplGl3();
    
    private SpellModelTable table = new SpellModelTable();
    
    @Override
    public void create() {
        Lwjgl3Graphics lwjgl3Graphics = (Lwjgl3Graphics) Gdx.graphics;
        long windowHandle = lwjgl3Graphics.getWindow().getWindowHandle();

        ImGui.createContext();
        ImGui.getIO().addConfigFlags(ImGuiConfigFlags.DockingEnable);
        imGuiGlfw.init(windowHandle, true);
        imGuiGl3.init("#version 130");
        
        Gdx.graphics.setWindowedMode(1600, 900);
    }

    @Override
    public void render() {
        Gdx.gl.glClearColor(0f, 0f, 0f, 1f);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT | GL20.GL_DEPTH_BUFFER_BIT);
        imGuiGlfw.newFrame();
        ImGui.newFrame();

        renderImGui();

        ImGui.render();
        if (ImGui.getDrawData() != null) {
            imGuiGl3.renderDrawData(ImGui.getDrawData());
        }
    }

    private void renderImGui() {
        // ImGui.showDemoWindow();
        SpellModelWindow.renderSpells();
        ActionWindow.renderActions();
        CreatureWindow.renderCreatures();
        table.render();

        for(int i = 0; i < Pachinko.windows.size(); i++)
            Pachinko.windows.get(i).render();
    }

    @Override
    public void dispose() {
        // imGuiGlfw.shutdown();
        // ctx.shutdown();
    }

}
