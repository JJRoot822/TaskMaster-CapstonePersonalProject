using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TaskMaster.Model.API.UserData;

namespace TaskMaster.Model.Domain.UserData;

public class UserRole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserRoleId { set; get; }

    [Required]
    public string RoleName { set; get; }

    public string RoleDetails { get; set; }

    public List<User> Users { get; set; }
}
