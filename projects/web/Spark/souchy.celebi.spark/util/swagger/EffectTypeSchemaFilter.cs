using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.enums.characteristics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using souchy.celebi.eevee.enums.effects;

namespace souchy.celebi.spark.util.swagger;

/// <summary>
/// Handles an enum that is defined as a Class with static readonly instances
/// </summary>
public class EffectTypeSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsClass)
        {
            if (!context.Type.IsAssignableTo(typeof(EffectType)))
            {
                return;
            }
            var members = context.Type.GetFields()
                .Where(m => m.FieldType == context.Type)
                .Where(m => m != null);
            var sc = new OpenApiSchema();
            foreach (var memberInfo in members)
            {
                var characType = memberInfo.GetValue(null);

                var json = JsonConvert.SerializeObject(characType, Formatting.Indented, new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                });

                sc.Enum.Add(new OpenApiString($"{memberInfo.Name}: {context.Type.Name} = {json};")); 
            }
            sc.Type = "string";
            sc.Format = string.Empty;
            context.SchemaRepository.AddDefinition(context.Type.Name + "Types", sc);
        }
    }

}
