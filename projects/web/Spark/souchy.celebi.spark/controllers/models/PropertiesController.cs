using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.values;
using Spark;

namespace souchy.celebi.spark.controllers.models
{
    //public record TestRecord(string name, CharacteristicCategory Category, StatFactory Factory) //CharacteristicCategory Category, int LocalId, string BaseName, StatFactory Factory)
    //{

    //}

    [ApiController]
    [Route(Routes.Models + "properties")]
    public class PropertiesController : ControllerBase
    {
        [HttpGet("charac/characteristicId")]
        public List<CharacteristicId> GetCharacId() => new List<CharacteristicId>();
        [HttpGet("charac/characType")]
        public List<CharacteristicType> GetCharacType() => new List<CharacteristicType>();

        //[HttpGet("charac/testrecord")]
        //public TestRecord TestRecord() => new TestRecord("test", CharacteristicCategory.Resource, null); // CharacteristicCategory.Resource, 1, "test", null);


        //[HttpGet("charac/resource")]
        //public List<Resource> GetResource() => Resource.values.Values.ToList();
        //[HttpGet("charac/affinity")]
        //public List<Affinity> GetAffinity() => Affinity.values.Values.ToList();
        //[HttpGet("charac/resistance")]
        //public List<Resistance> GetResistance() => Resistance.values.Values.ToList();
        //[HttpGet("charac/contextual")]
        //public List<Contextual> GetContextual() => Contextual.values.Values.ToList();
        //[HttpGet("charac/other")]
        //public List<OtherProperty> GetOther() => OtherProperty.values.Values.ToList();
        //[HttpGet("charac/spellmodel")]
        //public List<SpellModelProperty> GetSpellModelProperty() => SpellModelProperty.values.Values.ToList();
        //[HttpGet("charac/spell")]
        //public List<SpellProperty> GetSpellProperty() => SpellProperty.values.Values.ToList();
        //[HttpGet("charac/state")]
        //public List<State> GetState() => State.values.Values.ToList();
        //[HttpGet("charac/statusmodel")]
        //public List<StatusModelProperty> GetStatusModelProperty() => StatusModelProperty.values.Values.ToList();
    }
}
