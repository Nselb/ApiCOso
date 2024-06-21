using System.Text.Json.Serialization;

namespace ApiCOso
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
    public class OrderDetails
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
