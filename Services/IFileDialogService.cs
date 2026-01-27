namespace PersonRecord.Services
{
    public interface IFileDialogService
    {
        string? OpenFile(string filter);
        string? SaveFile(string filter, string defaultFileName = "");
    }
}