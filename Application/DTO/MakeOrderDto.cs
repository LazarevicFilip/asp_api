using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class MakeOrderDto
    {
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Recipient { get; set; }
        public IEnumerable<OrderLineDto> OrderLines { get; set; }
    }

    public class OrderLineDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public decimal BookPrice { get; set; }
        public int Quantity { get; set; }
        public int BookPublisherId { get; set; }
    }
    public class GetOrderDto
    {
        public int OrderId { get; set;}
        public string Recipient { get; set; }
        public string Adress { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<OrderLineDto> OrderLines { get; set; }
    }
}