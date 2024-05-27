using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(7139, listenOptions =>
    {
        listenOptions.UseHttps();
    });
    options.ListenLocalhost(5139);
});

var app = builder.Build();


app.UseHttpsRedirection();


app.MapGet("/init-networks/{deviceSigfox}/{deviceLoraWan}", (string deviceSigfox, string deviceLoraWan, IMemoryCache cache) =>
{
    if (deviceSigfox != null)
        cache.Remove(deviceSigfox);
    if (deviceLoraWan != null)
        cache.Remove(deviceLoraWan);
    return Results.Ok();
});
app.MapPost("/sigfox/{device}", (string device, IMemoryCache cache) =>
{
    if (device != null)
        cache.Set(device, new { Result = true });
    return Results.Ok();
});

app.MapPost("/lorawan/{device}", (string device, IMemoryCache cache) =>
{
    if (device != null)
        cache.Set(device, new { Result = true });
    return Results.Ok();
});

app.MapGet("/sigfox/{device}", (string device, IMemoryCache cache) =>
{
    if (device != null)
        return Results.Ok(cache.Get(device));
    return Results.NotFound();
});

app.MapGet("/lorawan/{device}", (string device, IMemoryCache cache) =>
{
    if (device != null)
        return Results.Ok(cache.Get(device));
    return Results.NotFound();
});

app.MapRazorPages();
app.Run();