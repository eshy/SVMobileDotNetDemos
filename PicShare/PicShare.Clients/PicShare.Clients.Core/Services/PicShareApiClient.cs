using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EShyMedia.ClientApi.SimpleRestClient;
using PicShare.Clients.Core.Helpers;
using PicShare.Clients.Core.Model;

namespace PicShare.Clients.Core.Services
{
    public class PicShareApiClient : IPicShareApiClient
    {
        private readonly ISimpleRestClient _restClient;

        public PicShareApiClient(ISimpleRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = "http://picsshare.azurewebsites.net/api/";
        }

        public async Task<List<PictureModel>> GetPicturesAsync(int pageNumber, int pageSize)
        {
            var parameters = new RestParameters();
            parameters.AddPagingParameters(pageNumber, pageSize);
            
            var result = await _restClient
                .MakeRequestAsync<List<PictureModel>>("Pictures", 
                HttpMethod.Get, parameters);

            foreach (var pictureModel in result)
            {
                pictureModel.PictureUrl = 
                    String.Format("http://az693289.vo.msecnd.net/pics/picture_small_{0}.jpg",
                    pictureModel.Id);
            }
            return result;
        }

        public async Task CreateAccountAsync(string nickName, string password)
        {
            //Send a POST to resource "Account" with AccountModel in the body
            var account = new AccountModel
            {
                NickName = nickName,
                Password = password
            };

            var parameters = new RestParameters();
            parameters.AddParameter("body",account, RestParameterTypes.Body);
            
            var result = await _restClient
                .MakeRequestAsync<string>("Account", HttpMethod.Post, parameters);

            Settings.NickName = nickName;
            Settings.Password = password;

        }

        public async Task<PictureModel> UploadPictureAsync(byte[] pictureBytes)
        {
            var parameters = new RestParameters();
            parameters.AddBasicAuthorization(Settings.NickName, Settings.Password);
            
            var stream = new MemoryStream(pictureBytes);
            parameters.AddParameter("file", stream, RestParameterTypes.Body);
            parameters.AddParameter("lat", 0, RestParameterTypes.QueryString);
            parameters.AddParameter("lng", 0, RestParameterTypes.QueryString);

            var result = await _restClient
                .MakeRequestAsync<PictureModel>("Pictures", HttpMethod.Post, parameters);

            return result;
        }
    }
}
