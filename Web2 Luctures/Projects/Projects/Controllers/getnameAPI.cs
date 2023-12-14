using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Projects.Models;

namespace Projects.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class getnameAPI : ControllerBase
    {
        
        [HttpGet("{role}")]
        public IEnumerable<usersaccounts> role(string role)
        {
            List<usersaccounts> li = new List<usersaccounts>();
            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("projectsContext");
            SqlConnection conn1 = new SqlConnection(conStr);
            string sql;
            sql = "SELECT * FROM usersaccounts where role ='" + role + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                li.Add(new usersaccounts
                {
                    name = (string)reader["name"],
                    pass = (string)reader["pass"],
                    role = (string)reader["role"],
                    RegistDate = (DateTime)reader["RegistDate"],
                    
                });

            }

            reader.Close();
            conn1.Close();
            return li;
        }


    }
}
