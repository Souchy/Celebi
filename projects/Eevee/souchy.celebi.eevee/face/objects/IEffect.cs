using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.face.objects
{
    public interface IEffect : IEntityModeled, IFightEntity
    {
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }


        public IZone zone { get; set; }
        public List<ITrigger> triggers { get; set; }


        public ICompiledEffect compile(IFight fight, IID source, IID targetCell); // IActionContext context);

    }
}