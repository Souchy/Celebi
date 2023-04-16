namespace souchy.celebi.eevee.enums
{
    public enum ConditionComparatorType
    {
        EQ, // equal
        NE, // not equal
        GT, // greater than
        GE, // greater or equal
        LT, // lesser than
        LE // lesser or equal
    }

    public static class ConditionComparatorTypeExtentions {
        public static bool check(this ConditionComparatorType comparator, object fetchedValue, object wantedValue) {
            if(fetchedValue == null) return false;
            // can compare EQ, NE between IID, strings, numbers...
            if(comparator == ConditionComparatorType.EQ) {
                return fetchedValue == wantedValue;
            }
            if(comparator == ConditionComparatorType.NE) {
                return fetchedValue != wantedValue;
            }
            // only numbers can be used with GT, GE, LT, LE
            double fetchedInt = (double) fetchedValue;
            double wantedInt = (double) wantedValue;
            switch(comparator) {
                case ConditionComparatorType.GT:
                    return fetchedInt > wantedInt;
                case ConditionComparatorType.GE:
                    return fetchedInt >= wantedInt;
                case ConditionComparatorType.LT:
                    return fetchedInt < wantedInt;
                case ConditionComparatorType.LE:
                    return fetchedInt <= wantedInt;
            }
            return false;
        }
    }
}
