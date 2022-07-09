package pachinko;

import espeon.game.red.*;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Set;

import espeon.emerald.amber.Amber;
import espeon.game.jade.Position;
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
import imgui.type.ImBoolean;
import imgui.type.ImInt;
import pachinko.windows.ActionWindow;
import pachinko.windows.CreatureWindow;
import pachinko.windows.SpellModelWindow;

public class Pachinko1 extends Application {

    public static void main(String[] args) {
        launch(new Pachinko1());
    }


    // private Aoe aoe = new Aoe(5, 5, 0);
    public static final List<Window> windows = new ArrayList<>();

    private Pachinko1() {
    }

    @Override
    protected void preRun() {
        super.preRun();
        ImGui.getIO().addConfigFlags(ImGuiConfigFlags.DockingEnable);
        // ImGui.getIO().addConfigFlags(ImGuiConfigFlags.ViewportsEnable);

        // windows.add(new SpellModelWindow("1"));
        // windows.add(new AoeWindow(aoe));
    }


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

        // ImGui.end();

        for(int i = 0; i < windows.size(); i++)
            windows.get(i).render();

    }



}
