using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using ComparativeComber.Entities;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(IFormFile file, string containerName);
    Task<string> UploadExtractedImageAsync(ExtractedImage extractedImage, string containerName);
    Task<(Stream content, IDictionary<string, string> metadata)> FetchFileAsync(string fileUrl);  // Adjusted method signature



}

public class FileStorageService : IFileStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILogger<FileStorageService> _logger;


    public FileStorageService(BlobServiceClient blobServiceClient, ILogger<FileStorageService> logger)
    {
        _blobServiceClient = blobServiceClient;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string containerName)
    {
      //  _logger.LogInformation($"Entered UploadFileAsync method in FileStorageService.");

        try
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
          //  _logger.LogInformation($"Container URL: {blobContainerClient.Uri}");

            var createResponse = await blobContainerClient.CreateIfNotExistsAsync();
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
            {
              //  _logger.LogInformation("Container created.");
            }
            else
            {
                //_logger.LogInformation("Container exists, skipped creation.");
            }

            var blobName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
          //  _logger.LogInformation($"Blob Name: {blobName}");

            var blobClient = blobContainerClient.GetBlobClient(blobName);
           // _logger.LogInformation($"Blob URL: {blobClient.Uri}");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
             //   _logger.LogInformation($"Stream length: {stream.Length}");

                var uploadResponse = await blobClient.UploadAsync(stream, overwrite: true);
            //    _logger.LogInformation($"Upload Status: {uploadResponse.GetRawResponse().Status}, Reason: {uploadResponse.GetRawResponse().ReasonPhrase}");
            }

            return blobClient.Uri.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during file upload.");
            throw;
        }
    }
    public async Task<string> UploadExtractedImageAsync(ExtractedImage extractedImage, string containerName)
    {
       // _logger.LogInformation($"Entered UploadExtractedImageAsync method in FileStorageService.");

        try
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            //_logger.LogInformation($"Container URL: {blobContainerClient.Uri}");

            var createResponse = await blobContainerClient.CreateIfNotExistsAsync();
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
            {
              //  _logger.LogInformation("Container created.");
            }
            else
            {
              //  _logger.LogInformation("Container exists, skipped creation.");
            }

            var blobName = $"{Guid.NewGuid()}.{extractedImage.ImageFormat.Split('/')[1]}";
          //  _logger.LogInformation($"Blob Name: {blobName}");

            var blobClient = blobContainerClient.GetBlobClient(blobName);
           // _logger.LogInformation($"Blob URL: {blobClient.Uri}");

            var blobUploadOptions = new BlobUploadOptions
            {
                Metadata = new Dictionary<string, string> { { "Caption", extractedImage.Caption } }
            };

            using (var stream = new MemoryStream(extractedImage.ImageData))
            {
                var uploadResponse = await blobClient.UploadAsync(stream, blobUploadOptions);
                _logger.LogInformation($"Upload Status: {uploadResponse.GetRawResponse().Status}, Reason: {uploadResponse.GetRawResponse().ReasonPhrase}");
            }


            return blobClient.Uri.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during image upload.");
            throw;
        }
    }
    public async Task<(Stream content, IDictionary<string, string> metadata)> FetchFileAsync(string fileUrl)
    {
      //  _logger.LogInformation($"Entered FetchFileAsync method in FileStorageService. Fetching file from URL {fileUrl}.");

        try
        {
            var uri = new Uri(fileUrl);
            var containerName = uri.Segments[1].TrimEnd('/');
            var blobName = uri.Segments[2];

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync())
            {
                _logger.LogWarning($"File {fileUrl} does not exist.");
                return (null, null);
            }

            //_logger.LogInformation($"Blob URL: {blobClient.Uri}");
            BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();
            BlobProperties properties = await blobClient.GetPropertiesAsync();

            return (blobDownloadInfo.Content, properties.Metadata);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching file from URL {fileUrl}.");
            throw;
        }
    }
   

}




