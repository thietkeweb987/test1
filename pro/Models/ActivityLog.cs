using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pro.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string PairKey { get; set; } // Dùng để định danh cặp Mentor-Intern (v dụ: "milan-pair")
        public string Date { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string InternComment { get; set; }
    }
}