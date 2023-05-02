using MyClassroom.Core.Models;

namespace MyClassroom.Application.ViewModels
{
    public class NewAssignmentViewModel
    {
        public string Title { get; set; } = null!;
        public RepositoriesViewModel RepositoriesViewModel { get; set; } = new RepositoriesViewModel();
    }
}
