using InventoryService.Databases;
using InventoryService.Helpers;
using InventoryService.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace InventoryService;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.AddDbContext<InventoryDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(sg =>
		{
			sg.SwaggerDoc(HelperVariables.SwaggerVersion, new OpenApiInfo{Title = "InventoryApp", Version = HelperVariables.SwaggerVersion});
			sg.OperationFilter<SwaggerDefaultResponses>();
		});

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}