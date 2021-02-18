using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoFinalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger;
        public ProductoController(ILogger<ProductoController> logger)
        {
            _logger = logger;
        }

        
        /// <summary>
        /// Obtener todos los productos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            
        }
    }
}
