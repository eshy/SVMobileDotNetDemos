using System;
using PicShare.Data.Entities;

namespace PicShare.Api.Models
{
    public class PictureModel
    {
        public int Id { get; set; }

        public string AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public static PictureModel ToModel(Picture item)
        {
            return new PictureModel
            {
                Id = item.Id,
                AddedBy = item.AddedBy != null ? item.AddedBy.UserName : String.Empty,
                AddedOn = item.AddedOn,
                Lat = item.Location != null && item.Location.Latitude.HasValue ? item.Location.Latitude.Value : 0,
                Lng = item.Location != null && item.Location.Longitude.HasValue ? item.Location.Longitude.Value : 0,
            };
        }
    }
}
