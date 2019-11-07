using System;
using System.Collections.Generic;
using System.Text;

namespace UrlClicks.Infrastructure.Models.AppInsights
{
    public class AppInsightResponse
    {
        public IList<Table> tables { get; set; }
    }
    public class Column
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Table
    {
        public string name { get; set; }
        public IList<Column> columns { get; set; }
        public IList<IList<object>> rows { get; set; }
    }
}
