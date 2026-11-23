using FinnyApp.DAL.Database;
using FinnyApp.DAL.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.BLL.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        public AuthService(DataContext context)
        {
            _context = context;
        }
        /*
        public async Task Join(string name, string email, string password)
        {
            var user = new User { Email = email, Name = name, Password = password };
            _context.UserTable.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Login(string? email, string? password)
        {
            var users = await _context.UserTable.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return !(users == null || users.Password != password);
        }
        */
    }
}
