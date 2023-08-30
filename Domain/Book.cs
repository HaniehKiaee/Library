using Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book : Entity<int>
    {
        public string Title { get; set; }
        public int Total_pages { get; set; }
        public string Published_Date { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}
