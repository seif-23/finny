using FinnyApp.DAL.Database;
using FinnyApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinnyApp.BLL.Services
{
    public class AuthService
    {
        private readonly DContext _context;
        public AuthService(DContext context)
        {
            _context = context;
        }

        public async Task Join(string name, string email, string password)
        {
            var user = new Parent { Email = email, Name = name, Password = password };
            _context.ParentTable.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Login(string? email, string? password)
        {
            var users = await _context.ParentTable.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return !(users == null || users.Password != password);
        }
    }
}
