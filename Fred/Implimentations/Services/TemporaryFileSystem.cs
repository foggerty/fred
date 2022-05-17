using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Implimentations.Services;

public class TemporaryFileSystem : ITemporaryFileSystem
{
    public bool IsAvailable()
    {
        throw new NotImplementedException();
    }

    public Task<string> ReadAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> ReadBytesAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public Task WriteAsync(string fileName, string contents)
    {
        throw new NotImplementedException();
    }

    public Task WriteBytesAsync(string fileName, byte[] contents)
    {
        throw new NotImplementedException();
    }
}