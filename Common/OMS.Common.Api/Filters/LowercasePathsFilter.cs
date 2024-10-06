using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OMS.Common.Api.Filters
{
    public class LowercasePathsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var newPaths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                var lowercasePath = path.Key.ToLowerInvariant();
                newPaths.Add(lowercasePath, path.Value);
            }
            swaggerDoc.Paths = newPaths;
        }
    }
}
