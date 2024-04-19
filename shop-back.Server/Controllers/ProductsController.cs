using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_back.Server.Data;
using shop_back.Server.Entities;
using shop_back.Server.Models;

namespace shop_back.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private MainContext _context;
        private readonly UserManager<IdentityUser> _user;

        public ProductsController(ILogger<ProductsController> logger, MainContext context, UserManager<IdentityUser> user)
        {
            _logger = logger;
            _context = context;
            _user = user;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetProducts")]
        public IEnumerable<ProductEntity> Get([FromQuery] int index = 0, [FromQuery] int count = 100)
        {
            return _context.Products.Include(p => p.Feedbacks).ThenInclude(f=>f.User).Skip(index).Take(count).ToArray();
        }
        //search
        [HttpGet("search", Name = "SearchProducts")]
        public async Task<IEnumerable<ProductEntity>> Search([FromQuery] string q, [FromQuery] int count = 10)
        {
            //use fuzzysharp to search, order by score and take top 10
            var products = await _context.Products.Include(p => p.Feedbacks).ToArrayAsync();
            return products.OrderByDescending(p => FuzzySharp.Fuzz.WeightedRatio(p.Name, q)).Take(count);
        }

        //get products of type, order by score
        [HttpGet("type", Name = "GetProductsOfType")]
        public IEnumerable<ProductEntity> GetOfType([FromQuery] string type, [FromQuery] int count = 10)
        {
            return from p in _context.Products.Include(p => p.Feedbacks).ToArray()
                   where p.Category == type
                   orderby p.Score descending
                   select p;
        }
        //get products of type, order by score
        [HttpGet("types", Name = "GetAllTypes")]
        public IEnumerable<string> GetAllTypes() => _context.Products.Select(p => p.Category).Distinct().ToArray();


        //create an order with all required data
        [HttpPost("order", Name = "OrderProduct")]
        public async Task<IActionResult> Order([FromBody] OrderModel orderData)
        {
            if (User?.Identity?.Name == null)
            {
                return BadRequest(new { Error = "Invalid session" });
            }
            var user = await _user.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return BadRequest(new { Error = "Invalid session" });
            }
            var userId = await _user.GetUserIdAsync(user);
            if (userId == null)
            {
                return BadRequest(new { Error = "User not found" });
            }

            var product = await _context.Products.FindAsync(orderData.ProductId);
            if (product == null)
            {
                return BadRequest(new { Error = "Product not found" });
            }
            if (product.Quantity < orderData.Quantity)
            {
                return BadRequest(new { Error = "Not enough products" });
            }
            //fill order data
            var order = new OrderEntity(orderData, product, userId);
            product.Quantity -= orderData.Quantity;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return Ok(new { Success = true });
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IEnumerable<OrderEntity>?> GetOrders()
        {
            if (User?.Identity?.Name == null)
            {
                return null;
            }
            var user = await _user.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return null;
            }
            var userId = await _user.GetUserIdAsync(user);
            if (userId == null)
            {
                return null;
            }
            return _context.Orders.Where(o => o.UserId == userId).ToArray();
        }

        //get feedbacks of product
        [HttpGet("/feedbacks/{id}", Name = "GetFeedbacks")]
        public async Task<IEnumerable<FeedbackEntity>> GetFeedbacks(int id)
        {
            return await _context.Feedbacks.Where(f => f.ProductId == id).Include(f => f.User).ToArrayAsync();
        }

        //create comment
        [HttpPost("feedback", Name = "CreateFeedback")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackModel feedbackData)
        {
            if (User?.Identity?.Name == null)
            {
                return BadRequest(new { Error = "Invalid session" });
            }
            var user = await _user.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return BadRequest(new { Error = "Invalid session" });
            }
            var userId = await _user.GetUserIdAsync(user);
            if (userId == null)
            {
                return BadRequest(new { Error = "User not found" });
            }

            var product = await _context.Products.FindAsync(feedbackData.ProductId);
            if (product == null)
            {
                return BadRequest(new { Error = "Product not found" });
            }
            if (feedbackData.Rating < 1 || feedbackData.Rating > 5)
            {
                return BadRequest(new { Error = "Invalid rating" });
            }
            //check if user already left feedback
            if (_context.Feedbacks.Any(f => f.ProductId == feedbackData.ProductId && f.UserId == userId))
            {
                return BadRequest(new { Error = "Feedback already exists" });
            }
            //fill feedback data
            var feedback = new FeedbackEntity(feedbackData, product, userId);
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return Ok(new { Success = true });
        }
    }
}
