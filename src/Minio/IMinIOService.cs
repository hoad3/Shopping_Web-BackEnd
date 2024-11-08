namespace Web_2.Minio;

public interface IMinIOService
{
    Task<string> UploadFileAsync(string objectName, Stream data, string contentType);
    Task<bool> DeleteFileAsync(string objectName);
    Task<string> GetFileUrl(string objectName);

}