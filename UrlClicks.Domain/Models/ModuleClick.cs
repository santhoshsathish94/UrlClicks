using System;

namespace UrlClicks.Domain.Models
{
    public class ModuleClick
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int UniqueClicks { get; set; }
        public int TotalClicks { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
