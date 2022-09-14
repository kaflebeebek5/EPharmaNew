using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests.Identity
{
   public class MenuRoleRequest
    {
        public string RoleId { get; set; }
        public int? MenuId { get; set; }
        public int Id { get; set; }
        public int MenuListId { get; set; }
        public string MenuList { get; set; }
        public string MenuName { get; set; }
        public string Path { get; set; }
        public string ParentItem { get; set; }
        public bool IsChecked { get; set; }
    }
}
