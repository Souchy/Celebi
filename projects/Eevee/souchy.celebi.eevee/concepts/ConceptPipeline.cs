using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.impl.objects.effectResults;

namespace souchy.celebi.eevee.concepts;

public class ConceptPipeline
{
    public IFight fight;
    public List<EffectPreview> pipe = new List<EffectPreview>();

    public void pushCompiledEffect(EffectPreview e) {
        procTriggers(TriggerOrderType.Before, e);
        pipe.Add(e);
        procTriggers(TriggerOrderType.After, e);
    }

    private void procTriggers(TriggerOrderType orderType, EffectPreview e) {
        foreach(IStatusInstance s in fight.statuses.Values.SelectMany(sc => sc.instances)) {
            // s.holderEntity
            var triggeredEffects = s.checkTriggers(null, null); // orderType, e);
            foreach(var te in triggeredEffects)
                pushCompiledEffect(null);
        }
    }

}
