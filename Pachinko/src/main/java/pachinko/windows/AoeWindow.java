package pachinko.windows;

import imgui.ImGui;
import imgui.flag.ImGuiCol;
import imgui.flag.ImGuiTableFlags;
import pachinko.Window;
import espeon.game.jade.Target.TargetType;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.game.red.Aoe;
import espeon.game.red.Position;

public class AoeWindow implements Window {
    
    private Aoe aoe;
    private Position cursorHover = new Position();
    private Position selected = new Position();

    public AoeWindow(Aoe aoe) {
        this.aoe = aoe;
    }

    @Override
    public void render() { //private void renderAoeWindow() {
        ImGui.begin("Aoe##" + this.hashCode());
        
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

            renderAoeTable();

            var filter = aoe.get(selected.y, selected.x);

            renderFilterButton(TargetType.nothingStr, TargetType.nothing, TargetType.isNothing(filter), filter);
            for(TargetType type : TargetType.values()) {
                renderFilterButton(type.name(), type.value,  type.isIn(filter), filter);
            }
            renderFilterButton(TargetType.allStr, TargetType.all, TargetType.isAll(filter), filter);
        }
        ImGui.end();
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

    private void renderAoeTable() {
        ImGui.beginTable("aoe", aoe.getWidth() + 1, ImGuiTableFlags.SizingFixedFit);
        for(int y = 0; y < aoe.getHeight(); y++) {
            
            if(y == 0) {
                // ImGui.tableHeadersRow();
                ImGui.tableNextColumn();
                for(int x = 0; x < aoe.getWidth(); x++) {
                    ImGui.tableNextColumn();
                    ImGui.tableHeader(x + "##x");
                }
                // continue;
            } 
            ImGui.tableNextRow();
            ImGui.tableNextColumn();
            // ImGui.text("" + y);
            ImGui.tableHeader(y + "##y");

            // ImGui::TableSetBgColor(ImGuiTableBgTarget_CellBg, ImGui::GetColorU32(ImVec4(0.2f, 0.2f, 0.2f, 1)));
            for(int x = 0; x < aoe.getWidth(); x++) {
                ImGui.tableNextColumn();
                var str = x + "," + y; // "x" + x + "y" + y;
                
                if(x == cursorHover.x || y == cursorHover.y) {
                    ImGui.tableSetBgColor(imgui.flag.ImGuiTableBgTarget.CellBg, ImGui.getColorU32(0.2f, 0.2f, 0.2f, 1));
                }
                try {
                    var value = aoe.get(y, x);
                    
                    // ImVec4 baseColor = ImGui.getStyle().getColor(ImGuiCol.Button);

                    
                    // var color = value > 0 ? baseColor : new ImVec4(0.1f, 0.1f, 0.1f, 1);
                    // ImGui.getColorU32(arg0, arg1, arg2, arg3)
                    if(value == TargetType.nothing) 
                        ImGui.pushStyleColor(ImGuiCol.Button, ImGui.getColorU32(0.1f, 0.1f, 0.1f, 1f));
                    if(ImGui.button(value + "##" + str, 32, 32)) {
                        selected.x = x;
                        selected.y = y;
                    }
                    if(value == TargetType.nothing) 
                        ImGui.popStyleColor();

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
