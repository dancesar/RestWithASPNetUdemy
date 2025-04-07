using System.Text.Json.Serialization;
using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;

namespace RestWithASPNETUdemy.Data.DTO;

public class PersonDTO : ISupportsHyperMedia
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    
    [JsonPropertyName("address")]
    public string Address { get; set; }
    
    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}