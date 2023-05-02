using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MyClassroom.Application.ViewModels
{
    public class RepositoriesViewModel
    {
        [Display(Name = "Template")]
        public int SelectedRepositoryId { get; set; }
        public IEnumerable<SelectListItem> Repositories { get; set; }
    }
}
