using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Application.ViewModels
{
    public class TabulatorViewModel
    {
        public IEnumerable<dynamic> data { get; set; }
        public int last_page { get; set; }
        public int row_count { get; set; }
    }
}
