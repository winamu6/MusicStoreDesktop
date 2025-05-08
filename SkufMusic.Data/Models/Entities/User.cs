using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Data.Models.Entities
{
    public enum UserRole
    {
        Customer,
        Manager,
        Administrator
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Cart? Cart { get; set; }
    }
}
