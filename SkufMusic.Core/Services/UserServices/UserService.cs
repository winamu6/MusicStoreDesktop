using Microsoft.EntityFrameworkCore;
using SkufMusic.Core.Services.UserServices.UserServicesInterfaces;
using SkufMusic.Data.Data;
using SkufMusic.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly MusicStoreDbContext _db;

        public UserService(MusicStoreDbContext db)
        {
            _db = db;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            if (await UserExistsAsync(username))
                throw new Exception("Пользователь уже существует");

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Role = UserRole.Customer,
                Cart = new Cart()
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _db.Users.AnyAsync(u => u.Username == username);
        }

        private string HashPassword(string password) => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        private bool VerifyPassword(string password, string hash) => hash == HashPassword(password);
    }

}
