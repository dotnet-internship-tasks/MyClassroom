using MyClassroom.Application.ViewModels;
using MyClassroom.Core.Models;

namespace MyClassroom.Application.Services
{
    public interface IAssignmentService
    {
        Task<Assignment> CreateAssignmentAsync(int classroomId, NewAssignmentViewModel viewModel);
        Task<IEnumerable<Assignment>> GetAssignmentsAsync(int classroomId);
    }
}