using System;
using System.Collections.Generic;
using System.Text;

namespace UrlClicks.Infrastructure.Models
{
    public class ActivityQueueModel
    {
        public Guid ModuleClickId { get; set; }
        public List<Guid> ClickIds { get; set; }
    }
    public class ActivityResponseModel
    {
        public Guid ActivityId { get; set; }
        public Guid ClickId { get; set; }
    }
}
