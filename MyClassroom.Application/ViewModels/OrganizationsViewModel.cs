using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MyClassroom.Application.ViewModels
{
    public class OrganizationsViewModel
    {
        [Display(Name = "Organization")]
        public int SelectedOrganizationId { get; set; }
        public IEnumerable<SelectListItem> Organizations { get; set; }
    }
}
