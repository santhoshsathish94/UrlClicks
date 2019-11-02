using System;
using System.Collections.Generic;
using System.Text;

namespace UrlClicks.Domain.Models.SMS
{
    public class SmsActivityClick
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string[] Urls { get; set; }
        public int UniqueClicks { get; set; }
        public int TotalClicks { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
