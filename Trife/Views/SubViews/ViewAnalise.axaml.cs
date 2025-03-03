using Avalonia.Controls;
using Avalonia.Interactivity;
using Trife.ViewModels.SubViewModels;

namespace Trife.Views.SubViews;

public partial class ViewAnalise : UserControl
{
    public ViewAnalise()
    {
        InitializeComponent();

        EditPoint.IsVisible = true;

        StackPanelNotEditPoint.IsVisible = !EditPoint.IsVisible;
        NotEditPoint.IsVisible = !EditPoint.IsVisible;
    }

    private void PageUnload(object sender, RoutedEventArgs e)
    {
        if (DataContext is AnaliseViewModel viewModel)
        {
            viewModel.UnloadPage();
        }
    }

    private void Binding(LiveChartsCore.Kernel.Sketches.IChartView chart, System.Collections.Generic.IEnumerable<LiveChartsCore.Kernel.ChartPoint> points)
    {
    }
}
