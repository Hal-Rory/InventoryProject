using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
// ReSharper disable ClassNeverInstantiated.Global

namespace InventoryProject.Swagger;

public class SwaggerDefaultResponses : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		string? actionName = context.ApiDescription.ActionDescriptor.DisplayName;
		if(string.IsNullOrEmpty(actionName)) return;
		if (actionName.Contains("Get"))
		{
			if (!actionName.Contains("All"))
			{
				operation.Responses.TryAdd("404",
					new OpenApiResponse
						{ Description = $"{(actionName.Contains("Item") ? "Item" : "Player")} not found" });
			}
			else
			{
				operation.Responses.TryAdd("400",
					new OpenApiResponse
						{ Description = $"{(actionName.Contains("Item") ? "Items" : "Players")} not present" });
			}
		}
		else if (actionName.Contains("Create"))
		{
			operation.Responses.TryAdd("409", new OpenApiResponse { Description = $"{(actionName.Contains("Item") ? "Item(s)" : "Player(s)")} cannot be added" });
		}
	}
}