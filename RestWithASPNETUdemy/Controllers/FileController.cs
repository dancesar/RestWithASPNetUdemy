using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.DTO;

namespace RestWithASPNETUdemy.Controllers;

[ApiVersion("1")]
[ApiController]
[Authorize("Bearer")]
[Route("api/[controller]/v{version:apiVersion}")]
public class FileController
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [HttpGet("downloadFile/{fileName}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType((200), Type = typeof(byte[]))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [Produces("application/octet-stream")]
    public async Task<IActionResult> GetFileAsync(string fileName)
    {
        byte[] buffer = _fileService.GetFile(fileName);
        if (buffer != null)
        {
            HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".","")}";
            HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
            await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
        return new ContentResult();
    }

    [HttpPost("uploadFile")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType((200), Type = typeof(FileDetailsDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [Produces("application/json")]
    public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
    {
        FileDetailsDTO detail = await _fileService.SaveFileToDisk(file);
        return new OkObjectResult(detail);
    }    
    
    [HttpPost("uploadMultipleFile")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType((200), Type = typeof(List<FileDetailsDTO>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [Produces("application/json")]
    public async Task<IActionResult> UploadManyFile([FromForm] List<IFormFile> files)
    {
        List<FileDetailsDTO> details = await _fileService.SaveFilesToDisk(files);
        return new OkObjectResult(details);
    }
}