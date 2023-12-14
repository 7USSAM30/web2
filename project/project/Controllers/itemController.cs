using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class itemController : Controller
    {
        private readonly projectContext _context;

        public itemController(projectContext context)
        {
            _context = context;
        }

        // GET: item
        public async Task<IActionResult> Index()
        {
            
            return _context.item != null ? 
                          View(await _context.item.ToListAsync()) :
                          Problem("Entity set 'projectContext.item'  is null.");
        }

        // GET: item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.item == null)
            {
                return NotFound();
            }

            var item = await _context.item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: item/Create
        // GET: item/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,name,descr,price,discount,category,quantity")] item item)
        {
            {
                if (file != null)
                {
                    string filename = file.FileName;
                    //  string  ext = Path.GetExtension(file.FileName);
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    { await file.CopyToAsync(filestream); }

                    item.imagefilename = filename;
                }

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }


        // GET: item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.item == null)
            {
                return NotFound();
            }

            var item = await _context.item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,descr,price,quantity,discount,category,imagefilename")] item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!itemExists(item.Id))
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
            return View(item);
        }

        // GET: item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.item == null)
            {
                return NotFound();
            }

            var item = await _context.item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.item == null)
            {
                return Problem("Entity set 'projectContext.item'  is null.");
            }
            var item = await _context.item.FindAsync(id);
            if (item != null)
            {
                _context.item.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool itemExists(int id)
        {
          return (_context.item?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
