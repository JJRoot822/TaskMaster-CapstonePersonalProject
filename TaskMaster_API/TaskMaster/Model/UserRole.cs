using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMaster.Model;

public class UserRole
{
    [Key]
    [DatabaseGenerated]
    public int UserRoleId { set; get; }

    [Required]
    public string RoleName { set; get; }
    
    public string RoleDetails { get; set; }

    public List<User> Users { get; set; }

    public string ToString() => string.Format("{0}, {1}, {2}", UserRoleId, RoleName, RoleDetails);
}
