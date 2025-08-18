using Microsoft.AspNetCore.Mvc;
using ProvaPub.Application.Services;
using ProvaPub.Dtos;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// Simula um pagamento de pedido utilizando Strategy Pattern para métodos de pagamento.
    /// O pedido é salvo com OrderDate em UTC, mas retornado para o cliente no fuso horário do Brasil.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly OrderService _orderService;

        public Parte3Controller(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Realiza o pagamento de um pedido.
        /// </summary>
        [HttpPost("orders")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request)
        {
            if (request == null)
                return BadRequest("Dados inválidos.");

            var order = await _orderService.PayOrder(request.PaymentMethod, request.PaymentValue, request.CustomerId);

            TimeZoneInfo brazilTimeZone;
            try
            {
                brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"); 
            }

            var response = new OrderResponseDto
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                PaymentMethod = request.PaymentMethod, 
                PaymentValue = request.PaymentValue,   
                OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate, brazilTimeZone)
            };

            return Ok(response);
        }
    }
}
