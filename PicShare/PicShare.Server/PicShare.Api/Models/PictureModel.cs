using System;
using PicShare.Data.Entities;

namespace PicShare.Api.Models
{
    public class PictureModel
    {
        public int Id { get; set; }

        public string AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public static PictureModel ToModel(Picture item)
        {
            return new PictureModel
            {
                Id = item.Id,
                AddedBy = item.AddedBy != null ? item.AddedBy.UserName : String.Empty,
                AddedOn = item.AddedOn,
            };
        }
    }
}
