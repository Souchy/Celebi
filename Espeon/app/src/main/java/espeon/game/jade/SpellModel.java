package espeon.game.jade;

import java.util.List;

import espeon.game.red.Aoe;

public class SpellModel {
    
    public SpellConditions conditions;
    public List<Cost> costs;
    // public Statement root;
    public List<SpellLine> lines;

    // SpellStats status; // for an instance of the spell

    public class SpellConditions {
        public Aoe cellConditions = new Aoe(1,1);
        public int castPerTarget = 0;
        public int castPerTurn = 0;
        public int cooldown = 0;
        public int range = 1;
    };

    public class Cost {
        public Mod resource;
        public int amount;
    };

    /**
     * {
     *      condition: "target.stats.hp>30",
     *      effect: null,
     *      {
     *          condition: null,
     *          effect: "damage10",
     *          children: null
     *      },
     *      {
     *          condition: null,
     *          effect: "push3",
     *          children: null
     *      }
     * }
     */
    
    // public class Statement {
    //     public Condition condition;
    //     public EffectModel effect;
    //     // public List<EffectModel> effects;
    //     public List<Statement> children;
    // };

    public interface SpellLine {
        public boolean isGroup();
        public SpellLineGroup asGroup();
        public SpellLineStatement asStatement();
    }

    public class SpellLineGroup implements SpellLine {
        public Condition condition;
        public List<SpellLine> children;
        public List<SpellLine> childrenOtherwise;
        public boolean isGroup() {
            return true;
        }
        @Override
        public SpellLineGroup asGroup() {
            return this;
        }
        @Override
        public SpellLineStatement asStatement() {
            return null;
        }
    }
    public class SpellLineStatement implements SpellLine {
        public EffectModel effect;
        public boolean isGroup() {
            return false;
        }
        @Override
        public SpellLineGroup asGroup() {
            return null;
        }
        @Override
        public SpellLineStatement asStatement() {
            return this;
        }
    }

}
