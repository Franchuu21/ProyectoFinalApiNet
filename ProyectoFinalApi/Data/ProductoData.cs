using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ProyectoFinalApi.Models;
using System.Threading.Tasks;

namespace ProyectoFinalApi.Data
{
    public class ProductoData
    {
        // Delete
        public async void DeleteProducto(int productoId)
        {
            //el using en una sola linea nos marca que dura todo el metodo y cuando termina, cierra la conexion
            using var con = new SqlConnection(ConnectionString.Value);
            int filasAfectadas = await con.ExecuteAsync("Delete from Productos where Id=@ProductoId", new { ProductoId=productoId });
        }

        //  Obtener un producto 
        public async Task<Producto> GetProducto(int productoId)
        {
            using var con = new SqlConnection(ConnectionString.Value);
            return await con.QueryFirstAsync<Producto>("Select * from Productos where Id=@ProductoId", new { ProductoId = productoId });
        }

        //  Obtener todos los productos
        public async Task<IEnumerable<Producto>> GetProductos()
        {
            using var con = new SqlConnection(ConnectionString.Value);
            return await con.QueryAsync<Producto>("Select * from Productos");
        }

        //  Insert
        public async Task<int> InsertProducto(Producto producto)
        {
            using var con = new SqlConnection(ConnectionString.Value);
            int filasAfectadas = await con.ExecuteAsync("Insert into Productos values (@Nombre,@Descripcion,@FechaVencimiento, @Precio)"
                , new { producto.Nombre, producto.Descripcion, producto.FechaVencimiento, producto.Precio });
            return filasAfectadas;
        }

        // Update
        public async Task<int> UpdateProducto(Producto producto)
        {
            using var con = new SqlConnection(ConnectionString.Value);
            int filasAfectadas = await con.ExecuteAsync("Update Productos Set Nombre=@Nombre, Descripcion=@Descripcion, " +
                "FechaVencimiento=@FechaVencimiento, Precio=@Precio"
                , new { producto.Nombre, producto.Descripcion, producto.FechaVencimiento, producto.Precio });
            return filasAfectadas;
        }

        // Obtener por rango fechas
        public async Task<IEnumerable<Producto>> GetByFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            using var con = new SqlConnection(ConnectionString.Value);
            return await con.QueryAsync<Producto>("Select * from Productos Where FechaVencimiento " +
                "between @FechaDesde and @FechaHasta",new { FechaDesde=fechaDesde, FechaHasta=fechaHasta});

        } 
    }
}
