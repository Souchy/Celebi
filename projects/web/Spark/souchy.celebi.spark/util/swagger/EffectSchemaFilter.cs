using Microsoft.OpenApi.Models;
using souchy.celebi.eevee.neweffects.face;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace souchy.celebi.spark.util.swagger
{
    public class EffectSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if(context.Type.IsAssignableTo(typeof(IEffectSchema))) {
                //schema.Type = "type";
                //schema.Format = "specialClass";
                //schema.
            }
        }
    }
}
