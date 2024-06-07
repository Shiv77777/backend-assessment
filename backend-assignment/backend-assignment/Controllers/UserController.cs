using backend_assignment.Core;
using backend_assignment.Interfaces;
using backend_assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_assignment.Controllers
{
    [Route("/")]
    public class UserController : Controller
    {
        private readonly IUserService _login;

        public UserController(IUserService login)
        {
            _login = login;
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<string> SignUp([FromBody]UserModel user)
        {
            return await _login.SignUp(user);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<string> Login([FromBody]UserModel user)
        {
            return await _login.Login(user);
        }

        [Route("Delete")]
        [HttpPost]
        public async Task<string> Delete([FromBody] UserModel user)
        {
            return await _login.Delete(user);
        }

        [Route("Search")]
        [HttpGet]
        public async Task<List<UserModel>> Search([FromQuery]string prompt)
        {
            return await _login.Search(prompt);
        }

        [Route("AllUsers")]
        [HttpGet]
        public async Task<List<UserModel>> GetAll()
        {
            return await _login.GetAll();
        }

        [Route("Follow")]
        [HttpPost]
        public async Task<string> Follow([FromBody]FollowModel users)
        {
            return await _login.AddFollower(users.account, users.follower);
        }

        [Route("Update")]
        [HttpPost]
        public async Task<string> Update([FromBody] UserModel user)
        {
            return await _login.UpdateUser(user);
        }
    }
}
