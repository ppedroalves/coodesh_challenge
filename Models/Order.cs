namespace Commerce.Models
{
    public class Order
    {

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public float Total { get; set; }

        public IList<Itens> Itens { get; set; }


    }
}
