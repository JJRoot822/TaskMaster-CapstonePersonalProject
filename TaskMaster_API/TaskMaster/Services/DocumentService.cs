using TaskMaster.Model.API.ProjectData;

namespace TaskMaster.Services;

public class DocumentService
{
    public static async Task<string> SaveDocument(byte[] documentData, string fileExtension)
    {
        if (documentData == null)
        {
            return "";
        }

        string uniqueFileName = string.Format("{0}.{1}", Guid.NewGuid(), fileExtension);
        string directoryPath = "~/Docs";

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = Path.Combine(directoryPath, uniqueFileName);

        try
        {
            await File.WriteAllBytesAsync(filePath, documentData);

            return filePath;
        }
        catch (Exception)
        {
            return "";
        }
    }
}
