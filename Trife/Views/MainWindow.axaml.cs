using Avalonia.Controls;
using Trife.ViewModels;

namespace Trife.Views;

public partial class MainWindow : Window
{
    #region [Properties]

    private readonly MainViewModel _viewModel = new();

    #endregion

    #region [Constructors]

    //public MainWindow() : this();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _viewModel;

        this.Closing += Close;

        //this.AttachDevTools();
    }

    #endregion

    #region [Events]

    private void Close(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.UnloadPage();
        }
    }

    #endregion
}
