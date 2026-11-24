
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
            var user = new Parent
            {
                Email = email,
                Name = name,
                Password = password
            };

            _context.ParentTable.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Parent?> GetParentByCredentials(string? email, string? password)
        {
            return await _context.ParentTable.FirstOrDefaultAsync(
                u => u.Email == email && u.Password == password);
        }

        public async Task<bool> Login(string? email, string? password)
        {
            var user = await GetParentByCredentials(email, password);
            return user != null;
        }
    }
}
