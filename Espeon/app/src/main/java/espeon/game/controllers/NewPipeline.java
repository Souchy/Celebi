package espeon.game.controllers;

import java.util.List;

import espeon.game.jade.EffectModel;
import espeon.game.jade.Statement;
import espeon.game.jade.Statement.StatementGroup;
import espeon.game.red.Action;
import espeon.game.red.Board;
import espeon.game.red.Cell;
import espeon.game.red.Effect;

public class NewPipeline {

    public abstract class Node {
        public int sourceid;
        public int cellid;
        public Node parent;
    }

    public class NodeGroup extends Node {
        public Context context;
        public List<Node> children;
        public NodeGroup newGroup() {
            NodeGroup n = new NodeGroup();
            n.context = context.copy();
            n.parent = this;
            return n;
        }
        public NodeEffect newEffect() {
            NodeEffect n = new NodeEffect();
            n.parent = this;
            return n;
        }
    }
    
    public class NodeEffect extends Node {
        public Effect e;
    }

    // -------

    // public List<Node> steps;


    public void pushAction(Action a, int sourceid, int cellid) {
        NodeGroup root = new NodeGroup();
        root.sourceid = sourceid;
        // steps.add(root);
        for(var s : a.statements) {
            prepareStatements(root, s);
        }
        applyEffects(root);
    }

    private void applyEffects(Node n) {
        if(n instanceof NodeGroup g) {

        } else {
            
        }
    }

    private void prepareStatements(NodeGroup parent, Statement s) {
        NodeGroup step = parent.newGroup();
        if(s.isGroup()) {
            StatementGroup group = s.asGroup();
            if(Conditions.verify(group.condition, this)) {
                for (Statement line : group.children) {
                    prepareStatements(step, line);
                }
            } else {
                for (Statement line : group.childrenOtherwise) {
                    prepareStatements(step, line);
                }
            }
        } else {
            EffectModel em = s.asEffect();
            Board board = Diamonds.getFightByClient(parent.sourceid).board;
            List<Cell> cells = board.getCellsInAoe(parent.cellid, em.aoe);
            
            for(var c : cells) { 
                // System.out.printf("Cell in aoe [%s] {%s, %s} \n", c.id, c.getX(), c.getY());

                // apply to the cell
                if(em.appliesCells) {
                    NodeEffect nodeEffect = step.newEffect();
                    nodeEffect.e = new Effect();
                    nodeEffect.e.model = em;
                    nodeEffect.e.entityid = c.id;
                }
                // apply to the creatures on the cell
                if(em.appliesCreatures) {
                    for(int i = 0; i < em.towerHeightMax; i++) {
                        int height = em.towerFromBottom ? i :  c.creatures.size() - i;
                        if(height >= c.creatures.size() || height < 0)  {
                            break;
                        } else {
                            NodeEffect nodeEffect = step.newEffect();
                            nodeEffect.e = new Effect();
                            nodeEffect.e.model = em;
                            nodeEffect.e.entityid = c.creatures.get(height);
                        }
                    }
                }
            }
        }
    }


}
