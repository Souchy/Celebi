package espeon;

import espeon.game.jade.Target.TargetType;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.util.Table;
import imgui.ImGui;
import imgui.app.Application;
import imgui.app.Configuration;
import imgui.flag.ImGuiTableFlags;
import imgui.type.ImInt;

public class Pachinko extends Application {


    private Table<TargetTypeFilter> table = new Table<>(5, 5, new TargetTypeFilter());


    public static void main(String[] args) {
        launch(new Pachinko());
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
            table.addColumn(x[0]);
        }
        if(ImGui.button("Add Row")) {
            table.addRow(y[0]);
        }

        ImGui.beginTable("aoe", table.getWidth(), ImGuiTableFlags.Borders);
        for(int i = 0; i < table.getWidth(); i++) {
            ImGui.tableNextRow();
            for(int j = 0; j < table.getHeight(); j++) {
                ImGui.tableNextColumn();
                // ImGui.button("" + i + ", " + j);
                // ImGui.button("" + table.get(i, j));
                ImInt imval = new ImInt(table.get(i, j).value);
                if(ImGui.inputInt("%i" + i + "" + j, imval)) {
                    table.set(i, j, new TargetTypeFilter(imval.get()));
                }
            }
        }
        ImGui.endTable();
    }

}