using System;

namespace GridViewAnimations.Core.Models
{
    public class MovieModel
    {
        public MovieModel(int id, string title, DateTime releasedOn, string coverName)
        {
            Id = id;
            Title = title;
            ReleasedOn = releasedOn;
            CoverName = coverName;
        }

        public int Id { get; }

        public string Title { get; }

        public DateTime ReleasedOn { get; }

        public string CoverName { get; }
    }
}
