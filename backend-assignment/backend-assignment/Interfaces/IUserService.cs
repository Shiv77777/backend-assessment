using backend_assignment.Models;

namespace backend_assignment.Interfaces
{
    public interface IUserService
    {
        public Task<string> SignUp(UserModel user);
        public Task<string> Login(UserModel user);
        public Task<string> Delete(UserModel user);
        public Task<List<UserModel>> Search(string prompt);
        public Task<List<UserModel>> GetAll();
        public Task<string> AddFollower(string account, string follower);
        public Task<string> UpdateUser(UserModel user);
    }
}
