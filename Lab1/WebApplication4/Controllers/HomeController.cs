using Lab1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private object _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["xx"] = 66;
            ViewBag.yy = 77;
            return View();
        }
      
        public IActionResult ReadDB()
        {
            SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True"
                );

            String sql="";
            sql = "SELECT * FROM SS ";

            SqlCommand comm = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader reader = comm.ExecuteReader();
            string mm = "";
            while (reader.Read())
            {
                mm += " " + (int)reader["Id"] + " " + (string)reader["Name"]
                    + " " + (int)reader["UNid"]+ " - ";
                ViewData["detail"] = mm;
            }
            reader.Close();
            conn.Close();
            return View();
        }
        public IActionResult InsertDB()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InsertDB(string name, int number)
        {
            using (SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True"))
            {
                conn.Open();
                string sql = "INSERT INTO SS (Name, UNid) VALUES (@name, @number)";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    comm.Parameters.AddWithValue("@name", name);
                    comm.Parameters.AddWithValue("@number", number);
                    comm.ExecuteNonQuery();
                }
            }
            ViewData["message"] = "Successfully added";
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string fname, string lname, string email, string education, string gender, bool married)
        {
            string mg = fname+"";
            mg += lname + " ";
            mg += email + " ";
            mg += education + " ";
            mg += gender + " ";
            mg += "Married: "+ married;
            ViewData["detail"] = mg;
            return View();
        }
        public IActionResult ReadCustomerDB()
        {
            List<customer> li = new List<customer>();
            SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True;Pooling=False"
                );
            string sql;
            sql = "select * from customer";
            SqlCommand comm = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                li.Add(new customer
                {
                    name = (string)reader["name"],
                    age = (int)reader["age"],
                    id = (int)reader["Id"],
                    location = (string)reader["location"],
                    education = (string)reader["education"],
                    married = (bool)reader["married"],
                    gender = (string)reader["gender"]



                });
            }
            reader.Close();
            conn.Close();

            return View(li);
        }
        public IActionResult searchCustomer(int na)
        {
            string sql = "";
            SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True"
                );
            SqlCommand comm;
            conn.Open();
            Boolean flage = true;
            {
                sql = "select * from customer where id ='" + na + "' ";
                comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    flage = false;
                }
                if (flage == false)
                {
                    ViewData["1"] = (string)reader["name"];
                    ViewData["2"] = (int)reader["age"];
                    ViewData["3"] = (string)reader["education"];
                    ViewData["4"] = (bool)reader["married"];
                    ViewData["5"] = (string)reader["gender"];
                    ViewData["6"] = (string)reader["location"];

                }
                else
                {
                    ViewData["message"] = "no name has this id ";
                }
               
                reader.Close();
            }
            return View();
        }

        public IActionResult registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> registration([Bind("name,age,education,gender,married ,location ")] customer cust)
        {
            SqlConnection conn = new SqlConnection
                (
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True;Pooling=False"
                );
            conn.Open();
            string sql;
            Boolean flage = false;
            sql = "select * from customer where name = '" + cust.name + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                flage = true;
            }
            reader.Close();
            if (flage == true)
            {
                ViewData["message"] = "name already exists";

            }
            else
            {
                sql = "insert into customer (name,age,education,married,gender,location)  values  ('" + cust.name + "','" + cust.age +
                    "','" + cust.education + "','" + cust.married + "' ,'" + cust.gender + "' , '" + cust.location + "')";
                comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                ViewData["message"] = "Sucessfully added";
            }
            conn.Close();


            return View();
        }
        public IActionResult Edit(int? id)
        {
            customer cust = new customer();
            SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True")
                ;
            string sql = "";
            sql = "select * from customer where id ='" + id + "' ";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                cust.id = (int)reader["Id"];
                cust.name = (string)reader["name"];
                cust.age = (int)reader["age"];
                cust.education = (string)reader["education"];
                cust.gender = (string)reader["gender"];
                cust.married = (bool)reader["married"];
                cust.location = (string)reader["location"];
            }
            reader.Close();
            return View(cust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("id,name, age, education, gender, married, location")] customer cust)
        {
            SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True"
               );
            string sql = "";
            sql = "update customer  set  name = '" + cust.name + "' , age = '" + cust.age + "', education = '" + cust.education + "', gender = '"
                + cust.gender + "' ,married = '" + cust.married + "' ,location = '" + cust.location + "' where id  = '" + cust.id + "' ";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            ViewData["Message"] = "Sucessfully edited";

            return View();
        }
        public IActionResult delete(int? id)
        {
            customer cust = new customer();
            SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True")
                ;
            string sql = "";
            sql = "select * from customer where id ='" + id + "' ";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                cust.id = (int)reader["Id"];
                cust.name = (string)reader["name"];
               
            }

            reader.Close();
            return View(cust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, [Bind("id,name,age,education,gender,married,location")] customer cust)
        {
            SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True"
               );
            string sql = "";
            sql = "delete from customer where id ='" + id + "' ";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            ViewData["Message"] = "Successfully deleted";

            return View();
        }
        public IActionResult getcustomers()
        {
            List<customer> li = new List<customer>();
            SqlConnection conn = new SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True;Pooling=False"
                );
            string sql;
            sql = "select * from customer";
            SqlCommand comm = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                li.Add(new customer
                {
                    name = (string)reader["name"],
                    age = (int)reader["age"],
                    id = (int)reader["Id"],
                    location = (string)reader["location"]
                });
            }
            reader.Close();
            conn.Close();

            return View(li);
        }


        public IActionResult Privacy()
        {
            ViewBag.Categories = new List<string>
            {
                "Guitars", "Basses", "Drums"
            };
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}