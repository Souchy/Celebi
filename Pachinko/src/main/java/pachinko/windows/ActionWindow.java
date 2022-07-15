package pachinko.windows;

import java.util.HashMap;
import java.util.Map;

import espeon.emerald.amber.Amber;
import imgui.ImGui;
import imgui.type.ImString;
import pachinko.Pachinko;
import pachinko.Window;

public class ActionWindow implements Window {
    
    public static Map<String, ActionWindow> windows = new HashMap<>();
    private static ImString newActionNameBean = new ImString();
    
    public static void open(ImString id) {
        if(!windows.containsKey(id.get())) {
            windows.put(id.get(), new ActionWindow(id));
        }
    }

    public static void renderActions() {
        ImGui.begin("Actions");
        
        ImGui.inputText("##createAction", newActionNameBean);
        ImGui.sameLine();
        if(ImGui.button("Create new")) {
            // ...
            Amber.actions.create(newActionNameBean.get());
            open(new ImString(newActionNameBean.get()));
        }
        
        var ids = Amber.actions.keys();
        if(ImGui.beginListBox("##actions")) {
            for(var s : ids) {
                ImGui.text(s);
                if(ImGui.button("x##" + s)) {
                    Amber.actions.delete(s);
                }
                ImGui.sameLine();
                if(ImGui.button("Edit##"+s)) {
                    open(new ImString(s));
                }
            }
            ImGui.endListBox();
        }

        ImGui.end();
    }

    private final ImString actionBean;
    public ActionWindow(ImString actionBean) {
        this.actionBean = actionBean;
    }

    @Override
    public void render() {
        ImGui.begin("Action");
        Amber.actions.getStatements(actionBean.get());


        ImGui.end();
    }

}
