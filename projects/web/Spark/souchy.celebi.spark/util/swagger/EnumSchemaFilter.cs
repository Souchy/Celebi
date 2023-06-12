using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;

namespace souchy.celebi.spark.util.swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();

                foreach (string enumName in Enum.GetNames(context.Type))
                {
                    MemberInfo? memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);
                    EnumMemberAttribute? enumMemberAttribute = 
                        memberInfo == null
                        ? null
                        : memberInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault();
                    string label =
                        (enumMemberAttribute == null || string.IsNullOrWhiteSpace(enumMemberAttribute.Value))
                        ? enumName
                        : enumMemberAttribute.Value;

                    var value = (int) Enum.Parse(context.Type, enumName);
                    schema.Enum.Add(new OpenApiString($"{label} = {value}"));
                    //schema.Enum.Add(new OpenApiString($"{label} = '{label}'"));
                    schema.Type = "string";
                    schema.Format = string.Empty;
                }
            }
        }
    }
}
