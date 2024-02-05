using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComparativeComber.Entities;
using ComparativeComber.Helpers;
using Microsoft.Extensions.Options;

namespace ComparativeComber.Services
{
    public interface IUserService
    {
        Task<UserAuthenticationResult> Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        Task<User> GetById(string id);
        Task<IdentityResult> Register(RegisterRequest model);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppSettings _appSettings;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        public async Task<UserAuthenticationResult> Authenticate(AuthenticateRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new UserAuthenticationResult { Success = true, User = user };
            }
            return new UserAuthenticationResult { Success = false };
        }

        public IEnumerable<User> GetAll()
        {
            return _userManager.Users;
        }

        public async Task<User> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> Register(RegisterRequest model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,

                // Additional properties
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

   
    }
    public class UserAuthenticationResult
    {
        public bool Success { get; set; }
        public User User { get; set; }
    }
}

