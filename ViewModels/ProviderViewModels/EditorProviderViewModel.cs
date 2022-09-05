using System.ComponentModel.DataAnnotations;

namespace Commerce.ViewModels.ProviderViewModels
{
    public class EditorProviderViewModel
    {


        [Required( ErrorMessage = "The field Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The field Email is required")]
        public string Email { get; set; }
    }
}
