using System.Text.Json.Serialization;

namespace Commerce.Models
{
    public class Itens
    {

        public int Id { get; set; }

        public Product Product { get; set; }

        public int Amount { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}