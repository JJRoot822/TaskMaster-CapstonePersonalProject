using TaskMaster.Model.Domain.ProjectData;
using TaskMaster.Services;

namespace TaskMaster.Model.API.ProjectData;

public class APIDocument
{
    public int DocumentId { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public byte[] DocumentData { get; set; }
    public int UserId { get; set; }
public int ProjectId { get; set; }

    public async Document ToDomainDocument()
    {
        var document = new Document();
        document.DocumentId = DocumentId;
        document.Name = Name;
        document.Details = Details;
        document.ProjectId = ProjectId;
        document.UserId = UserId;
        document.Path = await  DocumentService.SaveDocument();

        return document;
    }
}
