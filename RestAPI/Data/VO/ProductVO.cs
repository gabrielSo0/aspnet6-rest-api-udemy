using System.Text.Json.Serialization;

namespace RestAPI.Data.VO
{
    public class ProductVO
    {
        [JsonPropertyName("code")]
        public int Id { get; set; }
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
