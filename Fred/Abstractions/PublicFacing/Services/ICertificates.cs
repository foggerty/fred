namespace Fred.Abstractions.PublicFacing.Services;

public interface ICertificates
{
    public bool CertificateExists(string store, string name);
}