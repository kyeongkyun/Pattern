using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAllUsers()
        {
            // In a real application, this would fetch data from a database
            return new List<User>
            {
                    new User { Id = 1, Name = "Alice" },
                    new User { Id = 2, Name = "Bob" }
            };
        }

        public User? GetUserById(int id)
        {
            return GetAllUsers().FirstOrDefault(u => u.Id == id);
        }
    }
}
