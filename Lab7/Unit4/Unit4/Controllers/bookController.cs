using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Unit4.Models;

namespace Unit4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class bookController : ControllerBase
    {
        // getting all book search catagory
        [HttpGet("{cat}")]
        public IEnumerable<book> Get(int cat)
        {
            List<book> li = new List<book>();
            //  SqlConnection conn1 = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=mynewdb;Integrated Security=True;Pooling=False");
            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("Unit4Context");
            SqlConnection conn1 = new SqlConnection(conStr);
            string sql;
            sql = "SELECT * FROM book where category ='" + cat + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                li.Add(new book
                {
                    title = (string)reader["title"],
                });

            }

            reader.Close();
            conn1.Close();
            return li;
        }

       


    }

}

