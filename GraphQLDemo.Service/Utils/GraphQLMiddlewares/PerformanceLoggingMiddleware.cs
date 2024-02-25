using System.Diagnostics;
using GraphQL;
using GraphQL.Instrumentation;

namespace GraphQLDemo.Service.Utils.GraphQLMiddlewares;

public class PerformanceLoggingMiddleware : IFieldMiddleware
{
    public async ValueTask<object?> ResolveAsync(IResolveFieldContext context, FieldMiddlewareDelegate next)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await next(context);
        stopwatch.Stop();
        Console.WriteLine($"{context.FieldDefinition.Name} took {stopwatch.ElapsedMilliseconds} ms to finish!");
        return result;
    }
}