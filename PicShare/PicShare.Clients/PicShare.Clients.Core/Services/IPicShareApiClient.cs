using System.Collections.Generic;
using System.Threading.Tasks;
using PicShare.Clients.Core.Model;

namespace PicShare.Clients.Core.Services
{
    public interface IPicShareApiClient
    {
        Task<List<PictureModel>> GetPicturesAsync(int pageNumber, int pageSize);

        Task CreateAccountAsync(string nickName, string password);

        Task<PictureModel> UploadPictureAsync(byte[] pictureBytes);
    }
}