using backend_assignment.Models;

namespace backend_assignment.Interfaces
{
    public interface IUserRepository
    {
        public Task<string> SignUp(UserModel user);
        public Task<string> Login(UserModel user);
        public Task<string> DeleteUser(string email);
        public Task<List<UserModel>> SearchUser(string prompt, bool isGetAll);
        public Task<string> AddFollower(string account, string follower);
        public Task<string> UpdateUser(UserModel user);
    }
}
