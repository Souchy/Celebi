package espeon.game.red;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Stack;

import javax.swing.Action;

import espeon.game.Effects;
import espeon.game.jade.EffectModel;
import espeon.game.jade.Mod;
import espeon.game.red.compiledEffects.CompiledEffect;

public class ActionPipeline {
    
    public Stack<EffectAction> stack = new Stack<>();

    public final int sourceid;
    public final int actionid;
    public final int cellid;

    public ActionPipeline(int sourceid, int actionid, int cellid) {
        this.sourceid = sourceid;
        this.actionid = actionid;
        this.cellid = cellid;
    }


    // stats per entity id
    public Map<Integer, Stats> lastStats;
    // cellid per entity id
    public Map<Integer, Integer> lastPositions;

    public void push(EffectModel m, int sourceid, int cellid) {
        var action = new EffectAction();
        stack.push(action);
        action.effect = m;
        action.sourceid = sourceid;
        action.cellid = cellid;
        Effects.apply(stack.peek());
    }

    public class EffectAction {
        public EffectModel effect;
        public int sourceid;
        public int cellid;
        public Map<Integer, Stats> stats = new HashMap<>(lastStats);
        public Map<Integer, Integer> positions = new HashMap<>(lastPositions);
        public CompiledEffect compiled;
        public void setStat(int entityid, Mod mod, int val) {
            lastStats.get(entityid).stats.put(mod, val);
        }
        public void setPosition(int entityid, int cellid) {
            lastPositions.put(entityid, cellid);
        }
    }

    public static class Step {
        // stats before
        // stats after ??
    }


    /*
     * Exemple réseau:
     * Flèche de recul sur Monstre 1 pousse puis tape.
     * Stack = { flechePousse, flecheTape }
     * M déclenche les pièges dans l'ordre de pose. -> foreach cell.status -> stack.pushEnd EffectAction
     * Stack = { flechePousse, repuPousse, repuTape, sournoisTire, sournoisTape, dérivePousse, dériveTape flecheTape }
     * M arrive sur la cell 2 durant le répu et déclenche d'autre pièges -> foreach cell.stats -> stack.push(index) EffectAction
     * Stack = { flechePousse, repuPousse, { répu2, sournois2, dérive2 }, repuTape, sournoisTire, sournoisTape, dérivePousse, dériveTape, flecheTape }
     */


     /*
      foreach spell.statement {

      }
      */

}
