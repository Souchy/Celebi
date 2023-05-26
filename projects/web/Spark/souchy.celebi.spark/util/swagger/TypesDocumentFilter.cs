using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.neweffects.face;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using souchy.celebi.eevee.neweffects.impl;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.util.swagger
{
    public class TypesDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //var allTypes = typeof(IEntity).Assembly.GetTypes();
            //foreach(var t in allTypes)
            //{
            //    try
            //    {
            //        context.SchemaGenerator.GenerateSchema(t, context.SchemaRepository);
            //    } catch(Exception e)
            //    {
            //        Console.WriteLine(e);
            //    }
            //}

            context.SchemaGenerator.GenerateSchema(typeof(ITrigger), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IVector3), context.SchemaRepository);
            //context.SchemaGenerator.GenerateSchema(typeof(IStatSimple), context.SchemaRepository);
            //context.SchemaGenerator.GenerateSchema(typeof(IStatBool), context.SchemaRepository);
            //context.SchemaGenerator.GenerateSchema(typeof(IStatList), context.SchemaRepository);
            //context.SchemaGenerator.GenerateSchema(typeof(IEntityStatDictionary), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(EffT), context.SchemaRepository);
            //context.SchemaGenerator.GenerateSchema(typeof(EffectType), context.SchemaRepository);
            //context.SchemaGenerator.GenerateSchema(typeof(EffectTypeCreature), context.SchemaRepository);
            //context.SchemaGenerator.GenerateSchema(typeof(EffectTypeMove), context.SchemaRepository);

            var someTypes = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(IStats))
                         || t.IsAssignableTo(typeof(IStat))
                         || t.IsAssignableTo(typeof(ICondition))
                         || t.IsAssignableTo(typeof(IEffect))
                         || t.IsAssignableTo(typeof(IEffectSchema))
                         || t.IsAssignableTo(typeof(IEffectScript))
                         || t.IsAssignableTo(typeof(IID))
                );
            foreach (var t in someTypes)
            {
                context.SchemaGenerator.GenerateSchema(t, context.SchemaRepository);
            }
            

            // Effect Schemas
            /*
            var schemas = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(IEffectSchema)));
            var schemasSchema = new OpenApiSchema();
            foreach (var schema in schemas)
            {
                var schemaschema = context.SchemaGenerator.GenerateSchema(schema, context.SchemaRepository);
                var camelName = schema.Name.Substring(0, 1).ToLower() + schema.Name.Substring(1);
                schemasSchema.Enum.Add(new OpenApiString($"{camelName}: {schema.Name} = new {schema.Name}();")); // {context.Type.Name} 
            }
            //schemasSchema.Format = "enum";
            schemasSchema.Type = "string";
            schemasSchema.Format = string.Empty;
            //context.SchemaRepository.AddDefinition("EffectSchemaTypes", schemasSchema);
            */

            /*
            // Effect Scripts
            var scripts = GetAllTypesImplementingOpenGenericType(typeof(IEffectScript), typeof(IEntity).Assembly);
            foreach (var script in scripts)
            {
                //throw new Exception("Hey this type is funny: " + script.FullName);
                context.SchemaGenerator.GenerateSchema(script, context.SchemaRepository);
            }
            */
            // TODO: Check that every CharacType id is unique

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
