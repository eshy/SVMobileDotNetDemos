using System;

namespace PicShare.Data.Entities
{
    public class Picture
    {
        public int Id { get; set; }

        public Account AddedBy { get; set; }

        public DateTime? AddedOn { get; set; }
    }
}