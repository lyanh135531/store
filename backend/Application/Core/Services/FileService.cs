using Application.Configs;
using AutoMapper;
using Domain.Files.Entities;
using Domain.Files.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Application.Core.Services;

public class FileService(
    IHostEnvironment hostEnvironment,
    FileConfig fileConfig,
    IFileEntryRepository fileEntryRepository,
    IMapper mapper) : IFileService
{
    public async Task<Guid> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var allowedFileExtensions = fileConfig.AllowExtensions;
        if (file is null)
        {
            throw new Exception("File is null");
        }

        var path = Path.Combine(hostEnvironment.ContentRootPath, "UploadFiles");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var ext = Path.GetExtension(file.FileName);
        if (!allowedFileExtensions.Contains(ext))
        {
            throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
        }

        var fileEntry = mapper.Map<IFormFile, FileEntry>(file);
        fileEntry.Extension = ext;
        await fileEntryRepository.AddAsync(fileEntry, true);

        var fileName = $"{fileEntry.Id}{ext}";
        var fileFullPath = Path.Combine(path, fileName);
        await using var stream = new FileStream(fileFullPath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return fileEntry.Id;
    }

    public async Task DeleteFileAsync(Guid fileEntryId, CancellationToken cancellationToken = default)
    {
        var fileEntryQueryable = await fileEntryRepository.GetQueryableAsync();
        var fileEntry = await fileEntryQueryable.FirstOrDefaultAsync(x => x.Id == fileEntryId, cancellationToken);
        if (fileEntry is null)
        {
            throw new Exception("File entry is not found");
        }

        await fileEntryRepository.DeleteAsync(fileEntryId, true);

        var path = Path.Combine(hostEnvironment.ContentRootPath, "UploadFiles", $"{fileEntry.Id}{fileEntry.Extension}");
        
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Invalid file path");
        }
        File.Delete(path);
    }
}