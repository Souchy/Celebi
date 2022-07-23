package pachinko.windows;

import java.util.HashMap;
import java.util.Map;

import org.bson.conversions.Bson;
import org.checkerframework.checker.units.qual.s;

import com.mongodb.client.model.Filters;

import espeon.emerald.Emerald;
import espeon.emerald.amber.Amber;
import espeon.game.types.Mod;
import espeon.util.Util;
import imgui.ImGui;
import imgui.flag.ImGuiComboFlags;
import imgui.flag.ImGuiInputTextFlags;
import imgui.type.ImInt;
import imgui.type.ImString;
import pachinko.Pachinko;
import pachinko.Window;

public class SpellModelWindow implements Window {

    public static Map<String, SpellModelWindow> windows = new HashMap<>();
    private static ImString newSpellNameBean = new ImString();

    public static void open(String id) {
        if(!windows.containsKey(id)) {
            windows.put(id, new SpellModelWindow(id));
        }
    }
    
    public static void renderSpells() {
        ImGui.begin("Spell Models");
        
        if(ImGui.button("Create new##createspellbtn")) {
            Amber.spells.create(newSpellNameBean.get());
            open(newSpellNameBean.get());
        }
        ImGui.sameLine();
        ImGui.inputText("New spell name##createSpell", newSpellNameBean);
        
        ImGui.newLine();
        var ids = Amber.spells.keys();
        for (var s : ids) {
            var data = s.split(":");
            var strid = data[data.length - 1];

            ImGui.text(s);
            ImGui.sameLine();
            if (ImGui.button("x##" + s)) {
                Amber.spells.delete(strid);
            }
            ImGui.sameLine();
            if (ImGui.button("Edit##" + s)) {
                open(strid);
            }
        }

        ImGui.end();

        for(var w : windows.values())
            w.render();
    }



    /**
     * Spell Model id currently being edited
     */
    private String id;
    private ImString actionBean;
    private ImInt rangeBean;

    public SpellModelWindow(String spellModel) {
        this.id = spellModel;
        this.actionBean = new ImString(Amber.spells.getActionId(id));
        Integer range = Amber.spells.getRange(id);
        if(range == null)  this.rangeBean = new ImInt(1);
        else this.rangeBean = new ImInt(range);
    }

    @Override
    public void render() {
        ImGui.begin("Spell "+id+" ##"+id);
        
        ImGui.text("Action");
        ImGui.sameLine();
        if(ImGui.inputText("##Action ID", actionBean)) {
            Amber.spells.setActionId(id, actionBean.get());
        }
        ImGui.sameLine();
        if(ImGui.button("Open")) {
            ActionWindow.open(actionBean.get());
        }

        ImGui.text("Range");
        ImGui.sameLine();
        if(ImGui.inputInt("##Range", rangeBean)) {
            Amber.spells.setRange(id, rangeBean.get());
        }

        renderAddCost();
        renderCosts();

        ImGui.end();
    }


    private String addCostName = Mod.ap.name();
    private ImInt addCostValue = new ImInt();

    private void renderAddCost() {
        ImGui.newLine();
        ImGui.text("Add Cost");
        if (ImGui.beginCombo("##addCostname", addCostName, ImGuiComboFlags.HeightLarge)) {
            for (var mod : Mod.values()) {
                boolean selected = mod.name().equals(addCostName);
                if (ImGui.selectable(mod.name(), selected)) {
                    addCostName = mod.name();
                }
                if (selected) {
                    ImGui.setItemDefaultFocus();
                }
            }
            ImGui.endCombo();
        }
        // ImGui.sameLine();
        if (ImGui.inputInt("##addCostValue", addCostValue)) {

        }
        // ImGui.sameLine();
        if (ImGui.button("Set##setCost")) {
            Amber.spells.setCost(id, Mod.valueOf(addCostName), String.valueOf(addCostValue.get()));
        }
    }

    
    private void renderCosts() {
        ImGui.newLine();
        ImGui.text("Costs");

        Map<String, String> costs = Amber.spells.getCosts(id);
        for(String mod : costs.keySet()) {
            String cost = costs.get(mod);
            // var valueBean = new ImString(cost);
            var valueBean = new ImInt(Util.parseInt(cost));
            
            if(ImGui.button("x##"+mod)) {
                Amber.spells.deleteCost(id, Mod.valueOf(mod));
            }
            ImGui.sameLine();

            ImGui.text(mod);
            ImGui.sameLine();
            
            if(ImGui.inputInt("##"+mod, valueBean)) {
                Amber.spells.setCost(id, Mod.valueOf(mod), String.valueOf(valueBean.get()));
            }
        }

    }
     
}
