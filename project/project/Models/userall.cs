using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class userall
    {
          {
      public int Id { get; set; }
        public string name { get; set; }

        public string password { get; set; }
        public string role { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime RegistDate { get; set; }
    }

}
}
