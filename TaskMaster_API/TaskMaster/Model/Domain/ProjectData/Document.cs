using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TaskMaster.Model.API.ProjectData;

namespace TaskMaster.Model.Domain.ProjectData;

public class Document
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DocumentId { get; set; }

    [Required]
    public string Name { get; set; }
    
    public string Details { get; set; }

    public string Path { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("Project")]
    public int ProjectId { get; set; }

    public User User { get; set; }
    public Project Project { get; set; }
}
