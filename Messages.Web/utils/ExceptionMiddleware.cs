using Messages.Bll.Exceptions;
using System.Net;

namespace Messages.Web.Utils;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            httpContext.Response.ContentType = "application/json";
            await _next(httpContext);
        }
        catch (UserAlreadyExistsException ex)
        {
            await HandleItemAlreadyExistsExceptionAsync(httpContext, ex);
        }
        catch (EntityNotFoundException ex)
        {
            await HandleEntityNotFoundExceptionAsync(httpContext, ex);
        }
        catch (FailedToCreateException ex)
        {
            await HandleFailedToCreateExceptionAsync(httpContext, ex);
        }
        catch (UnavailableServiceException ex)
        {
            await HandleUnavailableServiceExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }

    }
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await httpContext.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = "Internal Server Error from the custom middleware."
        }.ToString());
    }

    private async Task HandleEntityNotFoundExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await WriteErrorAsync(httpContext, exception.Message);
    }
    private async Task HandleFailedToCreateExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await WriteErrorAsync(httpContext, exception.Message);
    }

    private async Task HandleItemAlreadyExistsExceptionAsync(HttpContext httpContext, Exception exception)
    { 
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await WriteErrorAsync(httpContext, exception.Message);
    }

    private async Task HandleUnavailableServiceExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
        await WriteErrorAsync(httpContext, exception.Message);
    }

    private async Task WriteErrorAsync(HttpContext httpContext, string message)
    {
        await httpContext.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = message
        }.ToString());
    }
}
