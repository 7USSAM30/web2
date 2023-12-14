using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int itemid { get; set; }
        public int userid { get; set; }
        public int Quantity { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime Buydate { get; set; }


    }

}
