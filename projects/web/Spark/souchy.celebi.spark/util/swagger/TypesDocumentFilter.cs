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

namespace souchy.celebi.spark.util.swagger
{
    public class TypesDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(ITrigger), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IStatSimple), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IStatBool), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IStatList), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IEntityStatDictionary), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(IVector3), context.SchemaRepository);

            var effectClasses = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(IEffect)));
            foreach (var effectClass in effectClasses)
            {
                context.SchemaGenerator.GenerateSchema(effectClass, context.SchemaRepository);
            }
            

            context.SchemaGenerator.GenerateSchema(typeof(EffectType), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(EffectTypeCreature), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(EffectTypeMove), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(EffT), context.SchemaRepository);

            // Effect Schemas

            var schemas = typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .Where(t => t.IsAssignableTo(typeof(IEffectSchema)));
            var schemasSchema = new OpenApiSchema();
            foreach (var schema in schemas)
            {
                //var json = JsonConvert.SerializeObject(Activator.CreateInstance(schema), Formatting.Indented, new JsonSerializerSettings()
                //{
                //    ContractResolver = new DefaultContractResolver()
                //    {
                //        NamingStrategy = new CamelCaseNamingStrategy()
                //    }
                //});

                var schemaschema = context.SchemaGenerator.GenerateSchema(schema, context.SchemaRepository);
                //schemaschema.Format = "class";
                //schemaschema.Type = "class";
                
                //context.SchemaRepository.AddDefinition(schema.Name, schemaschema);

                //var schemaRef = new OpenApiReference()
                //{
                //    Type = ReferenceType.Schema,
                //    Id = schema.Name
                //};
                var camelName = schema.Name.Substring(0, 1).ToLower() + schema.Name.Substring(1);
                schemasSchema.Enum.Add(new OpenApiString($"{camelName}: {schema.Name} = new {schema.Name}();")); // {context.Type.Name} 
                //schemasSchema.Properties.Add(schema.Name, new OpenApiSchema()
                //{
                //    Reference = schemaRef
                //});
            }
            //schemasSchema.Format = "enum";
            schemasSchema.Type = "string";
            schemasSchema.Format = string.Empty;
            //context.SchemaRepository.AddDefinition("EffectSchemaTypes", schemasSchema);

            // Effect Scripts
            var scripts = GetAllTypesImplementingOpenGenericType(typeof(IEffectScript), typeof(IEntity).Assembly);
            foreach (var script in scripts)
            {
                //throw new Exception("Hey this type is funny: " + script.FullName);
                context.SchemaGenerator.GenerateSchema(script, context.SchemaRepository);
            }
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
