using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BookController.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(IFormFile file_pic, [FromForm] string title, [FromForm] string description, [FromForm] int price, [FromForm] int cata)
        {
            var ti = title;
            var de = description;
            var pr = price;
            var ca = cata;
            var im = "";
            if (file_pic != null)
            {
                string filename = file_pic.FileName;
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { file_pic.CopyToAsync(filestream); }
                im = "http://127.0.0.1:5220/images/" + filename;
            }

            SqlConnection conn1 = new SqlConnection("Data Source =.\\sqlexpress; Initial Catalog = web2; Integrated Security = True");
            string sql;
            sql = "insert into book1 (title,description, price, cata, image)  values  ('" + ti + "','" + de + "','" + pr + "' ,'" + ca + "' ,'" + im + "'  )";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            comm.ExecuteNonQuery();
            conn1.Close();
            return Ok("Book Sucessfully Added");
        }

    
                [HttpGet]
                public IEnumerable<Book> Get()
                {
                    List<Book> li = new List<Book>();
                    SqlConnection conn1 = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=web2;Integrated Security=True;Pooling=False");

                    string sql;
                    sql = "SELECT * FROM book1 ";
                    SqlCommand comm = new SqlCommand(sql, conn1);
                    conn1.Open();
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        li.Add(new Book
                        {
                            title = (string)reader["title"],
                            image = (string)reader["image"],
                            price = (int)reader["price"],
                        });

                    }

                    reader.Close();
                    conn1.Close();
                    return li;
                }
            }

        }


    





