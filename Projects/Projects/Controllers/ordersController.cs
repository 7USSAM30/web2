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
    public class ordersController : Controller
    {
        private readonly ProjectsContext _context;

        public ordersController(ProjectsContext context)
        {
            _context = context;
        }

        // GET: orders
        public async Task<IActionResult> Index()
        {
              return _context.orders != null ? 
                          View(await _context.orders.ToListAsync()) :
                          Problem("Entity set 'ProjectsContext.orders'  is null.");
        }
        public async Task<IActionResult> Buy(int? id)
        {
                var book = await _context.book.FindAsync(id);
                return View(book);
        
        }
        [HttpPost]
        public async Task<IActionResult> Buy(int bookId, int quantity)
        {
            orders order = new orders();
            order.bookId = bookId;
            order.quantity = quantity;
            order.userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            order.buydate = DateTime.Today;
            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("ProjectsContext");
            SqlConnection conn = new SqlConnection(conStr);
            string sql;
            int qt = 0;
            sql = "select * from book where (id ='" + order.bookId + "' )";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                qt = (int)reader["quantity"]; // store quantity
            }
            reader.Close();
            conn.Close();
            if (order.quantity > qt)
            {
                ViewData["message"] = "maxiumam order quantity sould be " + qt;
                var book = await _context.book.FindAsync(bookId);
                return View(book);
            }
            else
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                sql = "UPDATE book  SET quantity  = quantity   - '" + order.quantity + "'  where (id ='" + order.bookId + "' )";
                comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction(nameof(myorders));
            }
        }

        public async Task<IActionResult> ordersdetail(int? id)
        {
            var orItems = await _context.orderdetail
                .FromSqlRaw("SELECT usersaccounts.id, usersaccounts.name AS username, orders.buydate AS BuyDate, " +
                            "book.price * orders.quantity AS totalprice, orders.quantity AS quantity " +
                            "FROM orders " +
                            "JOIN usersaccounts ON orders.userid = usersaccounts.id " +
                            "JOIN book ON orders.bookid = book.id " +
                            "WHERE orders.userid = {0}", id)
                .ToListAsync();

            return View(orItems);
        }



        public async Task<IActionResult> myorders()

        {


            int userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));

            var orItems = await _context.orders.FromSqlRaw("select *  from orders where  userid = '" + userid + "'  ").ToListAsync();

            return View(orItems);


        }

    


        public async Task<IActionResult> customerOrders(int? id)

        {
            int userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var orItems = await _context.orders.FromSqlRaw("select *  from orders where  userid = '" + id + "'  ").ToListAsync();

            return View(orItems);
            

        }
        public async Task<IActionResult> customerreport()
         {
          var orItems = await _context.report
        .FromSqlRaw("SELECT usersaccounts.id AS Id, usersaccounts.name AS customername, SUM(orders.quantity * book.price) AS total " +
                    "FROM orders " +
                    "JOIN book ON orders.bookid = book.id " +
                    "JOIN usersaccounts ON orders.userid = usersaccounts.id " +
                    "GROUP BY usersaccounts.id, usersaccounts.name")
        .ToListAsync();

        return View(orItems);
         }



        // GET: orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.orders == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }
        public async Task<IActionResult> Create(int? id)

        {

            var book = await _context.book.FindAsync(id);


            return View(book);

        }

        // GET: orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,bookId,userid,buydate,quantity")] orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {   
            if (id == null || _context.orders == null)
            {
                return NotFound();
            }

            var orders = await _context.orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,bookId,userid,buydate,quantity")] orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ordersExists(orders.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(myorders));
            }
            return View(orders);
        }

        // GET: orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.orders == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.orders == null)
            {
                return Problem("Entity set 'ProjectsContext.orders'  is null.");
            }
            var orders = await _context.orders.FindAsync(id);
            if (orders != null)
            {
                _context.orders.Remove(orders);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ordersExists(int id)
        {
          return (_context.orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
