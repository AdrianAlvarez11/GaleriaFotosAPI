using GaleriaFotosMaui.ViewModels;

namespace GaleriaFotosMaui.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(UsersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}