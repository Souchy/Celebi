using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.impl.objects.effectResults;

namespace souchy.celebi.eevee.concepts;

public class ConceptPipeline
{
    public IFight fight;
    public List<EffectResult> pipe = new List<EffectResult>();

    public void pushCompiledEffect(EffectResult e) {
        procTriggers(TriggerOrderType.Before, e);
        pipe.Add(e);
        procTriggers(TriggerOrderType.After, e);
    }

    private void procTriggers(TriggerOrderType orderType, EffectResult e) {
        foreach(var s in fight.statuses.Values) {
            // s.holderEntity
            var triggeredEffects = s.checkTriggers(orderType, e);
            foreach(var te in triggeredEffects)
                pushCompiledEffect(te);
        }
    }

}
