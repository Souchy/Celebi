package pachinko;

import espeon.game.red.*;

import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Set;

import javax.imageio.ImageIO;

import org.lwjgl.glfw.GLFW;
import org.lwjgl.glfw.GLFWImage;

import espeon.emerald.amber.Amber;
import espeon.game.jade.Target;
import espeon.game.jade.Target.TargetType;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.util.Table;
import imgui.ImGui;
import imgui.ImGuiStyle;
import imgui.ImVec2;
import imgui.ImVec4;
import imgui.app.Application;
import imgui.app.Configuration;
import imgui.flag.ImGuiCol;
import imgui.flag.ImGuiConfigFlags;
import imgui.flag.ImGuiSelectableFlags;
import imgui.flag.ImGuiStyleVar;
import imgui.flag.ImGuiTableColumnFlags;
import imgui.flag.ImGuiTableFlags;
import imgui.flag.ImGuiWindowFlags;
import imgui.internal.ImGuiWindow;
import imgui.internal.ImRect;
import imgui.type.ImBoolean;
import imgui.type.ImInt;
import pachinko.windows.ActionWindow;
import pachinko.windows.CreatureWindow;
import pachinko.windows.SpellModelTable;
import pachinko.windows.SpellModelWindow;

public class Pachinko extends Application {

    public static void main(String[] args) {
        launch(new Pachinko());
    }

    private static final String iconPath = "C:\\Users\\robyn\\Images\\icon16.png";
    // private Aoe aoe = new Aoe(5, 5, 0);
    public static final List<Window> windows = new ArrayList<>();

    private Pachinko() {
        
    }

    @Override
    protected void preRun() {
        super.preRun();
        ImGui.getIO().addConfigFlags(ImGuiConfigFlags.DockingEnable);
        // ImGui.getIO().addConfigFlags(ImGuiConfigFlags.ViewportsEnable);
        
        try {
            final ImageParser resource_01 = ImageParser.load_image(iconPath);
            GLFWImage image = GLFWImage.malloc(); 
            GLFWImage.Buffer imagebf = GLFWImage.malloc(1);
            image.set(resource_01.get_width(), resource_01.get_heigh(), resource_01.get_image());
            imagebf.put(0, image);
            GLFW.glfwSetWindowIcon(handle, imagebf);
        } catch(Exception e) {
            e.printStackTrace();
        }
    }

    private SpellModelTable table = new SpellModelTable();

    @Override
    protected void configure(Configuration config) {
        config.setTitle("Pachinko");
    }

    @Override
    public void process() {
        // ImGui.begin("Pachinko", new ImBoolean(false)); // , ImGuiWindowFlags.MenuBar);
        // if(ImGui.button("new aoe")) {
        // windows.add(new AoeWindow(aoe));
        // }

        SpellModelWindow.renderSpells();
        ActionWindow.renderActions();
        CreatureWindow.renderCreatures();
        table.render();

        // ImGui.end();

        for(int i = 0; i < windows.size(); i++)
            windows.get(i).render();

    }



}
