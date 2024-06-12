using Avalonia.Platform.Storage;
using Noteplat.Views;
using System;
using System.IO;
using System.Threading.Tasks;
namespace Noteplat;

public class DesktopRepository : IRepository
{
    public string Load(string filename)
    {
        if (File.Exists(filename))
        {
            return File.ReadAllText(filename);
        }
        return "";
    }
    public void Save(string filename, string contents)
    {
        File.WriteAllText(filename, contents);
    }
    public async Task<string> PickFile()
    {
        var window = MainWindow.GetMainWindow() ?? throw new Exception("Unable to open file dialog");
        FilePickerOpenOptions options = new();
        options.AllowMultiple = false;
        var result = await window.StorageProvider.OpenFilePickerAsync(options);
        string path = "";
        if (result.Count == 1)
        {
            path = result[0].Path.LocalPath;
        }

        return path;
    }
}
