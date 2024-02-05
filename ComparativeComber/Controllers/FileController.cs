using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
// ... other usings

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<FileController> _logger;

    public FileController(IFileStorageService fileStorageService, ILogger<FileController> logger)
    {
        _fileStorageService = fileStorageService;
        _logger = logger;
    }

    // File: FileController.cs

    [HttpPost]
    public async Task<IActionResult> UploadFiles([FromForm] List<IFormFile> files, [FromForm] string containerName)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError($"Invalid Model State: {JsonConvert.SerializeObject(ModelState)}");
            return BadRequest(ModelState);
        }

        //  _logger.LogInformation("Entered UploadFiles method.");

        if (files == null || files.All(f => f.Length == 0))
        {
            _logger.LogWarning("Upload failed due to empty or null files.");
            return BadRequest("Files are not present or empty.");
        }

        if (string.IsNullOrEmpty(containerName))
        {
            _logger.LogWarning("Container name is not provided.");
            return BadRequest("Container name is required.");
        }

        List<object> uploadedFiles = new List<object>();

        foreach (var file in files)
        {
            //_logger.LogInformation($"File Name: {file.FileName}");
            // _logger.LogInformation($"File Size: {file.Length}");

            try
            {
                //_logger.LogInformation($"File MIME type: {file.ContentType}");

                // _logger.LogInformation($"Uploading to container: {containerName}");
                var fileUri = await _fileStorageService.UploadFileAsync(file, containerName);

                // _logger.LogInformation($"File successfully uploaded, URI: {fileUri}");
                uploadedFiles.Add(new { FileUri = fileUri });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while uploading the file: {file.FileName}");
            }
        }

        if (uploadedFiles.Count > 0)
        {
            return Ok(new { UploadedFiles = uploadedFiles });
        }
        else
        {
            return BadRequest("No files were uploaded.");
        }
    }


    [HttpGet]
    public async Task<IActionResult> FetchFile(string fileUrl)
    {
        if (string.IsNullOrWhiteSpace(fileUrl))
        {
           // _logger.LogWarning("File URL is not provided.");
            return BadRequest("File URL is required.");
        }

      //  _logger.LogInformation($"Entered FetchFile method. Fetching file from URL {fileUrl}.");

        try
        {
            var (fileStream, metadata) = await _fileStorageService.FetchFileAsync(fileUrl);

            if (fileStream == null)
            {
                _logger.LogWarning($"File not found for URL {fileUrl}.");
                return NotFound("File not found.");
            }

            //_logger.LogInformation($"File successfully fetched from URL {fileUrl}.");

            // Convert the file stream to byte array or base64 string
            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                var base64File = Convert.ToBase64String(fileBytes);

                // Create a response object with the base64-encoded file and metadata
                var fileResponse = new
                {
                    FileData = base64File,  // base64-encoded file data
                    Metadata = metadata
                };

                return Ok(fileResponse);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching the file from URL {fileUrl}.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    private string GetMimeType(string fileExtension)
    {
        var mimeTypes = new Dictionary<string, string>
        {
            { ".txt", "text/plain" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/vnd.ms-word" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" }
            // ... other MIME types
        };

        return mimeTypes.TryGetValue(fileExtension, out var mimeType) ? mimeType : "application/octet-stream";
    }
}

public class FileResponse
{
    public Stream FileStream { get; set; }
    public IDictionary<string, string> Metadata { get; set; }
}
