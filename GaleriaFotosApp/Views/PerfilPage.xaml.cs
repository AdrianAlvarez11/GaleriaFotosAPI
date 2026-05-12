using GaleriaFotosMaui.ViewModels;

namespace GaleriaFotosMaui.Views;

public partial class PerfilPage : ContentPage
{
    private readonly UsersViewModel _vm;

    public PerfilPage(UsersViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.CargarPerfil();
    }
}