package pachinko;

import espeon.game.red.*;
import espeon.game.jade.Position;
import espeon.game.jade.Target;
import espeon.game.jade.Target.TargetType;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.util.Table;
import imgui.ImGui;
import imgui.ImVec2;
import imgui.app.Application;
import imgui.app.Configuration;
import imgui.flag.ImGuiCol;
import imgui.flag.ImGuiSelectableFlags;
import imgui.flag.ImGuiTableColumnFlags;
import imgui.flag.ImGuiTableFlags;
import imgui.type.ImInt;

public class Pachinko1 extends Application {

    public static void main(String[] args) {
        launch(new Pachinko1());
    }


    private Aoe aoe = new Aoe(5, 5, 0);

    private Pachinko1() {
        // aoe.addColumn();
    }

    @Override
    protected void configure(Configuration config) {
        config.setTitle("Dear ImGui is Awesome!");
    }

    // private int x[] = new int[] { 5 };
    // private int y[] = new int[] { 5 };
    // private ImInt imval = new ImInt();

    private Position cursorHover = new Position();
    private Position selected = new Position();

    @Override
    public void process() {
        ImGui.text("Hello, World!");

        // ImGui.sliderInt("x", x, 1, 16);
        // ImGui.sliderInt("y", y, 1, 16);

        if(ImGui.button("Add Col")) {
            aoe.addColumn();
        } else 
        if(ImGui.button("Add Row")) {
            aoe.addRow();
        } else {
            ImGui.text("Size {" + aoe.getWidth() + ", " + aoe.getHeight() + "}");
            ImGui.text("Hover {" + cursorHover.x + ", " + cursorHover.y + "}");
            ImGui.text("Selected {" + selected.x + ", " + selected.y + "}");
            // ImGuiSelectableFlags.SpanAllColumns
            // ImInt selectedVal = null;

            renderAoe();

            var filter = aoe.get(selected.y, selected.x);

            renderFilterButton(TargetType.nothingStr, TargetType.nothing, TargetType.isNothing(filter), filter);
            for(TargetType type : TargetType.values()) {
                renderFilterButton(type.name(), type.value,  type.isIn(filter), filter);
            }
            renderFilterButton(TargetType.allStr, TargetType.all, TargetType.isAll(filter), filter);
        }
    }

    private void renderFilterButton(String name, int typeBit, boolean active, int filter) {
        var color = active ? ImGui.getColorU32(0.2f, 0.2f, 1.0f, 1) : ImGui.getColorU32(1f, 0.2f, 0.2f, 1);
        ImGui.pushStyleColor(ImGuiCol.Button, color);
        if(ImGui.button(name)) {
            if(active) {
                aoe.set(selected.y, selected.x, TargetTypeFilter.sub(filter, typeBit));
            } else {
                aoe.set(selected.y, selected.x, TargetTypeFilter.add(filter, typeBit));
            }
        }
        ImGui.popStyleColor();
    }

    private void renderAoe() {
        ImGui.beginTable("aoe", aoe.getWidth(), ImGuiTableFlags.Borders);
        for(int y = 0; y < aoe.getHeight(); y++) {
            ImGui.tableNextRow();
            // ImGui::TableSetBgColor(ImGuiTableBgTarget_CellBg, ImGui::GetColorU32(ImVec4(0.2f, 0.2f, 0.2f, 1)));
            for(int x = 0; x < aoe.getWidth(); x++) {
                ImGui.tableNextColumn();
                var str = x + "," + y; // "x" + x + "y" + y;
                
                if(x == cursorHover.x || y == cursorHover.y) {
                    ImGui.tableSetBgColor(imgui.flag.ImGuiTableBgTarget.CellBg, ImGui.getColorU32(0.2f, 0.2f, 0.2f, 1));
                }
                try {
                    var value = aoe.get(y, x);

                    if(ImGui.button(value + "##" + str, 32, 32)) {
                        selected.x = x;
                        selected.y = y;
                    }

                    if(ImGui.isItemHovered()) {
                        cursorHover.x = x;
                        cursorHover.y = y;
                    }
                    if(x == selected.x && y == selected.y) {
                        ImGui.tableSetBgColor(imgui.flag.ImGuiTableBgTarget.CellBg, ImGui.getColorU32(1f, 0.2f, 0.2f, 1));
                    }
                } catch(Exception e) {
                    System.out.println("Cell : " + str);
                    e.printStackTrace();
                }
            }
        }
        ImGui.endTable();
    }


}
