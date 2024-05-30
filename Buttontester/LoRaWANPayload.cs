using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class LoRaWANPayload
{
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; set; }
}


public class Metadata
{
    [JsonPropertyName("network")]
    public Network Network { get; set; }
}

public class Network
{
    [JsonPropertyName("lora")]
    public Lora Lora { get; set; }
}

public class Lora
{
    //[JsonPropertyName("devEUI")]
    //public string DevEUI { get; set; }

    [JsonPropertyName("signalLevel")]
    public int SignalLevel { get; set; }

    //[JsonPropertyName("gatewayCnt")]
    //public int GatewayCnt { get; set; }
}
