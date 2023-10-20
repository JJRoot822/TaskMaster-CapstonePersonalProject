using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Model;

public class UserRole
{
    [Key]
    public int UserRoleId { set; get; }

    [Required]
    public string RoleName { set; get; }
    
    public string RoleDetails { get; set; }

    public List<User> Users { get; set; }
}
