using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HandLab6
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MovieContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            //Add the studio "20th Century Fox" with movies
            using (var db = new MovieContext())
            {
                Studio studio = new Studio
                {
                    Name = "20th Century Fox",
                    Movies = new List<Movie>
                    {
                        new Movie
                        {
                            Title = "Avatar",
                            Genre = "Action"

                        },
                    new Movie
                        {
                            Title = "Deadpool",
                            Genre = "Action"
                        },
                        new Movie
                        {
                            Title = "Apollo 13",
                            Genre = "Drama"
                        }, 
                        new Movie
                        {
                            Title = "The Martian",
                            Genre = "Sci-Fi"
                        },
                    }
                };

                db.AddRange(studio);
                db.SaveChanges();
            }
        
            //Add studio "universal Pictures without movies
            using (var db = new MovieContext())
            {
                Studio studio2 = new Studio
                {
                    Name = "Universal Pictures"
                };
                db.Add(studio2);
                db.SaveChanges();
            }

            //Add Jurassic Park to Universal Pictures
            using (var db = new MovieContext())
            {
                Movie movie = new Movie {Title = "Jurassic Park", Genre = "Action"};
                Studio studioToUpdate = db.Studios.Include(s => s.Movies).Where(s => s.Name == "Universal Pictures").First();
                studioToUpdate.Movies.Add(movie);
                db.SaveChanges();
            }

            //Move Apollo 13 from Fox to Universal
            using (var db = new MovieContext())
            {
                Movie movie = db.Movies.Where(s => s.Title == "Apollo 13").First();
                movie.Studio = db.Studios.Where(m => m.Name == "Universal Pictures").First();
                db.SaveChanges();
            }

            //Remove Dead Pool
            using (var db = new MovieContext())
            {
                Movie movie = db.Movies.Where(s => s.Title == "Deadpool").First();
                movie.Studio = db.Studios.Where(m => m.Name == "20th Century Fox").First();
                db.Remove(movie);
                db.SaveChanges();
            }

            //List all studios and movies
            using (var db = new MovieContext())
            {
                var studios = db.Studios.Include( s => s.Movies);
                foreach( var s in studios)
                {
                    Console.WriteLine(s);
                    foreach (var m in s.Movies)
                    {
                        Console.WriteLine("\t\t" + m);
                    }
                }
            }
        }
    }
}
