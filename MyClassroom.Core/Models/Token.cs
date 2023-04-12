using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassroom.Core.Models
{
    public class Token
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpireDate { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}
