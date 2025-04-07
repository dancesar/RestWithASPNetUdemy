using System.Text.Json.Serialization;

namespace RestWithASPNETUdemy.Data.DTO;

public class BooksDTO
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("launch_date")]
    public DateTime LaunchDate { get; set; }
}