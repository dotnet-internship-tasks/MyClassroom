using MyClassroom.Core.Models;

namespace MyClassroom.Application.ViewModels
{
    public class NewClassroomViewModel
    {
        public Classroom Classroom { get; set; } = new Classroom();
        public OrganizationsViewModel OrganizationsViewModel { get; set; }        
    }
}
