using System;

namespace ProyectoFinalApi.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public decimal Precio { get; set; }
    }
}
