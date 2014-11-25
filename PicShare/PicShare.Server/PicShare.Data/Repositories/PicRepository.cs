using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicShare.Data.Context;
using PicShare.Data.Entities;
using PicShare.Data.Helpers;

namespace PicShare.Data.Repositories
{
    public class PicRepository : IDisposable
    {
        private AuthContext _ctx;
        private UserManager<Account> _userManager;

        public PicRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<Account>(new UserStore<Account>(_ctx));
        }

        public async Task<Picture> UploadPictureAsync(string accountId, string azureAccount,
            string azureKey, string container, Stream file)
        {
            var picture = new Picture
            {
                AddedBy = await _userManager.FindByIdAsync(accountId),
                AddedOn = DateTime.UtcNow
            };

            _ctx.Pictures.Add(picture);

            await _ctx.SaveChangesAsync();

            await FileHelperMethods.SaveFileToAzureAsync(file, "small", picture.Id, azureAccount, azureKey, container);

            return picture;
        }


        public async Task<List<Picture>> GetPicturesAsync(int pageNumber, int pageSize)
        {
            var products = await _ctx.Pictures
                .Include("AddedBy")
                .OrderByDescending(product => product.Id)
                .Skip(pageSize*(pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return products;
        }

        #region Disposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_ctx == null) return;

            // Free managed resources
            _ctx.Dispose();
            _ctx = null;
        }

        #endregion

    }
}
