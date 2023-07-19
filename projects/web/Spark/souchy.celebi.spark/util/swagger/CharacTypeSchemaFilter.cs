﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.enums.characteristics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using souchy.celebi.eevee.neweffects.impl;
using souchy.celebi.eevee.face.shared.conditions;

namespace souchy.celebi.spark.util.swagger;

/// <summary>
/// Handles an enum that is defined as a Class with static readonly instances
/// </summary>
public class CharacTypeSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsClass)
        {
            
            if (!context.Type.IsAssignableTo(typeof(CharacteristicType))
                && !context.Type.IsAssignableTo(typeof(ConditionType))
                //&& !context.Type.IsAssignableTo(typeof(EffectType))
            )
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
                    },
                    //Converters = { new EnumToStringConverter() }
                });

                sc.Enum.Add(new OpenApiString($"{memberInfo.Name}: {context.Type.Name} = {json};")); 
            }
            sc.Type = "string";
            sc.Format = string.Empty;
            context.SchemaRepository.AddDefinition(context.Type.Name + "Types", sc);
        }
    }
    public class EnumToStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            //return reader.Value;
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value!.ToString());
        }
    }

}
