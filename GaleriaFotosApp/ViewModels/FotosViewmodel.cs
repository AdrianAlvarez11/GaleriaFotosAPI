using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GaleriaFotosApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaFotosApp.ViewModels
{
    public partial class FotosViewmodel : ObservableObject
    {
        public FotosViewmodel(FotoService service)
        {
            this.service = service;
        }
        public bool IsLoading { get; set; }

        [ObservableProperty]
        private string mensaje;

        [ObservableProperty]
        private string? rutaImagen;
        private readonly FotoService service;

        [RelayCommand]
        public async Task SubirFoto()
        {
            try
            {


                if (RutaImagen != null)
                {
                    IsLoading = true;
                    Mensaje = "Cargando foto...";
                    var id = await service.SubirFoto(rutaImagen);
                    if (id == null)
                    {
                        Mensaje = "No se pudo cargar la foto";
                    }
                    else
                    {
                        Mensaje = "Fotografia cargada exitosamente";
                        RutaImagen = null;
                    }

                }
                else
                {
                    Mensaje = "Tome o seleccione un mensaje antes de subirla";
                }
            }
            catch (Exception ex)
            {
                Mensaje += ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task TomarFoto()
        {
            Mensaje = "";
            var ruta = await service.TakePhoto();

            if (ruta == null)
            {
                Mensaje = "No se ha tomado la foto";
            }
            else
            {
                RutaImagen = ruta;
            }
        }
    }
}
