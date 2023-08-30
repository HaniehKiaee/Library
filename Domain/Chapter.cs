using Domain.Core;
using System;

namespace Domain
{
    public class Chapter : Entity<int>
    {
       // public int Id { get; set; }
        public string Title { get; set; }
        public int Total_pages { get; set; }
        public string Text { get; set; }
        public int BookId { get; set; }
    }
}
