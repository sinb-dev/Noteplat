using Avalonia.Platform.Storage;
using Noteplat.Models;
using Noteplat.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Noteplat.ViewModels;

public class EditViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit>? SaveAsCommand { get; set; } = null;
    public ReactiveCommand<Unit, Unit> LoadCommand { get; set; }
    TextDocument _textDocument = new TextDocument();
    public TextDocument TextDocument
    {
        get => _textDocument;
        set => this.RaiseAndSetIfChanged(ref _textDocument, value, nameof(TextDocument));
    }

    public EditViewModel()
    {
        TextDocument = new TextDocument();
        LoadCommand = ReactiveCommand.CreateFromTask(loadCommand);
        SaveCommand = ReactiveCommand.Create(() => { });
        SaveAsCommand = ReactiveCommand.CreateFromTask(saveAsCommand);
    }

    public async Task loadCommand()
    {

    }
    public void saveCommand()
    {

    }
    public async Task saveAsCommand()
    {
        var file = await PickFile();
        _textDocument.Filename = file;
        File.WriteAllText(_textDocument.Filename, _textDocument.Text);

    }
    public void SettingsCommand()
    {
        MainViewModel.Instance.ViewSettings();
    }

    public async Task<string> PickFile()
    {
        var window = MainWindow.GetMainWindow() ?? throw new Exception("Unable to open file dialog");
        FilePickerSaveOptions options = new();
        var result = await window.StorageProvider.SaveFilePickerAsync(options);
        string path = "";
        if (result != null)
        {
            path = result.Path.LocalPath;
        }

        return path;
    }
}
