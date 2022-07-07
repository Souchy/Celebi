package espeon.game.controllers.actionpipeline;

import java.util.ArrayList;
import java.util.List;

import espeon.game.controllers.Context;
import espeon.game.red.Effect;

/**
 * A node is either a group of nodes or a node that applies an effect
 */
public abstract class Node {

    public static NodeGroup newRoot() {
        return new NodeGroup();
    }

    // Context for the node
    public Context context;
    // source creature id
    public int sourceid;
    // target cell id for processing
    // public int cellid;
    // Node's parent
    public NodeGroup parent = null;

    public NodeGroup asGroup() {
        return null;
    }
    public NodeEffect asEffect() { 
        return null;
    }
    public boolean isGroup() {
        return false;
    }
    
    /**
     * Group of nodes
     */
    public static class NodeGroup extends Node {
        /**
         * Node's children
         */
        public List<Node> children = new ArrayList<>();
        private NodeGroup() {}

        public NodeGroup newGroup() {
            NodeGroup n = new NodeGroup();
            n.context = context; //.copy();
            n.sourceid = this.sourceid;
            n.parent = this;
            return n;
        }
        public NodeEffect newEffect(Effect e) {
            NodeEffect n = new NodeEffect();
            n.context = context;
            n.sourceid = this.sourceid;
            n.parent = this;
            n.effect = e;
            return n;
        }
        public boolean isGroup() {
            return true;
        }
        public NodeGroup asGroup() {
            return this;
        }
    }
    
    /**
     * Effect node
     */
    public static class NodeEffect extends Node {
        /**
         * Node's effect
         */
        public Effect effect;
        private NodeEffect() {}
        /**
         * Add brothers to this node's parent, right after it
         */
        public void addBrothers(Node... nodes) {
            // start adding after this node, so this index + 1
            int start = this.parent.children.indexOf(this) + 1;
            // add all the brothers in order
            for(int i = 0; i < nodes.length; i++) {
                this.parent.children.add(start + i, nodes[i]);
            }
        }
        public NodeEffect asEffect() { 
            return this;
        }
    }


}
