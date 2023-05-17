using Microsoft.OpenApi.Models;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.impl.objects.effects.res;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace souchy.celebi.spark.util.swagger
{
    public class TypesDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(ITrigger), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IStatSimple), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IStatBool), context.SchemaRepository);

            var effectClasses = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(IEffect)));
            foreach (var effectClass in effectClasses)
            {
                context.SchemaGenerator.GenerateSchema(effectClass, context.SchemaRepository);
            }


            // Effect Schemas
            var schemas = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(IEffectSchema)));
            foreach (var schema in schemas)
            {
                context.SchemaGenerator.GenerateSchema(schema, context.SchemaRepository);
            }
            // Effect Scripts
            var scripts = GetAllTypesImplementingOpenGenericType(typeof(IEffectScript), typeof(IEntity).Assembly);
            foreach (var script in scripts)
            {
                //throw new Exception("Hey this type is funny: " + script.FullName);
                context.SchemaGenerator.GenerateSchema(script, context.SchemaRepository);
            }
        }

        public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType, Assembly assembly)
        {
            return from x in assembly.GetTypes()
                   from z in x.GetInterfaces()
                   let y = x.BaseType
                   where
                   (y != null && y.IsGenericType &&
                   openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
                   (z.IsGenericType &&
                   openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
                   select x;
        }

    }
}
