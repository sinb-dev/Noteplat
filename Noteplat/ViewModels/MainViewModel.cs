using Avalonia.Platform.Storage;
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

    TextDocument _textDocument = new TextDocument();
    public TextDocument TextDocument
    {
        get => _textDocument;
        set => this.RaiseAndSetIfChanged(ref _textDocument, value, nameof(TextDocument));
    }
    public MainViewModel(IRepository repository) : base(repository)
    {
        LoadCommand = ReactiveCommand.CreateFromTask(loadCommand);
        SaveAsCommand = ReactiveCommand.CreateFromTask(saveAsCommand);
    }

    async Task loadCommand()
    {
        string path = await _repository.PickFile();
        if (File.Exists(path))
        {
            TextDocument = new TextDocument()
            {
                Filename = path,
                Text = File.ReadAllText(path)
            };
            SaveCommand = ReactiveCommand.Create(saveCommand);
        }
    }
    void saveCommand()
    {
        _repository.Save(_textDocument.Filename, _textDocument.Text);
    }

    async Task saveAsCommand()
    {
        TextDocument.Filename = await _repository.PickFile();
        _repository.Save(_textDocument.Filename, _textDocument.Text);
    }

    
}
