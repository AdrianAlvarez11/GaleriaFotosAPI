using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaFotosApp.Models.DTOs
{
    public class FotoDTOs
    {
        public class SubirFotoRequest
        {
            public string ImagenBase64 { get; set; } = null!;
        }

        public class FotoDto
        {
            public int Id { get; set; }
            public string NombreUsuario { get; set; } = null!;
            public string NombreArchivo { get; set; } = null!;
            public DateTime FechaSubida { get; set; }
        }

        public class SubirFotoResponse
        {
            public int Id { get; set; }
        }
    }
}
