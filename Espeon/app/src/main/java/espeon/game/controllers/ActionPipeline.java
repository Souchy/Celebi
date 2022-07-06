package espeon.game.controllers;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Stack;

import javax.swing.Action;

import espeon.game.jade.EffectModel;
import espeon.game.jade.Mod;
import espeon.game.jade.Statement;
import espeon.game.jade.Statement.StatementGroup;
import espeon.game.red.Board;
import espeon.game.red.Stats;
import espeon.game.red.Board.Cell;
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


    
    public final int sourceid;
    public final int actionid;
    public final int cellid;

    public Stack<EffectAction> stack = new Stack<>();
    public Stack<CompiledEffect> compiled = new Stack<>();

    // stats per entity id
    public Map<Integer, Stats> lastStats = new HashMap<>();
    // cellid per entity id
    public Map<Integer, Integer> lastPositions = new HashMap<>();
    

    public ActionPipeline(int sourceid, int actionid, int cellid) {
        this.sourceid = sourceid;
        this.actionid = actionid;
        this.cellid = cellid;
        Fight f = Diamonds.getFightByClient(sourceid);
        Cell casterCell = f.board.findCreatureCell(sourceid);
        Stats casterStats = Diamonds.getCreatureInstance(sourceid).stats;
        lastStats.put(sourceid, casterStats);
        lastPositions.put(sourceid, casterCell.id);
    }

    public void start() {

    }
    
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
            EffectModel effect = s.asEffect();
            Board board = Diamonds.getFightByClient(sourceid).board;
            List<Cell> cells = board.getCellsInAoe(this.cellid, effect.aoe);
            Cell cell = board.get(cellid);
            System.out.printf("Process statement %s on cellid [%s] {%s, %s} \n", s.hashCode(), cell.id, cell.getX(), cell.getY());
            for(var c : cells) { 
                System.out.printf("Cell in aoe [%s] {%s, %s} \n", c.id, c.getX(), c.getY());
                this.push(effect, this.sourceid, c.id);
            }
        }
    }

    public void push(EffectModel m, int sourceid, int cellid) {
        var action = new EffectAction();
        stack.push(action);
        action.effect = m;
        action.sourceid = sourceid;
        action.cellid = cellid;
        var e = Effects.compile(this, action);
        compiled.push(e);
    }
    
    public class EffectAction {
        public int sourceid;
        public int cellid;
        public EffectModel effect;
        // public Map<Integer, Stats> stats = new HashMap<>(lastStats);
        // public Map<Integer, Integer> positions = new HashMap<>(lastPositions);
        // public CompiledEffect compiled;
        public int getStat(int entityid, Mod mod) {
            if(!lastStats.containsKey(entityid)) {
                var stats = Diamonds.getCreatureInstance(entityid).stats;
                lastStats.put(entityid, stats);
            }
            return lastStats.get(entityid).get(mod);
        }
        public void setStat(int entityid, Mod mod, int val) {
            if(!lastStats.containsKey(entityid)) {
                var stats = Diamonds.getCreatureInstance(entityid).stats;
                lastStats.put(entityid, stats);
            }
            lastStats.get(entityid).stats.put(mod, val);
        }
        public int getPosition(int entityid) {
            if(!lastPositions.containsKey(entityid)) {
                var cell = Diamonds.getFightByClient(entityid).board.findCreatureCell(entityid);
                lastPositions.put(entityid, cell.id);
            }
            return lastPositions.get(entityid);
        }
        public void setPosition(int entityid, int cellid) {
            if(!lastPositions.containsKey(entityid)) {
                var cell = Diamonds.getFightByClient(entityid).board.findCreatureCell(entityid);
                lastPositions.put(entityid, cell.id);
            }
            lastPositions.put(entityid, cellid);
        }
        @Override
        public String toString() {
            return "EffectAction@"+hashCode() + ": " + effect.type();
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
