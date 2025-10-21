using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Interfaces.Repositories.Models;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext db;

        public UserRepository()
        {
            db = new AppDbContext();
        }

        public async Task<User> CreateAsync(User entity)
        {
            
            db.Users.Add(entity);
            var result = await db.SaveChangesAsync();
            if (result <= 0)
            {
                throw new Exception("Failed to create User");
            }
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            
            var user = db.Users.Find(id);
            if(user == null)
            {
                return false;
            }
            db.Users.Remove(user);
            var result = await db.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateAsync(User entity)
        {
            
            var existingUser = await db.Users.FindAsync(entity.Id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            existingUser.Username = entity.Username;
            existingUser.Email = entity.Email;
            existingUser.Password = entity.Password;
            db.Entry(existingUser).State = EntityState.Modified;
            var result = await db.SaveChangesAsync();
            if (result <= 0)
            {
                throw new Exception("Failed to update User");
            }
            return entity;
        }
    }
}