package pachinko.windows;

import java.util.HashMap;
import java.util.Map;

import com.mongodb.client.model.Filters;

import espeon.emerald.Emerald;
import espeon.emerald.amber.Amber;
import espeon.game.jade.Action;
import espeon.game.jade.EffectModel;
import espeon.game.jade.Statement;
import espeon.game.jade.Statement.StatementEffect;
import espeon.game.jade.Statement.StatementGroup;
import espeon.game.jade.effects.AddStatusEffect;
import espeon.game.jade.effects.DamageEffect;
import espeon.game.jade.effects.SummonEffect;
import espeon.game.jade.effects.moves.MoveBy;
import espeon.game.jade.effects.moves.MoveSymmetrically;
import espeon.game.jade.effects.moves.MoveTo;
import espeon.game.jade.effects.moves.MoveToPrevious;
import espeon.game.jade.effects.moves.MoveEffect.MoveType;
import imgui.ImGui;
import imgui.type.ImString;
import pachinko.Pachinko;
import pachinko.Window;

public class ActionWindow implements Window {
    
    public static Map<String, ActionWindow> windows = new HashMap<>();
    private static ImString newActionNameBean = new ImString();
    
    public static void open(Action a) { //ImString id) {
        if(!windows.containsKey(a.id)) {
            windows.put(a.id, new ActionWindow(a));
        }
    }
    public static void open(String id) {
        if(!windows.containsKey(id)) {
            var action = Emerald.actions().find(Filters.eq("id", id)).first();
            windows.put(id, new ActionWindow(action));
        }
    }

    public static void renderActions() {
        ImGui.begin("Actions");
        
        if(ImGui.button("Create new##newactionbtn")) {
            Amber.actions.create(newActionNameBean.get());
            var action = new Action();
            action.id = newActionNameBean.get();
            Emerald.actions().insertOne(action);
            open(action); //new ImString(newActionNameBean.get()));
        }
        ImGui.sameLine();
        ImGui.inputText("##createAction", newActionNameBean);
        
        var ids = Amber.actions.keys();
        if(ImGui.beginListBox("##actions")) {
            for(var s : ids) {
                ImGui.text(s);
                if(ImGui.button("x##" + s)) {
                    Amber.actions.delete(s);
                }
                ImGui.sameLine();
                if(ImGui.button("Edit##"+s)) {
                    open(s); //new ImString(s));
                }
            }
            ImGui.endListBox();
        }

        ImGui.end();
        
        for(var w : windows.values())
            w.render();
    }

    private final Action actionBean;
    public ActionWindow(Action actionBean) {
        this.actionBean = actionBean;
    }

    public void save() {
        Emerald.actions().replaceOne(Filters.eq("id", this.actionBean.id), this.actionBean);
    }

    @Override
    public void render() {
        ImGui.begin("Action " + actionBean.id + "##" + actionBean.id);
        // Amber.actions.getStatements(actionBean.get());

        if(ImGui.button("Save")) {
            save();
        }
        if(ImGui.button("New StatementGroup")) {
            actionBean.statements.add(new StatementGroup());
        }

        ImGui.separator();
        for(var s : actionBean.statements) {
            renderStatement(s);
        }
        ImGui.separator();
        renderEffectCreators();

        ImGui.end();
    }

    private StatementEffect selectedEffect;
    public void renderStatement(Statement s) {
        if(s.isGroup()) {
            StatementGroup g = s.asGroup();
            if(ImGui.treeNode("Group " + g.hashCode())) {
                if(ImGui.button("New Effect##" + g.hashCode())) {
                    g.children.add(new StatementEffect());
                }
                for(var child : g.children) renderStatement(child);
                for(var child : g.childrenOtherwise) renderStatement(child);
                ImGui.treePop();
            }
        } else {
            StatementEffect se = (StatementEffect) s;
            if(ImGui.treeNode("StateEffect " + se.hashCode())) {
                selectedEffect = se;
                renderEffect(se);

                ImGui.treePop();
            }
        }
    }

    public void renderEffect(StatementEffect se) {
        EffectModel e = se.asEffect();
        if(e == null) return;
        ImGui.text("Effect type " + e.type());

    }

    public void renderEffectCreators() {
        if(ImGui.button("Damage")) {
            selectedEffect.effect = new DamageEffect();
        }
        if(ImGui.button("Heal")) {
            
        }
        if(ImGui.button("Summon")) {
            selectedEffect.effect = new SummonEffect();
        }
        if(ImGui.button("AddStatus")) {
            selectedEffect.effect = new AddStatusEffect();
        }
        if(ImGui.button("MoveTo")) {
            selectedEffect.effect = new MoveTo(MoveType.translation);
        }
        if(ImGui.button("MoveBy")) {
            selectedEffect.effect = new MoveBy(MoveType.translation);
        }
        if(ImGui.button("MoveSymmetrically")) {
            selectedEffect.effect = new MoveSymmetrically();
        }
        if(ImGui.button("MoveToPrevious")) {
            selectedEffect.effect = new MoveToPrevious();
        }
        if(ImGui.button("AddRessource")) {
            
        }
    }

}
