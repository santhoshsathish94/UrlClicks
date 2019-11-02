using System;
using UrlClicks.Domain.Enums;

namespace UrlClicks.Domain.Models
{
    public class UrlClick
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ModuleType Type { get; set; }
        public Guid ModuleClickId { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
