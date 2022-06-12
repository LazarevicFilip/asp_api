using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Recipient { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public decimal SumPrice { get; set; }
        public User User { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
 
}
