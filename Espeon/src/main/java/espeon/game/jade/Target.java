package espeon.game.jade;

public class Target {
    
    Position pos;
    TargetTypeFilter filter;
        
    public static enum TargetType {
        /** any meaning unfiltered */
        // nothing(0),
        // nothing,
        emptyCell,
        ally,
        enemy,
        corpse,
        summon,
        summon_static,
        // all(true);
        // all
        ;
        public static final String nothingStr = "nothing";
        public static final int nothing = 0;
        public static final String allStr = "all";
        public static final int all;
        static {
            int asdf = 0;
            for(var t : values())
                asdf |= t.value;
            all = asdf;
        }
        //ALL = emptyCell | ally | enemy | summon | summon_static | corpse,
        public final int value = (int) Math.pow(2, ordinal() + 1);

        // private TargetType(int forced) {
        //     value = forced;
        // }
        // private TargetType() {
        //     // if(ordinal() == 0)
        //     //     value = 0;
        //     // else
        //     //     this.value = (int) Math.pow(2, ordinal());
        //     this.value = (int) Math.pow(2, ordinal());
        // }
        // private TargetType(boolean isLast) {
        //     int v = 0;
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
            return (filter & this.value) == this.value;
        }
        public static boolean isNothing(int filter) {
            return nothing == filter;
        }
        public static boolean isAll(int filter) {
            return all == filter;
        }
        public TargetTypeFilter toFilter() {
            return new TargetTypeFilter(this);
        }
    }


    public static class TargetTypeFilter {
        public int value = TargetType.all;

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
            return value == TargetType.nothing;
        }
        public boolean isAll() {
            return value == TargetType.all;
        }

        /**
         * @return A new filter 
         */
        public TargetTypeFilter add(TargetTypeFilter filter) {
            return new TargetTypeFilter(value | filter.value);
        }
        /**
         * @return A new filter 
         */
        public TargetTypeFilter sub(TargetTypeFilter filter) {
            return new TargetTypeFilter(value & ~filter.value);
        }
        public TargetTypeFilter add(TargetType type) {
            value |= type.value;
            return this;
        }
        public TargetTypeFilter sub(TargetType type) {
            value &= ~type.value;
            return this;
        }
        
        public static int add(int filter1, int filter2) {
            return filter1 | filter2;
        }
        public static int sub(int filter1, int filter2) {
            return filter1 & ~filter2;
        }
    };

}
