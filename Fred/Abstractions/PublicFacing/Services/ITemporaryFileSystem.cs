namespace Fred.Abstractions.PublicFacing.Services;

// Use if you need to read/write the occasional file.
// Question is, why would an endpoint handler do so?
// A service might rely on the filesystem, but that's an implementation
// detail that our endpoint handler should not know about.
// The pattern should be that handlers consume services.
// A filesystem is not a service, it is an implementation detail
// for a (say) ITemporaryFile service.

// Ah, but even the service should prefer framework-provided services
// like a temporary filesystem, so this should be consumed by services
// that require it, and those are then consumed by handlers.
// Maybe put in some code that checks if your handler requests a certain
// services, and throws a DeveloperException if someone tries using it 
// (or others).
public interface ITemporaryFileSystem : IFredService
{
    public bool IsAvailable();

    public Task<string> ReadAsync(string fileName);

    public Task<byte[]> ReadBytesAsync(string fileName);

    public Task WriteAsync(string fileName, string contents);

    public Task WriteBytesAsync(string fileName, byte[] contents);
}