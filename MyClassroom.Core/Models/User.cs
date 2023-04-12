using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassroom.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }   = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Role> Roles { get; set; }

    }
}
