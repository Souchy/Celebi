using Microsoft.AspNetCore.Mvc;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl;

namespace souchy.celebi.spark.controllers.models
{
    //public record TestRecord(string name, CharacteristicCategory Category, StatFactory Factory) //CharacteristicCategory Category, int LocalId, string BaseName, StatFactory Factory)
    //{

    //}

    [ApiController]
    [Route(Routes.Models + "properties")]
    public class PropertiesController : ControllerBase
    {
        //[HttpGet("charac/characteristicId")]
        //public List<CharacteristicId> GetCharacId() => new List<CharacteristicId>();
        //[HttpGet("charac/characType")]
        //public List<CharacteristicType> GetCharacType() => new List<CharacteristicType>();
        [HttpGet("charac/resource")]
        public List<Resource> GetResource() => Resource.values.Values.ToList();
        [HttpGet("charac/affinity")]
        public List<Affinity> GetAffinity() => Affinity.values.Values.ToList();
        [HttpGet("charac/resistance")]
        public List<Resistance> GetResistance() => Resistance.values.Values.ToList();
        [HttpGet("charac/contextual")]
        public List<Contextual> GetContextual() => Contextual.values.Values.ToList();
        [HttpGet("charac/other")]
        public List<OtherProperty> GetOther() => OtherProperty.values.Values.ToList();
        [HttpGet("charac/spellmodel")]
        public List<SpellModelProperty> GetSpellModelProperty() => SpellModelProperty.values.Values.ToList();
        [HttpGet("charac/spell")]
        public List<SpellProperty> GetSpellProperty() => SpellProperty.values.Values.ToList();
        [HttpGet("charac/state")]
        public List<State> GetState() => State.values.Values.ToList();
        [HttpGet("charac/statuscontainer")]
        public List<StatusContainerProperty> GetStatusContainerProperty() => StatusContainerProperty.values.Values.ToList();
        [HttpGet("charac/statusinstance")]
        public List<StatusInstanceProperty> GetStatusInstanceProperty() => StatusInstanceProperty.values.Values.ToList();

        [HttpGet("effects/schemas")]
        public ActionResult<IEnumerable<SchemaDescription>> GetEffectSchemas()
        {
            var schemas = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(IEffectSchema)));
            var descriptions = schemas.Select(t => SchemaDescription.GetSchemaDescription(t));
            return Ok(descriptions);
        }
        [HttpGet("effects/schema/{name}")]
        public ActionResult<SchemaDescription> GetEffectSchema(string name)
        {
            var type = typeof(IEntity).Assembly
                .GetTypes().FirstOrDefault(t => t.Name == name);
            if (type == null) return NotFound();
            return Ok(SchemaDescription.GetSchemaDescription(type));
        }

    }

    public record SchemaDescription(string name, Dictionary<string, string> properties)
    {
        public static SchemaDescription GetSchemaDescription(Type schemaType)
        {
            var dic = new Dictionary<string, string>();
            foreach (var p in schemaType.GetProperties())
            {
                var name = p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1);
                dic.Add(name, p.PropertyType.Name);
            }
            return new SchemaDescription(schemaType.Name, dic);
        }
    }

}
