using System.Text.Json.Serialization;

namespace CloudCityCakesMVC.Models.DTO
{
    public struct OrderDetails
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("cake")]
        public Cake Cake { get; set; }
        
        [JsonPropertyName("isNewUser")]
        public bool IsNewUser { get; set; }
    }
}