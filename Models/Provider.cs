using System.Text.Json.Serialization;

namespace Commerce.Models
{
    public class Provider
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }


        [JsonIgnore]
        public IList<Product> Products { get; set; }
    }
}