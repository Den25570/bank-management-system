using API.Exceptions;
using API.Models;
using API.Database;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly BankManagmentSystemContext _context;

        public ClientsController(ILogger<ClientsController> logger, BankManagmentSystemContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Database.Client> GetAll()
        {
            var clients = _context.Clients.OrderBy(c => c.Firstname).ToList();
            return clients;
        }

        [HttpGet]
        public Database.Client Get(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                return client;
            }
            throw new HttpResponseException(404, $"Client with id={id} does not exist");
        }

        [HttpPut]
        public Response Put([FromBody] Models.Client clientModel)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == clientModel.Id);
            if (client != null)
            {
                clientModel.ToDatabaseModel(client);
                _context.SaveChanges();
                return new Response("Client updated");
            }
            throw new HttpResponseException(404, $"Client with id={clientModel.Id} does not exist");
        }

        [HttpDelete]
        public Response Delete(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
                return new Response("Client deleted");
            }
            throw new HttpResponseException(404, $"Client with id={id} does not exist");
        }

        [HttpPost]
        public Response Post([FromBody] Models.Client clientModel)
        {
            _context.Clients.Add(clientModel.ToDatabaseModel());
            _context.SaveChanges();
            return new Response("Client updated");
        }
    }
}