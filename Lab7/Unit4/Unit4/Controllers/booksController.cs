using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unit4.Data;
using Unit4.Models;

namespace Unit4.Controllers
{
    public class booksController : Controller
    {
        private readonly Unit4Context _context;

        public booksController(Unit4Context context)
        {
            _context = context;
        }
     


        // GET: books
        public async Task<IActionResult> Index()
        { 
              return _context.book != null ? 
                          View(await _context.book.ToListAsync()) :
                          Problem("Entity set 'Unit4Context.book'  is null.");
        }
        public async Task<IActionResult> Catelog()
        {
            return _context.book != null ?
                View(await _context.book.ToListAsync()) :
                Problem("Entity set 'Unit4Context.book' is null");
        }
        public async Task<IActionResult> orderdetails()
        {
            var orItems = await _context.orderdetail.FromSqlRaw("select orders.Id as id, name as customer, title as booktitle, orders.quantity as quantity from book, orders, usersaccounts  where book.id = orders.bookid and orders.custid = usersaccounts.id  ").ToListAsync();
            return View(orItems);
        }

        public async Task<IActionResult> search()
        {
            List<book> brBook = new List<book>();
            return View(brBook);
        }

        // POST: items/search
        [HttpPost]
        public async Task<IActionResult> search(string s)
        {
            var brBook = await _context.book.FromSqlRaw("select * from book where info LIKE '%" + s + "%' ").ToListAsync();
            return View(brBook);
        }


        // GET: books/Details/5
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

        // GET: books/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,title,info,price,discount,pubdate,category,bookquantity")] book book)
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
        
        return View(book);
        }

        // GET: books/Edit/5
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

        // POST: books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,title,info,price,discount,pubdate,category,bookquantity,imgfile")] book book)
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


        // GET: books/Delete/5
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

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.book == null)
            {
                return Problem("Entity set 'Unit4Context.book'  is null.");
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
        public async Task<IActionResult> searchall()
        {

            {
                book brItem = new book();

                return View(brItem);
            }
        }

        [HttpPost]

        public async Task<IActionResult> SearchAll(string tit)
        {
            var bkItems = await _context.book.FromSqlRaw("select * from book where title = '" + tit + "' ").FirstOrDefaultAsync();

            return View(bkItems);
        }


    }

}

