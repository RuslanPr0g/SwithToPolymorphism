public class FileSaverNeedsToBeRefactored_SwitchVersion
{
    private readonly IFilesRepository repository;
    private readonly FileSystemSavingOptions savingOptions;

    public FileSaverNeedsToBeRefactored_SwitchVersion(IFilesRepository repository, FileSystemSavingOptions savingOptions)
    {
        this.repository = repository;
        this.savingOptions = savingOptions;
    }

    public async ValueTask<bool> StoreFile(string fileName, byte[] content, StorageOption option, string? folderName = null)
    {
        switch (option)
        {
            case StorageOption.Database:
                bool saveSucceeded = await repository.SaveAsync(fileName, content);
                return saveSucceeded;
            case StorageOption.FileSystem:
                bool missingFolderName = string.IsNullOrEmpty(folderName);
                if (missingFolderName) return false;

                try
                {
                    // do some file stiuff using FileSystemSavingOptions
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
    }
}

public class FileSaverIsRefactored_PolymorphicVersion
{
    private readonly IFilesRepository repository;
    private readonly FileSystemSavingOptions savingOptions;

    public FileSaverIsRefactored_PolymorphicVersion(IFilesRepository repository, FileSystemSavingOptions savingOptions)
    {
        this.repository = repository;
        this.savingOptions = savingOptions;
    }

    public async ValueTask<bool> StoreFile(DatabaseContext context)
        => await repository.SaveAsync(context.FileName, context.Content);

    public async ValueTask<bool> StoreFile(FileSystenContext context)
    {
        try
        {
            // do some file stiuff using FileSystemSavingOptions
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // or some other logging statement
            throw;
        }
    }
}

public record DatabaseContext(string FileName, byte[] Content, string FolderName);
public record FileSystenContext(string FileName, byte[] Content);