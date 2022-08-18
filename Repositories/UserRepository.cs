using System.Collections.Generic;
using System.Linq;
using netcore_api_p001.Models;

namespace netcore_api_p001.Repositories
{
    public static class UserRepository
    {
        public static User Get(string Username, string Password)
        {
            var users = new List<User>();

            users.Add(new User { Id = 1, Username = "Paulo", Password = "123@qwe", Role = "manager" });
            users.Add(new User { Id = 1, Username = "Adriana", Password = "qwe@123", Role = "employee" });

            return users.Where(x => x.Username.ToLower() == Username.ToLower() && x.Password.ToLower() == Password.ToLower()).FirstOrDefault();
        }
    }
}
