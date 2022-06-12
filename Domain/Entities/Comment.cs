﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment : Entity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
