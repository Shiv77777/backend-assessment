using backend_assignment.Interfaces;
using backend_assignment.Models;
using backend_assignment.Repositories;

namespace backend_assignment.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> SignUp(UserModel user)
        {
            try
            {
                return await _repository.SignUp(user);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Login(UserModel user)
        {
            try
            {
                return await _repository.Login(user);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Delete(UserModel user)
        {
            try
            {
                return await _repository.DeleteUser(user.EmailId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<UserModel>> Search(string prompt)
        {
            return await _repository.SearchUser(prompt, false);
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _repository.SearchUser("", true);
        }

        public async Task<string> AddFollower(string account, string follower)
        {
            try
            {
                if(account == follower)
                {
                    throw new Exception("Invalid Operation");
                }
                return await _repository.AddFollower(account, follower);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateUser(UserModel user)
        {
            try
            {
                return await _repository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
