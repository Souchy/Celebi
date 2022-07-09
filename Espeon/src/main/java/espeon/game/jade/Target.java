package espeon.game.jade;

public class Target {
    
    Position pos;
    TargetTypeFilter filter;
        
    public static enum TargetType {
        /** any meaning unfiltered */
        // nothing(0),
        emptyCell,
        ally,
        enemy,
        corpse,
        summon,
        summon_static,
        all(true);
        public static final String allStr = "all";
        public static final int all;
        static {
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
            return this.ordinal() == TargetType.values().length - 1;
        //     // if(ordinal() == 0)
        //     //     value = 0;
        //     // else
        //     //     this.value = (int) Math.pow(2, ordinal());
        //     for(int i = 0; i < this.ordinal(); i++) {
        //         if(i == 0)
        //             v += 0;
        //         else
        //             v += Math.pow(2, i);
        //     }
        //     this.value = v;
        // }
        // private boolean isLast() {
        //     return this.ordinal() == TargetType.values().length - 1;
        // }
        public boolean isIn(int filter) {
        }
        }
        public TargetTypeFilter toFilter() {
            return new TargetTypeFilter(this);
        }
    }


    public static class TargetTypeFilter {
        public int value = TargetType.all.value;

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
        public boolean isNothing() {
            return value == TargetType.nothing.value;
        }
        public boolean isAll() {
            return value == TargetType.all.value;
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
