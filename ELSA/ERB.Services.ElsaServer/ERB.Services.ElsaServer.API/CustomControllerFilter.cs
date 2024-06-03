using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ERB.Services.ElsaServer.API;
public class CustomDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Replace "YourNamespace" with the actual namespace of your custom controllers
        var customNamespace = "ERB.Services.ElsaServer.API";
        var customAPIRoute = "/api/";

        // Filter paths to include only those from specific namespaces
        var filteredPaths = swaggerDoc.Paths
            .Where(pathItem => context.ApiDescriptions
                .Any(apiDesc => pathItem.Key.StartsWith(customAPIRoute) &&
                                apiDesc.ActionDescriptor.RouteValues["controller"]!.StartsWith(customNamespace)))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        // Replace the paths in the document with the filtered paths
        swaggerDoc.Paths = new OpenApiPaths();
        foreach (var path in filteredPaths)
        {
            swaggerDoc.Paths.Add(path.Key, path.Value);
        }

        // Find schemas used in the filtered paths
        var usedSchemas = filteredPaths.Values
            .SelectMany(pathItem => pathItem.Operations.Values)
            .SelectMany(operation => operation.RequestBody?.Content.Values.Select(c => c.Schema.Reference?.Id) ?? Enumerable.Empty<string>()
                                 .Concat(operation.Responses.Values.SelectMany(response => response.Content.Values.Select(c => c.Schema.Reference?.Id))))
            .Where(schemaId => schemaId != null)
            .Distinct()
            .ToList();

        // Filter schemas to include only those used by your endpoints
        var filteredSchemas = swaggerDoc.Components.Schemas
            .Where(schema => usedSchemas.Contains(schema.Key))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        // Replace the schemas in the document with the filtered schemas
        swaggerDoc.Components.Schemas = new Dictionary<string, OpenApiSchema>();
        foreach (var schema in filteredSchemas)
        {
            swaggerDoc.Components.Schemas.Add(schema.Key, schema.Value);
        }
    }
}
