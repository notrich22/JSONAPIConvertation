using JSONConvertationAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Services.AddTransient<ISolvator, NSSolvator>();
var app = builder.Build();


app.UseMiddleware<TokenMiddleware>();


app.MapGet("/", async (context) => {
    app.Logger.LogInformation($"Path: /  Time: {DateTime.Now.ToLongTimeString()}");
    Messages.StatusMessage responseData =
        new Messages.StatusMessage("Service is avaliable", DateTime.Now);
    app.Logger.LogInformation(responseData.ToString());
    await context.Response.WriteAsJsonAsync(responseData);
});

app.MapGet("/ping", async (context) =>
{
    app.Logger.LogInformation($"Path: /ping  Time: {DateTime.Now.ToLongTimeString()}");
    Messages.StatusMessage status = new Messages.StatusMessage("pong", DateTime.Now);
    app.Logger.LogInformation(status.ToString());
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
        Messages.ErrorMessage error = new Messages.ErrorMessage("Invalid request parameters");
        app.Logger.LogError($"Exception occured: {error}: DateTime:{DateTime.Now.ToLongTimeString()}");
        await context.Response.WriteAsJsonAsync(error);
    }
    else
    {
        try
        {
            var Solvator = app.Services.GetRequiredService<ISolvator>();
            Messages.CalcOutputMessage solvation = Solvator.Solve(convertation);
            app.Logger.LogInformation(solvation.ToString());
            await context.Response.WriteAsJsonAsync(solvation);
        }
        catch (Exception e)
        {
            Messages.ErrorMessage error = new Messages.ErrorMessage($"Error during request processing: {e.Message}");
            app.Logger.LogError($"Exception occured: {error}: DateTime:{DateTime.Now.ToLongTimeString()}");
            await context.Response.WriteAsJsonAsync(error);
        }
    }
});

app.Run();
