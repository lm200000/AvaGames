using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CrossGames.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private object _currentViewModel;
    public MainViewModel()
    {
        _currentViewModel = new Page2048ViewModel();
    }
    [RelayCommand]
    private void _navigate(object? parameter)
    {
        if (parameter is null) return;
        var pageName = parameter.ToString();
        switch (pageName)
        {
            case "2048":
                CurrentViewModel = new Page2048ViewModel();
                break;
            case "Tetris":
                CurrentViewModel = new PageTetrisViewModel();
                break;
            default:
                break;
        }
    }
}
