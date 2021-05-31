using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Data.Entities
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RolName { get; set; }
        public string ImgURL { get; set; }
        public DateTime Birthday { get; set; }

        public bool FirstTimeStart { get; set; }
    }
}
