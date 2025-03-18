using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Codyssi.Controllers
{
    /// <summary>
    /// Configures parameter filters
    /// </summary>
    public class ParameterFilter : IParameterFilter
    {
        /// <summary>
        /// Applies parameter filters to the API calls in Swagger
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            // Ensure that the input day is a valid value (1 - 17)
            if (parameter.Name.Equals("day", StringComparison.InvariantCultureIgnoreCase))
            {
                List<int> days = Enumerable.Range(1, Globals.NUMBER_OF_PUZZLES).ToList();
                parameter.Schema.Enum = days.Select(d => new OpenApiString(d.ToString())).ToList<IOpenApiAny>();
            }

            // Ensure that the input part is a valid value (1 - 3)
            if (parameter.Name.Equals("part", StringComparison.InvariantCultureIgnoreCase))
            {
                List<int> parts = Enumerable.Range(1, 3).ToList();
                parameter.Schema.Enum = parts.Select(d => new OpenApiString(d.ToString())).ToList<IOpenApiAny>();
            }
        }
    }
}