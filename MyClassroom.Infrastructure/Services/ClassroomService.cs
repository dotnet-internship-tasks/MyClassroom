using Microsoft.EntityFrameworkCore;
using MyClassroom.Application.Services;
using MyClassroom.Application.ViewModels;
using MyClassroom.Core.Models;
using MyClassroom.Infrastructure.Data;

namespace MyClassroom.Infrastructure.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly MyClassroomContext _context;

        public ClassroomService(MyClassroomContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Classroom>> GetClassroomsAsync()
        {
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<Classroom> GetClassroomAsync(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            return classroom ?? throw new Exception();
        }

        public async Task<Classroom> CreateClassroomAsync(NewClassroomViewModel viewModel)
        {
            viewModel.Classroom.OrganizationId = viewModel.OrganizationsViewModel.SelectedOrganizationId;
            _context.Classrooms.Add(viewModel.Classroom);
            await _context.SaveChangesAsync();
            return viewModel.Classroom;
        }
    }
}
