using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicShare.Data.Context;
using PicShare.Data.Entities;

namespace PicShare.Data.Repositories
{
    public class AuthRepository : IDisposable
    {
        private readonly AuthContext _ctx;

        private readonly UserManager<Account> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<Account>(new UserStore<Account>(_ctx));
        }

        public async Task<IdentityResult> RegisterUserAsync(string nickName, string password)
        {
            var user = new Account
            {
                UserName = nickName,
                JoinedOn = DateTime.UtcNow,
                LastSignedInOn = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<bool> IsUserValidAsync(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);
            if (user != null)
            {
                user.LastSignedInOn = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
            }
            return user != null;
        }

        public Account FindUser(string userName)
        {
            var user = _userManager.FindByName(userName);
            return user;
        }


        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}
