using GaleriaFotosApp.ViewModels;
using GaleriaFotosMaui.ViewModels;

namespace GaleriaFotosMaui.Views;

public partial class SubirFotoPage : ContentPage
{
    public SubirFotoPage(FotosViewmodel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}