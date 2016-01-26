using System;
using System.Collections.Generic;
using System.Linq;
using GridViewAnimations.Core.Models;

namespace GridViewAnimations.Core.Services
{
    public interface IMovieDataService
    {
        List<MovieModel> GetMovies();
        MovieModel GetMovieById(int id);
    }
    public class MovieDataService : IMovieDataService
    {
        private List<MovieModel> _movies;
        private void InitializeMovies()
        {
            if (_movies == null || _movies.Count == 0)
            {
                _movies = new List<MovieModel>
                {
                    new MovieModel(1, "The Dark Knight", new DateTime(2008, 7, 18), "01.jpg"),
                    new MovieModel(2, "Pulp Fiction", new DateTime(1994, 10, 14), "02.jpg"),
                    new MovieModel(3, "The Good, The Bad and the Ugly", new DateTime(1966, 12, 23), "03.jpg"),
                    new MovieModel(4, "Avatar", new DateTime(2009, 12, 18), "04.jpg"),
                    new MovieModel(5, "Léon", new DateTime(1994, 11, 18), "05.jpg"),
                    new MovieModel(6, "North by Northwest", new DateTime(1959, 9, 26), "06.jpg"),
                    new MovieModel(7, "The Big Lebowski", new DateTime(1998, 3, 6), "07.jpg"),
                    new MovieModel(8, "E.T. the Extra-Terrestrial", new DateTime(1982, 6, 11), "08.jpg"),
                    new MovieModel(9, "Twelve Monkeys", new DateTime(1995, 12, 27), "09.jpg"),
                    new MovieModel(10, "Mad Max: Road Fury", new DateTime(2015, 5, 15), "10.jpg"),
                    new MovieModel(11, "Jurassic World", new DateTime(2015, 6, 12), "11.jpg"),
                    new MovieModel(12, "Star Wars: Episode VII - The Force Awakens", new DateTime(2015, 12, 18), "12.jpg"),
                    new MovieModel(13, "The Martian", new DateTime(2015, 10, 2), "13.jpg"),
                    new MovieModel(14, "The Hateful Eight", new DateTime(2015, 12, 25), "14.jpg"),
                    new MovieModel(15, "The Revenant", new DateTime(2015, 12, 25), "15.jpg")
                };
            }
        }

        public List<MovieModel> GetMovies()
        {
            InitializeMovies();
            return _movies;
        }

        public MovieModel GetMovieById(int id)
        {
            InitializeMovies();
            return _movies.FirstOrDefault(m => m.Id == id);
        }
    }
}
