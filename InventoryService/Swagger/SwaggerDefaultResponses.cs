using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
// ReSharper disable ClassNeverInstantiated.Global

namespace InventoryService.Swagger;

public class SwaggerDefaultResponses : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		string? actionName = context.ApiDescription.ActionDescriptor.DisplayName;
		if(string.IsNullOrEmpty(actionName)) return;
		if (actionName.Contains("GetItem"))
		{
			operation.Responses.TryAdd("404", new OpenApiResponse { Description = "Item not found" });
		} else if (actionName.Contains("CreateItem"))
		{
			operation.Responses.TryAdd("409", new OpenApiResponse { Description = "Item cannot be added" });
		}
	}
}