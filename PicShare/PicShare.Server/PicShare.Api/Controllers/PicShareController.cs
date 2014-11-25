using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PicShare.Api.Helpers;
using PicShare.Api.Models;
using PicShare.Data.Repositories;

namespace PicShare.Api.Controllers
{
     [RoutePrefix("api")]
    public class PicShareController : ApiController
    {
        private  readonly AuthRepository _authRepository;
        private readonly PicRepository _picRepository;

        public PicShareController()
        {
            _authRepository = new AuthRepository();
            _picRepository = new PicRepository();
        }

        public string AuthenticatedUserName
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    //ServerAnalytics.CurrentRequest.AppUserId = User.Identity.Name;
                    return User.Identity.Name;
                }
                throw new UnauthorizedAccessException("Unauthorized");
            }
        }
        
        private string _accountId;
        public string AccountId
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(_accountId))
                    {
                        var account = _authRepository.FindUser(AuthenticatedUserName);
                        _accountId = account != null ? account.Id : String.Empty;
                    }

                    return _accountId;
                }
                catch (UnauthorizedAccessException)
                {
                }
                return String.Empty;

            }
        }

        [AllowAnonymous]
        [Route("Account")]
        public async Task<IHttpActionResult> PostRegister(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.RegisterUserAsync(userModel.NickName, userModel.Password);
 
            var errorResult = GetErrorResult(result);
            if (errorResult != null) return errorResult;

            var account = _authRepository.FindUser(userModel.NickName);
            if (account != null){return Ok();}

            return InternalServerError();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
 
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
 
                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }
 
                return BadRequest(ModelState);
            }
 
            return null;
        }

        //GET to Pictures
        [AllowAnonymous]
        [Route("Pictures", Name = "GetPictures")]
        public async Task<List<PictureModel>> GetPicturesAsync(int page = 1, int size = 20)
        {
            //Get chats for the user (Chat Name, Last Updated On, Last Message)
            var products = await _picRepository.GetPicturesAsync(page, size);
            return products.ConvertAll(PictureModel.ToModel);
        }

        //POST to Pictures
        [Authorize]
        [Route("Pictures")]
        public async Task<IHttpActionResult> PostProductPicture()
        {
            var picturesStorageAccount = ConfigHelper.GetAppSetting("PicturesStorageAccount", "");
            var picturesStorageKey = ConfigHelper.GetAppSetting("PicturesStorageKey", "");
            var picturesContainer = ConfigHelper.GetAppSetting("PicturesContainer", "pics");
            var file = await Request.Content.ReadAsStreamAsync();

            var result = await _picRepository.UploadPictureAsync(AccountId, picturesStorageAccount, picturesStorageKey, picturesContainer, file);

            if (result.Id > 0)
            {
                return CreatedAtRoute("GetPictures", new {}, PictureModel.ToModel(result));
            }

            return InternalServerError();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _authRepository.Dispose();
            }
 
            base.Dispose(disposing);
        }
 

    }
}
