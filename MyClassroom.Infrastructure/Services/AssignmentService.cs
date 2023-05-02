using Microsoft.EntityFrameworkCore;
using MyClassroom.Application.Services;
using MyClassroom.Application.ViewModels;
using MyClassroom.Core.Models;
using MyClassroom.Infrastructure.Data;

namespace MyClassroom.Infrastructure.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly MyClassroomContext _context;

        public AssignmentService(MyClassroomContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync(int classroomId)
        {
            var classroom = await _context.Classrooms
                .Where(c => c.Id == classroomId)
                .Include(c => c.Assignments)
                .FirstOrDefaultAsync();
            return classroom == null ? throw new Exception() : (IEnumerable<Assignment>)classroom.Assignments;
        }

        public async Task<Assignment> CreateAssignmentAsync(int classroomId, NewAssignmentViewModel viewModel)
        {
            var classroom = await _context.Classrooms
                .Where(c => c.Id == classroomId)
                .Include(c => c.Assignments)
                .FirstOrDefaultAsync();
            if (classroom == null)
            {
                throw new Exception();
            }
            var assignment = new Assignment()
            {
                Title = viewModel.Title,
                TemplateId = viewModel.RepositoriesViewModel.SelectedRepositoryId,
                ClassroomId = classroomId,
            };       
            classroom.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }
    }
}
