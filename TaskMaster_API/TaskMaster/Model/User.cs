using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace TaskMaster.Model;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { set; get; }

    [Required]
    public string FirstName { set; get; }

    [Required]
    public string LastName { set; get; }

    [Required]
    public string Username { set; get; }

    [Required]
    public string Email { set; get; }
    
    public string ProfilePicturePath { set; get; }
    [Required]
    public string Password { set; get; }

    [Required]
    [ForeignKey("UserRole")]
    public int UserRoleId { get; set; }
    public UserRole Role { set; get; }
}
