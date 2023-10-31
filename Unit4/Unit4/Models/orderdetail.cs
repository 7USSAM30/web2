using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Unit4.Models
{
    public class orderdetail
    {
        public int Id { get; set; }
        public string booktitle { get; set; }
        public string customer { get; set; }
        public int quantity { get; set; }
        
    }



}
