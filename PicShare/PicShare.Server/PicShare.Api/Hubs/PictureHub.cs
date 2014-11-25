using Microsoft.AspNet.SignalR;
using PicShare.Api.Models;

namespace PicShare.Api.Hubs
{
    public class PictureHub : Hub
    {
        public void NewPicture(PictureModel picture)
        {
            Clients.All.NewPicture(picture);
        }
    }
}
