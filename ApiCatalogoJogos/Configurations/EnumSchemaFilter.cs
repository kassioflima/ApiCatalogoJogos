using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiCatalogoJogos.Configurations
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext filterContext)
        {
            if(filterContext.Type.IsEnum)
            {
                var array = new OpenApiArray();
                array.AddRange(Enum.GetNames(filterContext.Type).Select(x => new OpenApiString(x)));
                schema.Extensions.Add("x-enumNames", array);
                schema.Extensions.Add("x-enum-varnames", array);
            }
        }
    }
}
