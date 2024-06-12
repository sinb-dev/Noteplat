using ReactiveUI;

namespace Noteplat.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected IRepository _repository { get; set; }
    public ViewModelBase(IRepository repository)
    {
        _repository = repository;
    }
}
