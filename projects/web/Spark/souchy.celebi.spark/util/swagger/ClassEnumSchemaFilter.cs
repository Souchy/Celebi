using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.spark.util.swagger;

/// <summary>
/// Handles an enum that is defined as a Class with static readonly instances
/// </summary>
public class ClassEnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsClass)
        {
            var dsaa = context.Type.GetCustomAttributes(typeof(ClassEnumAttribute), false)
                .OfType<ClassEnumAttribute>().FirstOrDefault();

            if (dsaa == null)
            {
                return;
            }
            //schema.AdditionalPropertiesAllowed = true;
            var sc = new OpenApiSchema();
            //schema.AdditionalProperties = sc;
            //sc.Enum
            //schema.AdditionalProperties.Properties.Add();
            //return;

            var members = context.Type.GetFields()
                .Where(m => m.FieldType == context.Type)
                .Where(m => m != null);

            //schema.Enum.Clear();

            //string enumName in Enum.GetNames(context.Type))
            foreach (var memberInfo in members)
            {
                //MemberInfo? memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);
                EnumMemberAttribute? enumMemberAttribute =
                        memberInfo == null
                        ? null
                        : memberInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault();
                string label =
                    (enumMemberAttribute == null || string.IsNullOrWhiteSpace(enumMemberAttribute.Value))
                    ? memberInfo.Name //enumName
                    : enumMemberAttribute.Value;

                var characType = memberInfo.GetValue(null);
                var characId = characType.GetType().GetProperty("ID").GetValue(characType);
                //var prop = new OpenApiSchema();
                //prop.Properties.Add("", new OpenApiString(""));

                //sc.Title = context.Type.Name + "Type";
                sc.Enum.Add(new OpenApiString(memberInfo.Name + ": " + characId.ToString())); // label));
                //sc.Properties.Add("fire", prop);
            }
            sc.Type = "string";
            sc.Format = string.Empty;
            context.SchemaRepository.AddDefinition(context.Type.Name + "Type", sc);
        }
    }

}
