using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Helpers;

public class InventoryExceptionHandler
{
	private readonly RequestDelegate _next;
	private readonly ILogger<InventoryExceptionHandler> _logger;

	public InventoryExceptionHandler(RequestDelegate next, ILogger<InventoryExceptionHandler> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task Invoke(HttpContext context)
	{
		Exception? failedExecution = null;
		try
		{
			await _next(context);
		}
		catch (DbUpdateException ex)
		{
			failedExecution = ex.InnerException;
			context.Response.StatusCode = StatusCodes.Status409Conflict;
		}
		catch (KeyNotFoundException ex)
		{
			failedExecution = ex;
			context.Response.StatusCode = StatusCodes.Status404NotFound;
		}
		catch (ArgumentNullException ex)
		{
			failedExecution = ex;
			context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
		}
		catch (InvalidOperationException ex)
		{
			failedExecution = ex;
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
		}
		catch (SqlException ex)
		{
			failedExecution = ex;
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
		}
		catch (TimeoutException ex)
		{
			failedExecution = ex;
			context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
		}
		catch (Exception ex)
		{
			failedExecution = ex;
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		}
		finally
		{
			if (failedExecution != null)
			{
				_logger.LogWarning("Exception: {failedExecution} from : {context.Request.Method}", failedExecution, context.Request.Path);
				await context.Response.WriteAsJsonAsync(new { message = failedExecution.Message });
			}
		}
	}
}