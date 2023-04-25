using Microsoft.EntityFrameworkCore;
using MyClassroom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassroom.Infrastructure.Data
{
    public class MyClassroomContext : DbContext
    {
        public MyClassroomContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
    }
}
