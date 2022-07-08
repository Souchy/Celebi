package espeon.game.controllers.actionpipeline;

import java.util.List;

import espeon.game.controllers.Board;
import espeon.game.controllers.Conditions;
import espeon.game.controllers.Context;
import espeon.game.controllers.Diamonds;
import espeon.game.controllers.actionpipeline.Node.NodeEffect;
import espeon.game.controllers.actionpipeline.Node.NodeGroup;
import espeon.game.jade.EffectModel;
import espeon.game.jade.Statement;
import espeon.game.jade.Statement.StatementGroup;
import espeon.game.red.Action;
import espeon.game.red.Cell;
import espeon.game.red.Effect;


/*
 * Accessed by GameActionHandler to create a new Pipeline with a new Action
 * Accessed by Effects to add a new Action after an Effect triggered a Trigger
 */
public class NewPipeline {

    /**
     * Root node for the root action
     */
    public NodeGroup root = Node.newRoot();

    public NewPipeline(int fightid) {
        root.context = new Context(); // TODO Context
        root.context.fightid = fightid;
    }
    
    /**
     * Start the pipeline with the root action
     */
    public void start(Action a, int sourceid, int cellid) {
        if(root != null) throw new RuntimeException("Cannot start() Pipeline as it already has a root Node.");
        add(a, sourceid, cellid, null);
    }

    /**
     * Add an Action in the list <i>after</i> a NodeEffect brother
     */
    public void add(Action a, int sourceid, int cellid, NodeEffect brother) {
        // create a new group for the action
        NodeGroup group = null;
        if(brother == null) { //} && root == null) {
            // create a group as the root
            // root = group = Node.newRoot();
            // root.context = new Context(); // TODO Context
            group = root.newGroup();
            root.children.add(group);
        } else {
            // create a group and add it to its brother's parent
            NodeGroup parent = brother.parent;
            group = parent.newGroup();
            brother.addBrothers(group);
        }
        // set the sourceid
        group.sourceid = sourceid;

        // process statements into nodes
        for(var s : a.statements) {
            processStatementsToNodes(group, s, cellid);
        }
        // apply nodes to the context, which might trigger other actions and add them to this group
        applyNodeToContext(group);
    }


    private void applyNodeToContext(Node node) {
        if(node.isGroup()) {
            // applyEffects(node.asGroup());
            List<Node> children = node.asGroup().children;
            for(int i = 0; i < children.size(); i++) {
                applyNodeToContext(children.get(i));
            }
        }
        else 
            applyEffect(node.asEffect());
    }
    
    private void applyEffect(NodeEffect n) {
        // Effects.compile(n);
            // n.addBrothers(null);
    }
    
    /**
     * Transforms statements applied to a cell into node effects <p>
     * If the statement is a group, create a NodeGroup and iterate the its children <br>
     * If the statement is an effectmodel, create a NodeEffect for each cell affected by the aoe <br>
     */
    private void processStatementsToNodes(NodeGroup parent, Statement s, int cellid) {
        NodeGroup step = parent.newGroup();
        if(s.isGroup()) {
            StatementGroup group = s.asGroup();
            if(Conditions.verify(group.condition, this)) {
                for (Statement line : group.children) {
                    processStatementsToNodes(step, line, cellid);
                }
            } else {
                for (Statement line : group.childrenOtherwise) {
                    processStatementsToNodes(step, line, cellid);
                }
            }
        } else {
            EffectModel em = s.asEffect();
            Board board = Diamonds.getFight(parent.getFightId()).board; //Diamonds.getFightByClient(parent.sourceid).board;
            List<Cell> cells = board.getCellsInAoe(cellid, em.aoe);
            
            for(var c : cells) { 
                // System.out.printf("Cell in aoe [%s] {%s, %s} \n", c.id, c.getX(), c.getY());

                // apply to the cell
                if(em.appliesCells) {
                    var e = new Effect();
                    e.model = em;
                    e.entityid = c.id;
                    step.newEffect(e);
                }
                // apply to the creatures on the cell
                if(em.appliesCreatures) {
                    for(int i = 0; i < em.towerHeightMax; i++) {
                        int height = em.towerFromBottom ? i :  c.creatures.size() - i;
                        if(height >= c.creatures.size() || height < 0)  {
                            break;
                        } else {
                            var e = new Effect();
                            e.model = em;
                            e.entityid = c.creatures.get(height);
                            step.newEffect(e);
                        }
                    }
                }
            }
        }
    }


}
