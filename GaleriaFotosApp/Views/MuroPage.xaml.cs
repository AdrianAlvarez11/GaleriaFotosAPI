using GaleriaFotosApp.ViewModels;
using GaleriaFotosMaui.ViewModels;

namespace GaleriaFotosMaui.Views;

public partial class MuroPage : ContentPage
{

    public MuroPage(FotosViewmodel vm)
    {

        InitializeComponent();
        Vm = vm;
        BindingContext = vm;
    }

    public FotosViewmodel Vm { get; }

    protected async override void OnAppearing()
    {
        await Vm.DescargarFotos();
        base.OnAppearing();
    }

}