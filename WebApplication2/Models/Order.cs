using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Не определен";

        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}