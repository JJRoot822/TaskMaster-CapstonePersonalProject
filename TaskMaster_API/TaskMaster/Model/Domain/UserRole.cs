using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMaster.Model.Domain;

public class UserRole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserRoleId { set; get; }

    [Required]
    public string RoleName { set; get; }

    public string RoleDetails { get; set; }

    public List<User> Users { get; set; }

    public override string ToString() => string.Format("{0}, {1}, {2}", UserRoleId, RoleName, RoleDetails);
}
