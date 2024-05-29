using ButtonTesterLibrary;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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
    {
        var result = cache.Get(device);
        if (result == null)
        {
            return Results.Ok(new ResponseModel {Result = false});
        }
        else
        {
            return Results.Ok(result);
        }
    }
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