using RestWithASPNETUdemy.Data.DTO;

namespace RestWithASPNETUdemy.Business;

public interface IFileService
{
    public byte[] GetFile(string filename);
    
    public Task<FileDetailsDTO> SaveFileToDisk(IFormFile file);
    
    public Task<List<FileDetailsDTO>> SaveFilesToDisk(IList<IFormFile> file);
}