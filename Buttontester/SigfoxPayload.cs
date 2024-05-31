using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SigfoxDuplicate
{
    [JsonPropertyName("bsId")]
    public string BsId { get; set; }

    [JsonPropertyName("rssi")]
    public int Rssi { get; set; }

    [JsonPropertyName("nbRep")]
    public int NbRep { get; set; }
}

public class SigfoxPayload
{
    [JsonPropertyName("device")]
    public string Device { get; set; }

    [JsonPropertyName("lqi")]
    public string Lqi { get; set; }

    [JsonPropertyName("linkQuality")]
    public string LinkQuality { get; set; }

    [JsonPropertyName("duplicates")]
    public List<SigfoxDuplicate> Duplicates { get; set; }
}

