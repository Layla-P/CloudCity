using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudCityCakesMVC.Models.DTO
{
    public struct IngredientList
    {
        [JsonPropertyName("flavour")]
        public List<string> Flavour { get; set; }
        [JsonPropertyName("topping")]
        public List<string> Topping { get; set; }
        [JsonPropertyName("frosting")]
        public List<string> Frosting { get; set; }
    }
}