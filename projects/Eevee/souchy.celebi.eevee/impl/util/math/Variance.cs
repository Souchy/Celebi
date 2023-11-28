using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.schemas;

namespace souchy.celebi.eevee.impl.util.math
{
    /// <summary>
    /// This cannot work. We need variance only for "OnApplyStatus"-effects that actually consume it like AddStats
    /// </summary>
    public static class Variance
    {
        public static void consumeVariance(IEffectSchema schema)
        {
            if (schema == null) throw new ArgumentNullException();

            if (schema is AddStats add)
                consumeVarianceStats(add);
            if (schema is AddStatsPercent addPercent)
                consumeVarianceStats(addPercent);

            //if (schema is AbstractDamageSchema dmg)
            //    consumeVarianceDamage(dmg);

            //if (schema is Heal heal)
            //    consumeVarianceHeal(heal);
        }

        public static void consumeVarianceStats(AddStats schema)
        {
            foreach(var stat in schema.stats.Values)
            {
                if(stat is IStatSimple statSimple)
                {
                    int variance = (int) (statSimple.value * schema.percentVariance / 100.0);
                    statSimple.value = new Random().Next(statSimple.value - variance, statSimple.value + variance);
                }
                // I dont think we can/should apply variance to booleans.
                // If you want that, do a RandomEffect { 50%: addStats(true), 50%: addStats(false) }
            }
            schema.percentVariance = 0;
        }
        public static void consumeVarianceStats(AddStatsPercent schema)
        {
            foreach (var stat in schema.statsPercent.Values)
            {
                if (stat is IStatSimple statSimple)
                {
                    int variance = (int) (statSimple.value * schema.percentVariance / 100.0);
                    statSimple.value = new Random().Next(statSimple.value - variance, statSimple.value + variance);
                }
            }
            schema.percentVariance = 0;
        }



        public static void consumeVarianceDamage(AbstractDamageSchema schema)
        {
            int variance = (int) (schema.baseDamage * schema.percentVariance / 100.0);
            schema.baseDamage = new Random().Next(schema.baseDamage - variance, schema.baseDamage + variance);
            schema.percentVariance = 0;
        }


        public static void consumeVarianceHeal(Heal schema)
        {
            int variance = (int) (schema.baseHeal * schema.percentVariance / 100.0);
            schema.baseHeal = new Random().Next(schema.baseHeal - variance, schema.baseHeal + variance);
            schema.percentVariance = 0;
        }

    }
}
