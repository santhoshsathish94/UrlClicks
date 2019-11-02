using System;

namespace UrlClicks.Domain.Models.SMS
{
    public class LinkSmsActivity
    {
        public Guid UrlClickId { get; set; }
        public Guid SmsActivityClickId { get; set; }
    }
}
