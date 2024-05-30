using Microsoft.AspNetCore.Mvc;
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

app.MapPost("/lorawan/{device}", (string device, LoRaWANPayload payload, IMemoryCache cache) =>
{
    if (device != null)
        cache.Set(device, payload);
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
    if (device != null && cache.TryGetValue(device, out LoRaWANPayload payload))
        return Results.Ok(payload);
    return Results.NotFound();
});

app.MapRazorPages();
app.Run();