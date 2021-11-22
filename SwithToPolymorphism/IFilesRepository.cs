public interface IFilesRepository
{
    Task<bool> SaveAsync(string fileName, byte[] content);
    Task<ValueTask<bool>> SaveAsync(object fileName);
    Task<ValueTask<bool>> SaveAsync(object fileName, object content);
}