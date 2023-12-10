using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projects.Data;
using Projects.Models;

namespace Projects.Controllers
{
    public class usersaccountsController : Controller
    {
        private readonly ProjectsContext _context;

        public usersaccountsController(ProjectsContext context)
        {
            _context = context;
        }

        // GET: useraccounts
        public async Task<IActionResult> Index()
        {
              return _context.usersaccounts != null ? 
                          View(await _context.usersaccounts.ToListAsync()) :
                          Problem("Entity set 'ProjectsContext.useraccounts'  is null.");
        }
        public async Task<IActionResult> email(int? id)
        {


            return View();
        }


        [HttpPost, ActionName("email")]
        [ValidateAntiForgeryToken]
        public IActionResult email(string address, string subject, string body)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("homm9807@gmail.com");
            mail.To.Add(address); // receiver email address
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("homm9807@gmail.com", "mydjptatmcmypudn");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            ViewData["Message"] = "Email sent.";
            return View();

        }


        public async Task<IActionResult> Detailsitems(int? id)
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

        // GET: useraccounts/Details/5


        // GET: useraccounts/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Useralls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, name, pass, RegistDate")] usersaccounts useraccounts)
        {
       

            useraccounts.role = "customer";
            useraccounts.RegistDate = DateTime.Now;
                _context.Add(useraccounts);
                await _context.SaveChangesAsync();
            ViewData["added"] = "Added sucssefual";
            return RedirectToAction(nameof(login));
    
            
        }
        public IActionResult CreateAdmin()
        {
            return View();
        }
        // POST: Useralls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin([Bind("Id, name, pass, RegistDate")] usersaccounts useraccounts)
        {

            useraccounts.role = "admin";
            useraccounts.RegistDate = DateTime.Now;
            _context.Add(useraccounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(admin));


        }

        public IActionResult login()
        {
            return View();
        }

        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(string na, string pa)
        {
            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("projectsContext");
            SqlConnection conn1 = new SqlConnection(conStr);
            string sql;
            sql = "SELECT * FROM usersaccounts where name ='" + na + "' and  pass ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string id = Convert.ToString((int)reader["Id"]);
                string na1 = (string)reader["name"];
                string ro = (string)reader["role"];
                HttpContext.Session.SetString("userid", id);
                HttpContext.Session.SetString("Name", na1);
                HttpContext.Session.SetString("Role", ro);
                reader.Close();
                conn1.Close();
                if (ro == "admin")
                {
                    return RedirectToAction(nameof(admin));

                }
                return RedirectToAction(nameof(customer));
            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
        }


        public async Task<IActionResult> admin(int? id)
        {
            string ss = HttpContext.Session.GetString("Name");
            ViewData["admin"] = ss;
            return View();
        }

        public IActionResult customer(int? id)
        {
            string ss = HttpContext.Session.GetString("Name");
            ViewData["customer"] = ss;
            
            return View(_context.book.Where(k => k.discount == "yes"));
        }

        public async Task<IActionResult> searchall()
        {
            {
                usersaccounts brItem = new usersaccounts();

                return View(brItem);
            }
        }

        [HttpPost]
        public async Task<IActionResult> searchall(string role,string name)
        {
            var usItems = await _context.usersaccounts.FromSqlRaw("select * from usersaccounts where name = '" + name + "' ").FirstOrDefaultAsync();

            return View(usItems);
        }



        // GET: useraccounts/Edit/5
        public async Task<IActionResult> Edit()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var useraccounts = await _context.usersaccounts.FindAsync(id);
            return View(useraccounts);
        }

        // POST: useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,pass,role,RegistDate")] usersaccounts useraccounts)
        {
         
                {
                    _context.Update(useraccounts);
                    await _context.SaveChangesAsync();
                }
               
                return RedirectToAction(nameof(login));
           
        }

        // GET: useraccounts/Delete/5
        

        private bool useraccountsExists(int id)
        {
          return (_context.usersaccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
