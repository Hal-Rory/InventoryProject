using InventoryProject.Databases;
using InventoryProject.Helpers;
using InventoryProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace InventoryProject;

public static class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.AddDbContext<InventoryDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString(HelperVariables.ConnectionString)));

		builder.Services.AddScoped<PlayerService>();
		builder.Services.AddScoped<ItemService>();
		builder.Services.AddScoped<InventoryService>();

		builder.Services.AddControllers();

		builder.Services.AddSwaggerGen(sg =>
		{
			sg.SwaggerDoc(HelperVariables.SwaggerVersion, new OpenApiInfo{Title = "InventoryApp", Version = HelperVariables.SwaggerVersion});
		});

		WebApplication app = builder.Build();

		app.UseMiddleware<InventoryExceptionHandler>();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint($"/swagger/{HelperVariables.SwaggerVersion}/swagger.json", "InventoryApp");
			});
		}
		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.MapControllers();
		app.Run();
	}
}