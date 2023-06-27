using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Movie_Tai.Data;
using MVC_Movie_Tai.Models;

namespace MVC_Movie_Tai.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MVC_Movie_TaiContext _db;

        public MoviesController(MVC_Movie_TaiContext db)
        {
            _db = db;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString, string movieGenre)
        {
            if (_db.Movie == null)
            {
                return Problem("Entiry set 'MVC_Movie_TaiContext.Movie' is null");
            }

            //use LinQ to get list of Genres
            IQueryable<string> genreQuery = from m in _db.Movie
                                             orderby m.Genre
                                             select m.Genre;

            //use LinQ to get list of title
            var movies = from m in _db.Movie
                        select m;

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(g => g.Genre == movieGenre);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                movies=movies.Where(m=>m.Title!.Contains(searchString));
            }
            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };
            return View(movieGenreVM);
              //return _db.Movie != null ? 
              //            View(await _db.Movie.ToListAsync()) :
              //            Problem("Entity set 'MVC_Movie_TaiContext.Movie'  is null.");
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Movie == null)
            {
                return NotFound();
            }

            var movie = await _db.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Image,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _db.Add(movie);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Movie == null)
            {
                return NotFound();
            }

            var movie = await _db.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Image,Genre,Price")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(movie);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Movie == null)
            {
                return NotFound();
            }

            var movie = await _db.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Movie == null)
            {
                return Problem("Entity set 'MVC_Movie_TaiContext.Movie'  is null.");
            }
            var movie = await _db.Movie.FindAsync(id);
            if (movie != null)
            {
                _db.Movie.Remove(movie);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_db.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
