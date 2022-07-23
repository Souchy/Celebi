package pachinko.windows;

import java.util.HashMap;
import java.util.Map;

import espeon.emerald.amber.Amber;
import imgui.ImGui;
import imgui.type.ImString;
import pachinko.Window;

public class CreatureWindow implements Window {

    public static Map<String, CreatureWindow> windows = new HashMap<>();
    private static ImString newCreatureNameBean = new ImString();

    
    public static void open(ImString id) {
        if(!windows.containsKey(id.get())) {
            windows.put(id.get(), new CreatureWindow(id));
        }
    }
    public static void renderCreatures() {
        ImGui.begin("Creature Models");
        
        if(ImGui.button("Create new##createcreabtn")) {
            // ...
        }
        ImGui.sameLine();
        ImGui.inputText("New creature nam##createCreature", newCreatureNameBean);

        var ids = Amber.creatures.keys();
        if(ImGui.beginListBox("##creatures")) {
            for(var s : ids) {
                ImGui.text(s);
            }
            ImGui.endListBox();
        }

        ImGui.end();
        
        for(var w : windows.values())
            w.render();
    }

    private ImString id;
    public CreatureWindow(ImString id) {
        this.id = id;
    }

    @Override
    public void render() {
        // TODO Auto-generated method stub
        
    }
    
}
