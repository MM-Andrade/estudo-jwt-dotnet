using api_jwt.Models;

namespace api_jwt.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "admin", Password = "admin", Role = "Administrator"});
            users.Add(new User { Id = 2, Username = "regularuser", Password = "regularuser", Role = "User"});
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}
