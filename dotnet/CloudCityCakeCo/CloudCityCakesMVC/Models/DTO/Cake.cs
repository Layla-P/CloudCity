using System.Text.Json.Serialization;

namespace CloudCityCakesMVC.Models.DTO
{
    public class Cake
    {
        [JsonPropertyName("topping")]
        public string Topping { get; set; }
        
        [JsonPropertyName("frosting")]
        public string Frosting { get; set; }
        
        [JsonPropertyName("flavour")]
        public string Flavour { get; set; }
        
        [JsonPropertyName("size")]
        public string Size { get; set; }
        
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}