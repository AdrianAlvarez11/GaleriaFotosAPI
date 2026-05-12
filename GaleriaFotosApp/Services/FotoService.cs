using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static GaleriaFotosApp.Models.DTOs.FotoDTOs;

namespace GaleriaFotosApp.Services
{
    public class FotoService
    {
        private readonly HttpClient client;

        public FotoService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<int?> SubirFoto(string filepath)
        {
            var imgBase64 = Convert.ToBase64String(File.ReadAllBytes(filepath));
            SubirFotoRequest req = new SubirFotoRequest()
            {
                ImagenBase64 = imgBase64,
            };
            var response = await client.PostAsJsonAsync("/fotos", req);

            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadFromJsonAsync<SubirFotoResponse>();
                if (resp != null)
                {
                    return resp.Id;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public async Task<string?> TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory,photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();

                    using FileStream localFileStream =File.OpenWrite(localFilePath); //estas lineas comentadas son para guardar en el dispositivo
                    await sourceStream.CopyToAsync(localFileStream);

                    byte[] buffer = new byte[sourceStream.Length];
                    sourceStream.ReadExactly(buffer, 0, buffer.Length);

                    var base64 = Convert.ToBase64String(buffer);

                    await SubirFoto(base64);

                    return localFilePath;
                }
            }
            return null;
        }
    }
}
