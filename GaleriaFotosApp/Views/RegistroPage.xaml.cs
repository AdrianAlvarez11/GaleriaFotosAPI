using GaleriaFotosMaui.ViewModels;

namespace GaleriaFotosMaui.Views;

public partial class RegistroPage : ContentPage
{
    public RegistroPage(UsersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}