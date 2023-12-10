using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Projects.Models
{
    public class users
    {

        public int Id { get; set; }

        public string name { get; set; }

        public string pass { get; set; }

        public string role { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime RegistDate { get; set; }


    }
}
