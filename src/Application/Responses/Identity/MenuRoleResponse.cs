
namespace EPharma.Application.Responses.Identity
{
   public class MenuRoleResponse
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
        public string Icon { get; set; }
        public string MenuNameNepali { get; set; }
    }
}
