using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace ASP2184587.Models
{
    public class BaseModelos
    {
        public int ActualPage { get; set; }
        public int Total { get; set; }
        public int RecordsPage { get; set; }
        public RouteValueDictionary ValuesQueryString { get; set; }
    }
}