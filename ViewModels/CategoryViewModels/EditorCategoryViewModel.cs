using System.ComponentModel.DataAnnotations;

namespace Commerce.ViewModels
{
    public class EditorCategoryViewModel
    {

        [Required(ErrorMessage = "The field Name is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The field Description is required")]
        public string Description { get; set; }
    }
}
