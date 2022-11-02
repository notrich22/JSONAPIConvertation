using JSONConvertationAPI;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (context) => {
    Messages.StatusMessage responseData =
        new Messages.StatusMessage("Service is avaliable", DateTime.Now);
    Console.WriteLine(responseData);
    await context.Response.WriteAsJsonAsync(responseData);
});
app.MapGet("/ping", async (context) =>
{
    Messages.StatusMessage status = new Messages.StatusMessage("pong", DateTime.Now);
    Console.WriteLine(status);
    await context.Response.WriteAsJsonAsync(status);
});
app.MapPost("/calculate", async (context) =>
{
    Messages.CalcInputMessage? convertation = null;
    try
    {
        convertation = await context.Request.ReadFromJsonAsync<Messages.CalcInputMessage>();
        Console.WriteLine(convertation);
    }
    catch(Exception e) {
        Messages.ErrorMessage error = new Messages.ErrorMessage($"Error during request processing: {e.Message}");
    }
    if (convertation is null)
    {
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
        catch (Exception ex)
        {
            Messages.ErrorMessage error = new Messages.ErrorMessage($"Error during request processing: {ex.Message}");
            Console.WriteLine(error);
            await context.Response.WriteAsJsonAsync(error);
        }
    }
});
app.Run();
