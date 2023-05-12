using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;

namespace souchy.celebi.spark.util.swagger
{
    public class AdditionalPropertiesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!(
                context.Type.IsAssignableTo(typeof(ICondition)) ||
                context.Type.IsAssignableTo(typeof(IEffect))
                ))
                return;

            schema.AdditionalPropertiesAllowed = true;
            schema.AdditionalProperties = new OpenApiSchema()
            {
                Type = "object"
            };
        }
    }
}
