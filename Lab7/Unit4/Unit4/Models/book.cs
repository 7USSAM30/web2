using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Unit4.Models
{
    public class book
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string info { get; set; }
        public int price { get; set; }
        public string discount { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime pubdate { get; set; }
        public int category { get; set; }
        public int bookquantity { get; set; }
        public string imgfile { get; set; }
    }

}
