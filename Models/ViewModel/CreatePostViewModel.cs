using Microsoft.AspNetCore.Mvc.Rendering;

namespace SD_330_W22SD_Assignment.Models.ViewModel
{
    public class CreatePostViewModel
    {
       public IEnumerable<int> SelectedTags { get; set; }
       public IEnumerable<SelectListItem> Tags { get; set; }
    }
}
