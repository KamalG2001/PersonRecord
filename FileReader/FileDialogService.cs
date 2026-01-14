using Microsoft.Win32;

namespace PersonRecord.FileReader
{
    public class FileDialogService : IFileDialogService
    {
        public string? OpenFile(string filter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter,
                CheckFileExists = true,
                CheckPathExists = true
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public string? SaveFile(string filter, string defaultFileName = "")
        {
            var dialog = new SaveFileDialog
            {
                Filter = filter,
                FileName = defaultFileName,
                CheckPathExists = true
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}
