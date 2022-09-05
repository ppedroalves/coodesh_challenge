namespace Commerce.ViewModels.Order
{
    public class ResultOrderViewModel
    {

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public float Total { get; set; }

        public List<ResultItensViewModel> Itens { get; set; }
    }
}
