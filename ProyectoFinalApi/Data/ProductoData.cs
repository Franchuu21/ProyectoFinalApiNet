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
            //el using en una sola linea nos marca que dura todo el metodo y cuando termina, cierra la conexio
            using var con = new SqlConnection(ConnectionString.Value);
            con.Open();
            int filasAfectadas = await con.ExecuteAsync("Delete from Productos where Id=@productId", new { productoId });
        }

        //  Obtener un producto 
        public async Task<Producto> GetProducto(int productoId)
        {
            using var con = new SqlConnection(ConnectionString.Value);
            con.Open();
            return await con.QueryFirstAsync<Producto>("Select * from Productos where Id=@productId", new { productoId });
        }

        //  Obtener todos los productos
        public async Task<IEnumerable<Producto>> GetProductos()
        {
            using var con = new SqlConnection(ConnectionString.Value);
            con.Open();
            return await con.QueryAsync<Producto>("Select * from Productos");
        }

        //  Insert
        public async void InsertProducto(Producto producto)
        {
            //el using es que para cuando termina el metodo se hace DISPOSE y cierre de la conexion
            using var con = new SqlConnection(ConnectionString.Value);
            con.Open();
            int filasAfectadas = await con.ExecuteAsync("Insert into Productos values (@Nombre,@Descripcion,@FechaVencimiento)"
                , new { producto.Nombre, producto.Descripcion, producto.FechaVencimiento });
        }

        // Update
        public async void UpdateProducto(Producto producto)
        {
            //el using es que para cuando termina el metodo se hace DISPOSE y cierre de la conexion
            using var con = new SqlConnection(ConnectionString.Value);
            con.Open();
            int filasAfectadas = await con.ExecuteAsync("Update Productos Set Nombre=@Nombre, Descripcion=@Descripcion, " +
                "FechaVencimiento=@FechaVencimiento"
                , new { producto.Nombre, producto.Descripcion, producto.FechaVencimiento });
        }

        // Check Product Exists
        public async Task<IEnumerable<Producto>> GetByFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            //el using es que para cuando termina el metodo se hace DISPOSE y cierre de la conexion
            using var con = new SqlConnection(ConnectionString.Value);
            con.Open();

        } 
    }
}
