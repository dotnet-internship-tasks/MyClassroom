﻿using MyClassroom.Application.ViewModels;
using MyClassroom.Core.Models;

namespace MyClassroom.Application.Services
{
    public interface IClassroomService
    {
        Task<IEnumerable<Classroom>> GetClassroomsAsync();
        Task<Classroom> CreateClassroomAsync(NewClassroomViewModel viewModel);
    }
}