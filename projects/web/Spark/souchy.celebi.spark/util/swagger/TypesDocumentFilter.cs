using Microsoft.OpenApi.Models;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.triggers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace souchy.celebi.spark.util.swagger
{
    public class TypesDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(ITrigger), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IStatSimple), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IStatBool), context.SchemaRepository);
        }
    }
}
