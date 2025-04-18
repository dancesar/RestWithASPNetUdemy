using RestWithASPNETUdemy.Data.DTO;

namespace RestWithASPNETUdemy.Business.Implementations;

public class FilesServiceImplementation : IFileService
{
    private readonly string _basePath;
    private readonly IHttpContextAccessor _context;

    public FilesServiceImplementation(IHttpContextAccessor context)
    {
        _context = context;
        _basePath = Directory.GetCurrentDirectory() + "//UploadDir//";
    }

    public byte[] GetFile(string filename)
    {
        var filePath = _basePath + filename;
        return File.ReadAllBytes(filePath);
    }

    public async Task<FileDetailsDTO> SaveFileToDisk(IFormFile file)
    {
        FileDetailsDTO FileDetail = new FileDetailsDTO();
        
        var fileType = Path.GetExtension(file.FileName.ToLower());
        var baseUrl = _context.HttpContext.Request.Host;

        if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
            fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
        {
            var docName = Path.GetFileName(file.FileName);
            if (file != null && file.Length > 0)
            {
                var destination = Path.Combine(_basePath, "", docName);
                FileDetail.DocumentName = docName;
                FileDetail.DocumentType = fileType;
                FileDetail.DocumentUrl = Path.Combine(baseUrl + "/api/file/v1" + FileDetail.DocumentName);
                
                using var stream = new FileStream(destination, FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }
        
        return FileDetail;
    }

    public async Task<List<FileDetailsDTO>> SaveFilesToDisk(IList<IFormFile> files)
    {
        List<FileDetailsDTO> list = new List<FileDetailsDTO>();
        foreach (var file in files)
        {
            list.Add(await SaveFileToDisk(file));
        }
        return list;
    }
}