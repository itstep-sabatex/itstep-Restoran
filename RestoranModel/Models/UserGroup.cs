using System;
using System.Collections.Generic;
using System.Text;

namespace RestoranModel.Models
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
