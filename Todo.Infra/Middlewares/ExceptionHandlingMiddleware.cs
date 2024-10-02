using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Exceptions;

namespace Todo.Infra.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try{
                await next(context);
            }
            catch(Exception ex){
                // Default error
                var problemDetails = new ProblemDetails 
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error"
                };

                if(ex is NotFoundException){
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = ex.Message;
                }

                if(ex is BadRequestException){
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = ex.Message;
                }

                if(ex is UnauthorizedException){
                    problemDetails.Status = StatusCodes.Status403Forbidden;
                    problemDetails.Title = ex.Message;
                }

                if(ex is ConflictException){
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Title = ex.Message;
                }

                context.Response.StatusCode = (int) problemDetails.Status;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }

        }
    }
}