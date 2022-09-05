using System.ComponentModel.DataAnnotations;

namespace Commerce.ViewModels.ProductViewModels
{
    public class EditorProductViewModel
    {
        [Required(ErrorMessage = "The field Category is required")]
        public int Category{ get; set; }


        [Required(ErrorMessage = "The field Provider is required")]
        public int Provider { get; set; }


        [Required(ErrorMessage ="The field Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The field Description is required")]
        public string Description { get; set; }


        [Required(ErrorMessage = "The field Price is required")]
        public float Price { get; set; }
    }
}
