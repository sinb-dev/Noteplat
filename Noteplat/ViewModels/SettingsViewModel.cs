using Noteplat.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteplat.ViewModels;

public class SettingsViewModel : ViewModelBase
{

    public SettingsViewModel()
    {
    }

    public void CancelCommand()
    {
        MainViewModel.Instance.ViewEdit();
    }
    public void SaveCommand()
    {

    }
 
}
