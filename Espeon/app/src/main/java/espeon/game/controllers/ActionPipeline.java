package espeon.game.controllers;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Stack;

import espeon.game.jade.EffectModel;
import espeon.game.jade.Mod;
import espeon.game.jade.Statement;
import espeon.game.jade.Statement.StatementGroup;
import espeon.game.red.Board;
import espeon.game.red.Cell;
import espeon.game.red.Effect;
import espeon.game.red.Stats;
import espeon.game.red.compiledeffects.CompiledEffect;

public class ActionPipeline {

    /*
     Action1 {
        sourceid
        actionid
        cellid
        statements [
            s1 {

            }
            s2 {

            }
        ]
     }
     */

     /*
      * Exemples: 
      * Piège magnétique : liste tous les persos, puis applique l'attirance pour chacun
      * 
      */

    /*
    Action1 {
        statement push (positions1)
            effect push creature1
            effect push creature2
            effect push creature3
        Action2 {
            statement damage (positions2)
                effect damage creature1
        }
        statement damage (positions1)
            effect damage creature1
            effect damage creature2
            effect damage creature3
    }
    */

    /*
     * EffectAction
     *      
     */

     /*
      StatementGroup {
        Context context1;
        List<statements> [
            statement push (positions1) {
                effect push creature1,
                effect push creature2,
                effect push creature3
            },
            StatementGroup {
                Context context2 = context1.copy()
                List<statements> []
            }
            statement push (positions1) {
                effect push creature1,
                effect push creature2,
                effect push creature3
            },
        ]
      }
      */

    /*
    Context {
        map<int, int> positions;
        map<int, stats> stats;
    }
    */

    /*
     * Action
     * Spell : Action
     * Trigger : Action
     * both have a list of statements to execute when they are activated 
     */

    
    public final int sourceid;
    public final int actionid;
    public final int cellid;

    public Stack<EffectAction> stack = new Stack<>();
    public Stack<CompiledEffect> compiled = new Stack<>();

    public Context context;

    public ActionPipeline(int sourceid, int actionid, int cellid) {
        this.sourceid = sourceid;
        this.actionid = actionid;
        this.cellid = cellid;
        Fight f = Diamonds.getFightByClient(sourceid);
        Cell casterCell = f.board.findCreatureCell(sourceid);
        Stats casterStats = Diamonds.getCreatureInstance(sourceid).stats;
        context.stats.put(sourceid, casterStats);
        context.positions.put(sourceid, casterCell.id);
    }

    public void start() {

    }

    public void pushAction(int sourceid, int actionid, int cellid) {
    	
    }
    // public void pushAction(Action a) {
    	
    // }
    
    public void processStatement(Statement s) {
        if(s.isGroup()) {
            StatementGroup group = s.asGroup();
            if(Conditions.verify(group.condition, this)) {
                for (Statement line : group.children) {
                    processStatement(line);
                }
            } else {
                for (Statement line : group.childrenOtherwise) {
                    processStatement(line);
                }
            }
        } else {
            EffectModel em = s.asEffect();
            Board board = Diamonds.getFightByClient(sourceid).board;
            List<Cell> cells = board.getCellsInAoe(this.cellid, em.aoe);
            Cell cell = board.get(cellid);
            System.out.printf("Process statement %s on cellid [%s] {%s, %s} \n", s.hashCode(), cell.id, cell.getX(), cell.getY());
            for(var c : cells) { 
                System.out.printf("Cell in aoe [%s] {%s, %s} \n", c.id, c.getX(), c.getY());
                Effect e = new Effect();
                e.entityid = c.id;
                e.model = em;
                this.push(e, this.sourceid, c.id);
            }
        }
    }

    public void push(Effect e, int sourceid, int cellid) {
        var action = new EffectAction();
        stack.push(action);
        action.effect = e;
        action.sourceid = sourceid;
        action.cellid = cellid;
        var ce = Effects.compile(this, action);
        compiled.push(ce);
    }
    
    public class EffectAction {
        public int sourceid;
        public int cellid;
        public Effect effect;
        // public Map<Integer, Stats> stats = new HashMap<>(lastStats);
        // public Map<Integer, Integer> positions = new HashMap<>(lastPositions);
        // public CompiledEffect compiled;

        public int getStat(int entityid, Mod mod) {
            if(!context.stats.containsKey(entityid)) {
                var stats = Diamonds.getCreatureInstance(entityid).stats;
                context.stats.put(entityid, stats);
            }
            return context.stats.get(entityid).get(mod);
        }
        public void setStat(int entityid, Mod mod, int val) {
            if(!context.stats.containsKey(entityid)) {
                var stats = Diamonds.getCreatureInstance(entityid).stats;
                context.stats.put(entityid, stats);
            }
            context.stats.get(entityid).set(mod, val);
        }

        public int getPosition(int entityid) {
            if(!context.positions.containsKey(entityid)) {
                var cell = Diamonds.getFightByClient(entityid).board.findCreatureCell(entityid);
                context.positions.put(entityid, cell.id);
            }
            return context.positions.get(entityid);
        }
        public void setPosition(int entityid, int cellid) {
            if(!context.positions.containsKey(entityid)) {
                var cell = Diamonds.getFightByClient(entityid).board.findCreatureCell(entityid);
                context.positions.put(entityid, cell.id);
            }
            context.positions.put(entityid, cellid);
        }

        @Override
        public String toString() {
            return "EffectAction@"+hashCode() + ": " + effect.model.type();
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
