using Microsoft.Win32;

namespace PersonRecord.FileReader
{
    public class FileDialogService : IFileDialogService
    {
        public string? OpenFile(string filter)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = filter;

            if (dialog.ShowDialog() == true)
                return dialog.FileName;

            return null;
        }
    }
}
