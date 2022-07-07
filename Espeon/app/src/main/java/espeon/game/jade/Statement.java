package espeon.game.jade;

import java.util.List;

public interface Statement {

    public boolean isGroup();
    public StatementGroup asGroup();
    public EffectModel asEffect();

    public static class StatementGroup implements Statement {
        public Condition condition;
        public List<Statement> children;
        public List<Statement> childrenOtherwise;
        public boolean isGroup() {
            return true;
        }
        @Override
        public StatementGroup asGroup() {
            return this;
        }
        @Override
        public EffectModel asEffect() {
            return null;
        }
    }

    public static class StatementEffect implements Statement {
        public EffectModel effect;
        public boolean isGroup() {
            return false;
        }
        @Override
        public StatementGroup asGroup() {
            return null;
        }
        @Override
        public EffectModel asEffect() {
            return effect;
        }
    }
    
}
