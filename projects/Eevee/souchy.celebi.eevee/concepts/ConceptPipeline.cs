using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.impl.objects.compiledeffects;

namespace souchy.celebi.eevee.concepts;

public class ConceptPipeline
{
    public IFight fight;
    public List<CompiledEffect> pipe = new List<CompiledEffect>();

    public void pushCompiledEffect(CompiledEffect e) {
        procTriggers(TriggerOrderType.Before, e);
        pipe.Add(e);
        procTriggers(TriggerOrderType.After, e);
    }

    private void procTriggers(TriggerOrderType orderType, CompiledEffect e) {
        foreach(var s in fight.statuses.Values) {
            // s.holderEntity
            var triggeredEffects = s.checkTriggers(orderType, e);
            foreach(var te in triggeredEffects)
                pushCompiledEffect(te);
        }
    }

}
