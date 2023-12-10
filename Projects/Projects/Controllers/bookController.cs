using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projects.Data;
using Projects.Models;

namespace Projects.Controllers
{
    public class bookController : Controller
    {
        private readonly ProjectsContext _context;

        public bookController(ProjectsContext context)
        {
            _context = context;
        }

        // GET: book
        public async Task<IActionResult> Index()
        {
              return _context.book != null ? 
                          View(await _context.book.ToListAsync()) :
                          Problem("Entity set 'ProjectsContext.book'  is null.");
        }
        public async Task<IActionResult> list()
        {
            return _context.book != null ?
                        View(await _context.book.OrderBy(m =>m.category).ToListAsync()) :
                        Problem("Entity set 'ProjectsContext.book'  is null.");
        }

        public async Task<IActionResult> itemSlider()
        {


            return _context.book != null ?
                        View(await _context.book.ToListAsync()) :
                        Problem("Entity set 'ProjectsContext.book'  is null.");
        }

    



    // GET: book/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.book == null)
            {
                return NotFound();
            }

            var book = await _context.book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: book/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,name,descr,price,discount,category,quantity")] book book)
        {
            {
                if (file != null)
                {
                    string filename = file.FileName;
                    //  string  ext = Path.GetExtension(file.FileName);
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    { await file.CopyToAsync(filestream); }

                    book.imgfile = filename;
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }



    






    // GET: book/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.book == null)
            {
                return NotFound();
            }

            var book = await _context.book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("Id,name,descr,price,discount,category,quantity,imgfile")] book book)

        {
            if (id != book.Id)
            { return NotFound(); }

            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                book.imgfile = filename;
            }
            _context.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.book == null)
            {
                return NotFound();
            }

            var book = await _context.book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.book == null)
            {
                return Problem("Entity set 'ProjectsContext.book'  is null.");
            }
            var book = await _context.book.FindAsync(id);
            if (book != null)
            {
                _context.book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool bookExists(int id)
        {
          return (_context.book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
