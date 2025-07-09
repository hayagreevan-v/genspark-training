using System.Threading.Tasks;
using VMDemo.Models;
using VMDemo.Repositories;

namespace VMDemo.Services
{
    public class UserService
    {
        private readonly UserRepo _userRepo;
        public UserService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<User> Add(string name)
        {
            User user = new User { Name = name };
            user = await _userRepo.Add(user);
            return user;
        }
        public async Task<List<User>> ViewAll()
        {
            var users = await _userRepo.GetAll();
            return users;
        }
    }
}