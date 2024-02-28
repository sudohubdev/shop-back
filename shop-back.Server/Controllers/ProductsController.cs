using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_back.Server.Data;
using shop_back.Server.Entities;

namespace shop_back.Server.Controllers
{
    [ApiController]
    [Route("[controller]/products")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private MainContext _context;

        public ProductsController(ILogger<ProductsController> logger, MainContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<ProductEntity> Get()
        {
            return _context.Products.Include(p => p.Feedbacks).ToArray();
        }

    }
}
