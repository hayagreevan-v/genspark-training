using BankingChatbot.Models;
using BankingChatbot.Models.DTOs;
using BankingChatbot.Repositories;

namespace BankingChatbot.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            try
            {
                List<User>? users = _userRepository.GetAll();
                if (users == null || users.Count() == 0) throw new Exception("No user found");
                return users;
            }
            catch
            {
                throw;
            }
        }

        public User Get(Guid uuid)
        {
            try
            {
                User? user = _userRepository.Get(uuid);
                if (user == null) throw new Exception("No user found");
                return user;
            }
            catch
            {
                throw;
            }
        }
        public void Clear(Guid uuid)
        {
            try
            {
                User? user = _userRepository.Get(uuid);
                if (user == null || user.ChatHistory == null) throw new Exception("No user found");
                user.ChatHistory.Clear();
            }
            catch
            {
                throw;
            }
        }

        public User Add(UserCreateDTO dto)
        {
            try
            {
                User? user = _userRepository.Add(dto);
                return user;
            }
            catch
            {
                throw;
            }
        }
    }
}