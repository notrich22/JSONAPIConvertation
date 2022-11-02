using JSONConvertationAPI;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (context) => {
    app.Logger.LogInformation($"Path: /  Time: {DateTime.Now.ToLongTimeString()}");
    Messages.StatusMessage responseData =
        new Messages.StatusMessage("Service is avaliable", DateTime.Now);
    Console.WriteLine(responseData);
    await context.Response.WriteAsJsonAsync(responseData);
});
app.MapGet("/ping", async (context) =>
{
    app.Logger.LogInformation($"Path: /ping  Time: {DateTime.Now.ToLongTimeString()}");
    Messages.StatusMessage status = new Messages.StatusMessage("pong", DateTime.Now);
    Console.WriteLine(status);
    await context.Response.WriteAsJsonAsync(status);
});
app.MapPost("/calculate", async (context) =>
{
    app.Logger.LogInformation($"Path: /calculate  Time: {DateTime.Now.ToLongTimeString()}");
    Messages.CalcInputMessage? convertation = null;
    try
    {
        convertation = await context.Request.ReadFromJsonAsync<Messages.CalcInputMessage>();
    }
    catch(Exception e) {
        app.Logger.LogError($"Exception occured: {e.Message}: DateTime:{DateTime.Now.ToLongTimeString()}");
        Messages.ErrorMessage error = new Messages.ErrorMessage($"Error during request processing: {e.Message}");
    }
    if (convertation is null)
    {
        app.Logger.LogError($"Exception occured: Convertation is null: DateTime:{DateTime.Now.ToLongTimeString()}");
        Messages.ErrorMessage error = new Messages.ErrorMessage("Invalid request parameters");
        Console.WriteLine(error);
        await context.Response.WriteAsJsonAsync(error);
    }
    else
    {
        try
        {
            Messages.CalcOutputMessage solvation = NSSolvator.Solve(convertation);
            Console.WriteLine(solvation);
            await context.Response.WriteAsJsonAsync(solvation);
        }
        catch (Exception e)
        {
            app.Logger.LogError($"Exception occured: {e.Message}: DateTime:{DateTime.Now.ToLongTimeString()}");
            Messages.ErrorMessage error = new Messages.ErrorMessage($"Error during request processing: {e.Message}");
            Console.WriteLine(error);
            await context.Response.WriteAsJsonAsync(error);
        }
    }
});
app.Run();
