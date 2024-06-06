using Fiap.Clientes.Domain.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        

        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteService _clienteService;

        public ClientesController(ILogger<ClientesController> logger,
            IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        [HttpPost]
       
        public ActionResult Post([FromBody] ClienteInput client)
        {
            var result = _clienteService.Save(client);
            return StatusCode(201, result);
        }
    }
}
