package pachinko.windows;

import java.util.Set;

import espeon.emerald.amber.Amber;
import imgui.ImGui;
import imgui.flag.ImGuiTableFlags;
import imgui.type.ImInt;
import imgui.type.ImString;
import pachinko.Window;

public class SpellModelTable implements Window {
    
    public String[] keys;
    public int page = 0;

    public SpellModelTable() {
        refresh();
    }

    @Override
    public void render() {
        ImGui.begin("Spell Table##spelltable");

        if(ImGui.button("Refresh")) {
            refresh();
        }
        if(ImGui.button("<<")) {
            if(page > 0)
                page--;
        }
        ImGui.sameLine();
        ImGui.text("page " + page);
        ImGui.sameLine();
        if(ImGui.button(">>")) {
            if(page * 100 < keys.length)
                page++;
        }

        renderTable();
        ImGui.end();
    }


    private void renderTable() {
        ImGui.beginTable("##spellTable", 5, ImGuiTableFlags.SizingFixedFit);

        ImGui.tableHeader("ID");
        ImGui.tableHeader("ActionID");
        // ImGui.tableHeader("Conditions");
        ImGui.tableHeader("Casts/Target");
        ImGui.tableHeader("Casts/Turn");
        ImGui.tableHeader("Cooldown");
        ImGui.tableHeader("AoeConditions");
        ImGui.tableHeader("Costs");

        for(int i = page * 100; i < (page+1) * 100; i++) {
            if(i >= keys.length) break;
            ImGui.tableNextRow();
            String id = keys[i];
            ImGui.tableNextColumn();
            var imId = new ImString(id);
            ImGui.inputText("##spellid", imId);
            ImGui.tableNextColumn();
            var imAction = new ImString(Amber.spells.getActionId(id));
            ImGui.inputText("##spellaction", imAction);
            ImGui.tableNextColumn();
            var imCastTarget = new ImInt(Amber.spells.conditions.getMaxCastsPerTarget(id));
            ImGui.inputInt("##spellcasttarget", imCastTarget);
            ImGui.tableNextColumn();
            var imCastTurn = new ImInt(Amber.spells.conditions.getMaxCastsPerTurn(id));
            ImGui.inputInt("##spellcastturn", imCastTurn);
            ImGui.tableNextColumn();
            var imCooldown = new ImInt(Amber.spells.conditions.getCooldown(id));
            ImGui.inputInt("##spellcooldown", imCooldown);
            ImGui.tableNextColumn();
            var imCellCond = new ImString(String.valueOf(Amber.spells.conditions.getCellConditions(id)));
            ImGui.inputText("##spellCellConditions", imCellCond);
            ImGui.tableNextColumn();
            ImGui.inputText("##spellcosts", new ImString());
        }

        ImGui.endTable();
    }

    public void refresh() {
        var keys = Amber.spells.keys();
        this.keys = keys.toArray(new String[keys.size()]);
    }

}
