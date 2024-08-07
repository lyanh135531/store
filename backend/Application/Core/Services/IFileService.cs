using Microsoft.AspNetCore.Http;

namespace Application.Core.Services;

public interface IFileService
{
    Task<Guid> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);

    Task DeleteFileAsync(Guid fileEntryId, CancellationToken cancellationToken = default);

    Task<Guid> UploadMultiFileAsync(List<IFormFile> files, CancellationToken cancellationToken = default);
}