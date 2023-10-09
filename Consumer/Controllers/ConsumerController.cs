using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly KafkaConsumerService _consumerService;
        public ConsumerController(KafkaConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        [HttpGet]
        public ActionResult Test()
        {
            
            return Ok(_consumerService.Consume());
        }
    }
}
