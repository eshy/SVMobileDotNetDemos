using System;
using Newtonsoft.Json;

namespace PicShare.Clients.Core.Model
{
    public class PictureModel
    {
        public int Id { get; set; }

        public string AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        [JsonIgnore]
        public string PictureUrl { get; set; }
    }
}

