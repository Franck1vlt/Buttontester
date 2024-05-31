using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

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

app.MapPost("/sigfox/{device}", (string device, [FromBody] SigfoxPayload payload, IMemoryCache cache) =>
{
    if (device != null)
    {
        int duplicateCount = payload.Duplicates.Count;
        string Lqi = payload.Lqi;
        cache.Set(device, new { Result = true, SignalLevel = Lqi, GatewayCnt = duplicateCount });
    }
    return Results.Ok();
});

app.MapPost("/lorawan/{device}", (string device, [FromBody] LoRaWANPayload payload, IMemoryCache cache) =>
{
    if (device != null)
        cache.Set(device, new { Result = true, SignalLevel = payload.Metadata.Network.Lora.SignalLevel, GatewayCnt = payload.Metadata.Network.Lora.GatewayCnt });
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