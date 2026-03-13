using System.Text.Json.Serialization;

namespace RestWithASPNET10.Data
{
    public class PersonDTOV2
    {

        [JsonPropertyName("Code"), JsonPropertyOrder(1)] public long Id { get; set; }
        [JsonPropertyName("First_Name"), JsonPropertyOrder(3)] public string? FirstName { get; set; }
        [JsonPropertyName("Last_Name"), JsonPropertyOrder(4)] public string? LastName { get; set; }
        [JsonPropertyName("Address"), JsonPropertyOrder(5)] public string? Address { get; set; }
        [JsonPropertyName("Gender"), JsonPropertyOrder(2)] public string? Gender { get; set; }
    }
}
