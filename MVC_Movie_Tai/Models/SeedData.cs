using Microsoft.EntityFrameworkCore;
using MVC_Movie_Tai.Data;
using MVC_Movie_Tai.Models;


namespace MVC_Movie_Tai.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var db = new MVC_Movie_TaiContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MVC_Movie_TaiContext>>()))
        {
            // Look for any movies.
            if (db.Movie.Any())
            {
                return;   // DB has been seeded
            }
            db.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M
                }
            );
            db.SaveChanges();
        }
    }
}