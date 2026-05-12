using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GaleriaFotosMaui.Services;

namespace GaleriaFotosMaui.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    private readonly AuthService authService;

    [ObservableProperty]
    private string nombreUsuario = string.Empty;

    [ObservableProperty]
    private string nombreReal = string.Empty;

    [ObservableProperty]
    private string pin = string.Empty;

    [ObservableProperty]
    private string nombreRealRegistro = string.Empty;

    [ObservableProperty]
    private string nombreUsuarioRegistro = string.Empty;

    [ObservableProperty]
    private string pinRegistro = string.Empty;

    [ObservableProperty]
    private string mensajeError = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isEnabled = true;

    [ObservableProperty]
    private string perfilNombreUsuario = string.Empty;

    [ObservableProperty]
    private string perfilNombreReal = string.Empty;

    public UsersViewModel(AuthService authService)
    {
        this.authService = authService;
    }

    [RelayCommand]
    public async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(NombreUsuario) || string.IsNullOrWhiteSpace(Pin))
        {
            MensajeError = "Por favor ingresa usuario y PIN.";
            return;
        }

        IsLoading = true;
        IsEnabled = true;
        MensajeError = string.Empty;
        try
        {
            var result = await authService.LoginAsync(NombreUsuario, Pin);
            if (result != null)
            {
                await authService.GuardarTokensAsync(result.AccessToken, result.RefreshToken, result.NombreUsuario, result.NombreReal);
                NombreUsuario = string.Empty;
                Pin = string.Empty;
                await Shell.Current.GoToAsync("//muro");
            }
            else
            {
                MensajeError = "Usuario o PIN incorrecto.";
            }
        }
        catch (Exception ex)
        {
            MensajeError = $"Error de conexión: {ex.Message}";
        }
        finally
        {
            IsEnabled = false;
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task RegistrarAsync()
    {
        if (string.IsNullOrWhiteSpace(NombreUsuarioRegistro) ||
            string.IsNullOrWhiteSpace(NombreRealRegistro) ||
            string.IsNullOrWhiteSpace(PinRegistro))
        {
            MensajeError = "Todos los campos son obligatorios.";
            return;
        }

        IsEnabled = true;
        IsLoading = true;
        MensajeError = string.Empty;
        try
        {
            var result = await authService.RegisterAsync(NombreUsuarioRegistro, NombreRealRegistro, PinRegistro);
            if (result != null)
            {
                await authService.GuardarTokensAsync(result.AccessToken, result.RefreshToken, result.NombreUsuario, result.NombreReal);
                NombreUsuarioRegistro = string.Empty;
                NombreRealRegistro = string.Empty;
                PinRegistro = string.Empty;
                await Shell.Current.GoToAsync("//muro");
            }
            else
            {
                MensajeError = "No se pudo registrar. El usuario ya existe o los datos son inválidos.";
            }
        }
        catch (Exception ex)
        {
            MensajeError = $"Error de conexión: {ex.Message}";
        }
        finally
        {
            IsEnabled = false;
            IsLoading = false;
        }
    }

    [RelayCommand]
    public void CargarPerfil()
    {
        PerfilNombreUsuario = authService.GetNombreUsuario() ?? "Desconocido";
        PerfilNombreReal = authService.GetNombreReal() ?? "Desconocido";
    }

    [RelayCommand]
    public async Task CerrarSesionAsync()
    {
        await authService.CerrarSesionAsync();
        await Shell.Current.GoToAsync("//login");
    }

    [RelayCommand]
    public async Task IrARegistroAsync()
    {
        await Shell.Current.GoToAsync("//registro");
    }

    [RelayCommand]
    public async Task IrALoginAsync()
    {
        await Shell.Current.GoToAsync("//login");
    }
}