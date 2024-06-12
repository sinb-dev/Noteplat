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
    public ReactiveCommand<Unit, Unit> LoadCommand { get; set; }

    TextDocument _textDocument = new TextDocument();
    public TextDocument TextDocument
    {
        get => _textDocument;
        set => this.RaiseAndSetIfChanged(ref _textDocument, value, nameof(TextDocument));
    }
    public MainViewModel(IRepository repository) : base(repository)
    {
        SaveCommand = ReactiveCommand.Create(saveCommand);
        LoadCommand = ReactiveCommand.CreateFromTask(loadCommand);

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
        }
    }
    void saveCommand()
    {
        //_repository.Save(_textDocument.Filename, _textDocument.Text);
        File.WriteAllText(_textDocument.Filename, _textDocument.Text);
    }

    
}
