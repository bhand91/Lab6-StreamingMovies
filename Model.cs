using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HandLab6
{

    public class MovieContext : DbContext

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = database.db");
        }
        public DbSet<Movie> Movies {get; set;}

        public DbSet<Studio> Studios {get;set;}
    }

        public class Movie
        {
            public int MovieId {get; set;} //PK

            public string Title {get; set;}

            public string Genre {get; set;}

            public int StudioID {get; set;} //foreign key

            public Studio Studio {get; set;}

            public override string ToString()
            {
                return $"{Title} \t Genre: {Genre}";
            }
        }

        public class Studio
        {
            public int StudioId {get; set;} //PK

            public string Name {get; set;}

            public List<Movie> Movies {get; set;}

            public override string ToString()
            {
                return $"Studio {StudioId} - {Name}";
            }
        }
    }
