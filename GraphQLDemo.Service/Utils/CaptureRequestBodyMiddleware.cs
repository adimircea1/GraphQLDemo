using System.Text;
using Microsoft.AspNetCore.Http;

namespace GraphQLDemo.Service.Utils;

public class CaptureRequestBodyMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        // Read and capture the request body
        var body = context.Request.Body;

        // This allows the stream to be read multiple time
        context.Request.EnableBuffering();
        
        // Read the stream as string
        var bodyAsString = await new StreamReader(body, Encoding.Default).ReadToEndAsync();

        // Rewind the stream so model binding can still access it
        body.Position = 0;
        
        // Store the raw request body string for later use
        context.Items["RequestBody"] = bodyAsString;

        await next(context);
    }
}