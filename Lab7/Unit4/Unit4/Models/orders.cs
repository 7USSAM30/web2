using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Unit4.Models
{
    public class orders
    {
        public int Id { get; set; }
        public int bookid { get; set; }
        public int custid { get; set; }
        public int quantity { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime buydate { get; set; }
    }

}
