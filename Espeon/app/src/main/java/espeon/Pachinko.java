package espeon;

import espeon.game.red.*;
import espeon.game.jade.Target.TargetType;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.util.Table;
import imgui.ImGui;
import imgui.app.Application;
import imgui.app.Configuration;
import imgui.flag.ImGuiTableFlags;
import imgui.type.ImInt;

public class Pachinko extends Application {

    public static void main(String[] args) {
        launch(new Pachinko());
    }


    // private Table<TargetTypeFilter> table = new Table<>(5, 5, new TargetTypeFilter());
    private Aoe aoe = new Aoe(5, 5);

    private Pachinko() {
        aoe.addColumn();
    }

    @Override
    protected void configure(Configuration config) {
        config.setTitle("Dear ImGui is Awesome!");
    }

    private int x[] = new int[] { 5 };
    private int y[] = new int[] { 5 };
    // private ImInt imval = new ImInt();

    @Override
    public void process() {
        ImGui.text("Hello, World!");

        ImGui.sliderInt("x", x, 1, 16);
        ImGui.sliderInt("y", y, 1, 16);

        if(ImGui.button("Add Col")) {
            aoe.addColumn();
        } else 
        if(ImGui.button("Add Row")) {
            aoe.addRow();
        } else {
            ImGui.beginTable("aoe", aoe.getWidth(), ImGuiTableFlags.Borders);
            for(int i = 0; i < aoe.getWidth(); i++) {
                ImGui.tableNextRow();
                for(int j = 0; j < aoe.getHeight(); j++) {
                    ImGui.tableNextColumn();
                    // ImGui.button("" + i + ", " + j);
                    // ImGui.button("" + table.get(i, j));
                    try {
                        ImInt imval = new ImInt(aoe.get(i, j).value);
                        if(ImGui.button("##i" + i + "j" + j)) { //ImGui.inputInt("##i" + i + "" + j, imval)) {
                            aoe.set(i, j, new TargetTypeFilter(imval.get()));
                        }
                    } catch(Exception e) {
                        System.out.printf("Error trying to display button at cell {%s, %s}\n", i, j);
                        // e.printStackTrace();
                    }
                }
            }
            ImGui.endTable();
        }
    }

}
