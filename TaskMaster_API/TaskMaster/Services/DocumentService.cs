using TaskMaster.Model.API.ProjectData;

namespace TaskMaster.Services;

public class DocumentService
{
    public static async string SaveDocument(APIDocument document)
    {
        if (document == null)
        {
            return "";
        }

        if (document.DocumentData == null)
        {
            return "";
        }

        string uniqueFileName = string.Format("{0}.{1}", Guid.NewGuid, document.FileExtension);
        string directoryPath = "~/Docs";

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = Path.Combine(directoryPath, uniqueFileName);

        try
        {
            await File.WriteAllBytesAsync(filePath, document.DocumentData);

            return filePath;
        }
        catch (Exception)
        {
            return "";
        }
    }
}
