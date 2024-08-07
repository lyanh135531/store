using Application.Configs;
using AutoMapper;
using Domain.Files.Entities;
using Domain.Files.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Application.Core.Services;

public class FileService(
    IHostEnvironment hostEnvironment,
    IFileEntryRepository fileEntryRepository,
    IFileEntryCollectionRepository fileEntryCollectionRepository,
    IMapper mapper,
    IOptions<FileConfig> options) : IFileService
{
    private readonly FileConfig _fileConfig = options.Value;

    public async Task<Guid> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file is null)
        {
            throw new Exception("File is null");
        }

        var path = CreateIfDoesNotExistPath();

        ValidateFileExtension(file);

        var fileEntry = mapper.Map<IFormFile, FileEntry>(file);
        await fileEntryRepository.AddAsync(fileEntry, true);

        var fileFullPath = ConstructFilePath(fileEntry.Id, file, path);

        await SaveFileAsync(file, fileFullPath, cancellationToken);

        return fileEntry.Id;
    }

    private string CreateIfDoesNotExistPath()
    {
        var path = Path.Combine(hostEnvironment.ContentRootPath, "UploadFiles");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return path;
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

    public async Task<Guid> UploadMultiFileAsync(List<IFormFile> files, CancellationToken cancellationToken = default)
    {
        var fileEntryCollection = await AddFileEntryCollection(files);

        var path = CreateIfDoesNotExistPath();

        foreach (var file in files)
        {
            ValidateFileExtension(file);

            var fileEntryId = GetFileEntryId(file, fileEntryCollection);

            var fileFullPath = ConstructFilePath(fileEntryId, file, path);

            await SaveFileAsync(file, fileFullPath, cancellationToken);
        }

        return fileEntryCollection.Id;
    }

    private void ValidateFileExtension(IFormFile file)
    {
        var ext = Path.GetExtension(file.FileName);
        if (!_fileConfig.AllowExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException($"Only {string.Join(", ", _fileConfig.AllowExtensions)} are allowed.");
        }
    }

    private string ConstructFilePath(Guid fileEntryId, IFormFile file, string path)
    {
        var ext = Path.GetExtension(file.FileName);
        var fileName = $"{fileEntryId}{ext}";
        return Path.Combine(path, fileName);
    }

    private Guid GetFileEntryId(IFormFile file, FileEntryCollection fileEntryCollection)
    {
        var fileEntryId = fileEntryCollection.FileEntries
            .Where(x => x.FileName == file.FileName)
            .Select(x => x.Id)
            .FirstOrDefault();

        if (fileEntryId == Guid.Empty)
        {
            throw new InvalidOperationException($"File entry for '{file.FileName}' was not found.");
        }

        return fileEntryId;
    }

    private async Task<FileEntryCollection> AddFileEntryCollection(List<IFormFile> files)
    {
        var fileEntries = mapper.Map<List<IFormFile>, List<FileEntry>>(files);
        var fileEntryCollection = new FileEntryCollection
        {
            FileEntries = fileEntries
        };

        await fileEntryCollectionRepository.AddAsync(fileEntryCollection, true);
        return fileEntryCollection;
    }

    private async Task SaveFileAsync(IFormFile file, string fileFullPath, CancellationToken cancellationToken)
    {
        await using var stream = new FileStream(fileFullPath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);
    }
}