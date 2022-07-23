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

import espeon.emerald.Emerald;
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

// import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class Pachinko extends PachinGDX {

    public static void main(String[] args) {
        Emerald.init();
        launch(new Pachinko());
    }

    public static final List<Window> windows = new ArrayList<>();

    private Pachinko() {
        // FXMLController adsf;
    }

    private SpellModelTable table = new SpellModelTable();

    @Override
    public void renderPachinko() {
        SpellModelWindow.renderSpells();
        ActionWindow.renderActions();
        CreatureWindow.renderCreatures();
        table.render();

        for(int i = 0; i < windows.size(); i++)
            windows.get(i).render();

    }



}
