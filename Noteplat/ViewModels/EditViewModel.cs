using Noteplat.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteplat.ViewModels;

public class EditViewModel : ViewModelBase
{
    TextDocument _textDocument = new TextDocument();
    public TextDocument TextDocument
    {
        get => _textDocument;
        set => this.RaiseAndSetIfChanged(ref _textDocument, value, nameof(TextDocument));
    }

    public EditViewModel()
    {
        TextDocument = new TextDocument();
    }

    public void LoadCommand()
    {

    }
    public void SaveCommand()
    {

    }
    public void SaveAsCommand()
    {

    }
    public void SettingsCommand()
    {
        MainViewModel.Instance.ViewSettings();
    }
}
