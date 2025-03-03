using Avalonia.Controls;
using Avalonia.Interactivity;
using Trife.ViewModels.SubViewModels;

namespace Trife.Views.SubViews;

public partial class ViewAvaliacao : UserControl
{
    public ViewAvaliacao()
    {
        InitializeComponent();
    }

    private void PageUnload(object sender, RoutedEventArgs e)
    {
        if (DataContext is AvaliacaoViewModel viewModel)
        {
            viewModel.UnloadPage();
        }
    }
}