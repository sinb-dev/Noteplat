using Avalonia.Controls;

namespace Noteplat.Views;

public partial class MainWindow : Window
{
    static MainWindow Instance;
    public MainWindow()
    {
        InitializeComponent();
        if (Instance == null)
            Instance = this;
    }

    public static MainWindow GetMainWindow() => Instance;
        
}
