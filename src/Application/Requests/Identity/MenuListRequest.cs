using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests.Identity
{
    public class MenuListRequest
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuNameNepali { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
    }
}
