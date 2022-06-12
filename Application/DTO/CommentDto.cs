using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CommentDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }
    }
    public class GetCommentDto
    {
        public string User { get; set; }
        public string Book { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
