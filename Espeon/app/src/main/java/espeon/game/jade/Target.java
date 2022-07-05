package espeon.game.jade;

public class Target {
    
    Position pos;
    TargetTypeFilter filter;
        
    public static enum TargetType {
        /** any meaning unfiltered */
        any,
        emptyCell,
        ally,
        enemy,
        corpse,
        summon,
        summon_static,
        all(true);
        //ALL = emptyCell | ally | enemy | summon | summon_static | corpse,
        public final int value;
        private TargetType() {
            if(ordinal() == 0)
                value = 0;
            else
                this.value = (int) Math.pow(2, ordinal());
        }
        private TargetType(boolean isLast) {
            int v = 0;
            for(int i = 0; i < this.ordinal(); i++) {
                if(i == 0)
                    v += 0;
                else
                    v += Math.pow(2, i);
            }
            this.value = v;
        }
        private boolean isLast() {
            return this.ordinal() == TargetType.values().length - 1;
        }
        public TargetTypeFilter toFilter() {
            return new TargetTypeFilter(this);
        }
    }

    public static class TargetTypeFilter {
        public int value = TargetType.any.value;

        public TargetTypeFilter() { }
        public TargetTypeFilter(TargetType v) {
            value = v.value;
        }
        public TargetTypeFilter(int v) {
            value = v;
        }
        public boolean includes(TargetTypeFilter filter) {
            return (value & filter.value) != 0 || equals(filter);
        }
        public boolean equals(TargetTypeFilter filter) {
            return value == filter.value;
        }
        public boolean isAny() {
            return value == TargetType.any.value;
        }

        /**
         * @return A new filter 
         */
        public TargetTypeFilter sub(TargetTypeFilter filter) {
            return new TargetTypeFilter(value - filter.value);
        }
        /**
         * @return A new filter 
         */
        public TargetTypeFilter add(TargetTypeFilter filter) {
            return new TargetTypeFilter(value + filter.value);
        }
        
        // TargetTypeFilter operator-(const TargetTypeFilter other) {
        //     value = value & ~other.value;
        //     return value;
        // }
        // TargetTypeFilter operator+(const TargetTypeFilter other) {
        //     value = value | other.value;
        //     return value;
        // }
    };

}
