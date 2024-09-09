using Avalonia.Platform.Storage;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using Noteplat.Models;
using Noteplat.Views;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reactive;
using System.Threading.Tasks;
namespace Noteplat.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit>? SaveAsCommand { get; set; } = null;
    public ReactiveCommand<Unit, Unit> LoadCommand { get; set; }
    EditViewModel _editViewModel = new();
    SettingsViewModel _settingsViewModel = new();
    ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel 
    { 
        get => _currentViewModel; 
        set => this.RaiseAndSetIfChanged(ref _currentViewModel, value, nameof(CurrentViewModel)); }

    TextDocument _textDocument = new TextDocument();
    public TextDocument TextDocument
    {
        get => _textDocument;
        set => this.RaiseAndSetIfChanged(ref _textDocument, value, nameof(TextDocument));
    }
    public static MainViewModel Instance { get; private set; }
    static MainViewModel() => Instance = new();
    public MainViewModel()
    {
        Instance = this;

        _currentViewModel = _editViewModel;
        LoadCommand = ReactiveCommand.CreateFromTask(loadCommand);
        SaveCommand = ReactiveCommand.Create( () => { } );
        SaveAsCommand = ReactiveCommand.CreateFromTask(saveAsCommand);
        
    }

    async Task loadCommand()
    {
        string path = await PickFile();
        if (File.Exists(path))
        {
            TextDocument = new TextDocument()
            {
                Filename = path,
                Text = File.ReadAllText(path)
            };
        }
    }

    public void ViewSettings() => CurrentViewModel = _settingsViewModel;
    public void ViewEdit() => CurrentViewModel = _editViewModel;

    void saveCommand()
    {
        File.WriteAllText(_textDocument.Filename, _textDocument.Text);
    }

    async Task saveAsCommand()
    {
        TextDocument.Filename = await PickFile();
        File.WriteAllText(_textDocument.Filename, _textDocument.Text);
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
