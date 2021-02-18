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
            int filasAfectadas = await con.ExecuteAsync("Delete from Productos where Id=@productId",new { productoId });
        }

        //  Obtener un producto 
        public async Task<Producto> GetProducto(int productoId)
        {
            using var con = new SqlConnection(ConnectionString.Value);
            return await con.QueryFirstAsync<Producto>($"Select * from Productos where Id=@productId", new { productoId });
        }

        //  Obtener todos los productos
        public async Task<IEnumerable<Producto>> GetProductos()
        {
            using var con = new SqlConnection(ConnectionString.Value);
            return await con.QueryAsync<Producto>($"Select * from Productos");
        }

        //  Insert
        public async void InsertProducto(Producto producto)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString.Value);
                con.Open();
                var transaction = con.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Name", product.Name);
                    param.Add("@Quantity", product.Quantity);
                    param.Add("@Color", product.Color);
                    param.Add("@Price", product.Price);
                    param.Add("@ProductCode", product.ProductCode);
                    var result = con.Execute("Usp_Insert_Product", param, transaction, 0, CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update
        public void UpdateProduct(Product product)
        {
            using var con = new SqlConnection(ShareConnectionString.Value);
            con.Open();
            var transaction = con.BeginTransaction();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Name", product.Name);
                param.Add("@Quantity", product.Quantity);
                param.Add("@Color", product.Color);
                param.Add("@Price", product.Price);
                param.Add("@ProductCode", product.ProductCode);
                param.Add("@ProductId", product.ProductId);
                var result = con.Execute("Usp_Update_Product", param, transaction, 0, CommandType.StoredProcedure);
                if (result > 0)
                {
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        // Check Product Exists
        public bool CheckProductExists(int productId)
        {
            using var con = new SqlConnection(ShareConnectionString.Value);
            var param = new DynamicParameters();
            param.Add("@ProductId", productId);
            var result = con.Query<bool>("Usp_CheckProductExists", param, null, false, 0, CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }
    }
}
