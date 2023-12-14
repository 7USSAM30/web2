using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string descr { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string discount { get; set; }
        public string category { get; set; }
        public string imagefilename { get; set; }
    }
}
 