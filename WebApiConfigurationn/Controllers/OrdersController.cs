using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.Orders;

namespace WebApiConfigurationn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApiDbContext _context;
        IMapper _mapper;

        public OrdersController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var list = await _context.Orders.ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, list);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdOrder(Guid id)
        {
            var existsOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (existsOrder == null)
                return NotFound();

            var result = _mapper.Map<GetOrderDTO>(existsOrder);

            return StatusCode((int)HttpStatusCode.Found, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDTO createOrderDTO)
        {
            var order = _mapper.Map<Order>(createOrderDTO);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var existsOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (existsOrder == null)
                return NotFound();

            _context.Orders.Remove(existsOrder);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderDTO updateOrderDTO)
        {
            var existsOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (existsOrder == null)
                return NotFound();

            _mapper.Map(updateOrderDTO, existsOrder);

            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}

