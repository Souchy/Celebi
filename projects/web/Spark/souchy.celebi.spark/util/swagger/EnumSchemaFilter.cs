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
            if (!context.Type.IsEnum) return;

            schema.Enum.Clear();

            foreach (string enumName in Enum.GetNames(context.Type))
            {
                MemberInfo? memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);
                EnumMemberAttribute? enumMemberAttribute = null;
                if (memberInfo != null)
                {
                    enumMemberAttribute = memberInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault();
                }
                string label = null;
                if (enumMemberAttribute != null)
                {
                    label = enumMemberAttribute.Value;
                }

                schema.Enum.Add(new OpenApiString(label));
                schema.Type = "string";
                //schema.Format = "string";
            }

        }
    }
}
