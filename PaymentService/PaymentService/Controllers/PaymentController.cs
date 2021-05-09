using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.Core.Commands.Payment;
using System.Threading.Tasks;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentResult>> ExecutePayment([FromBody] PaymentCommand paymentCommand)
        {
            return await _mediator.Send(paymentCommand);
        }
    }
}
