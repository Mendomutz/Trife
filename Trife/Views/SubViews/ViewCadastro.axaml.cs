using Avalonia.Controls;
using Avalonia.Interactivity;
using Trife.ViewModels.SubViewModels;

namespace Trife.Views.SubViews;

public partial class ViewCadastro : UserControl
{
    public ViewCadastro()
    {
        InitializeComponent();

        //debug da visibilidade das telas
        MainController.IsVisible = true;

        MachineController.IsVisible = false;
        MachineCadastro.IsVisible   = false;
        MachineEdit.IsVisible       = false;
        MachineDelete.IsVisible     = false;

        MachineIndicatorController.IsVisible = false;
        MachineIndicatorCadastro.IsVisible   = false;
        MachineIndicatorEdit.IsVisible       = false;
        MachineIndicatorDelete.IsVisible     = false;

        MachineCadastroInput.IsVisible = MachineCadastro.IsVisible;
        MachineEditInput.IsVisible     = MachineEdit.IsVisible;
        MachineDeleteInput.IsVisible   = MachineDelete.IsVisible;

        MachineIndicatorCadastroInput.IsVisible = MachineIndicatorCadastro.IsVisible;
        MachineIndicatorEditInput.IsVisible     = MachineIndicatorEdit.IsVisible;
        MachineIndicatorDeleteInput.IsVisible   = MachineIndicatorDelete.IsVisible;
    }

    private void PageUnload(object sender, RoutedEventArgs e)
    {
        if (DataContext is CadastroViewModel viewModel)
        {
            viewModel.UnloadPage();
        }
    }
}
