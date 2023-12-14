using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Projects.Models
{
    public class orderdetail
    {
        public int Id { get; set; }

        public string username { get; set; }

        public int totalprice { get; set; }

        public int quantity { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime buydate { get; set; }
    }
}
