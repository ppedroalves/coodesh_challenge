using Commerce.ViewModels.ProviderViewModels;

namespace Commerce.ViewModels.ProductViewModels
{
    public class ResultProductViewModel
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public EditorProviderViewModel Provider { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public DateTime Created { get; set; }
    }
}
