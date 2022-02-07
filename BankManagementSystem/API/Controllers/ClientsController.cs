using API.Exceptions;
using API.Models;
using API.Database;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;

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
            var clients = _context.Clients.OrderBy(c => c.Lastname).ToList();
            return clients;
        }

        [HttpGet]
        [Route("all/passport")]
        public IEnumerable<Database.Client> GetByPassportId(string passportIdNumber)
        {
            var clients = _context.Clients.Where(c => c.PassportIdNumber.Contains(passportIdNumber)).ToList();
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
                var existingClients = _context.Clients.FirstOrDefault(c => c.PassportIdNumber == clientModel.PassportIdNumber && c.Id != clientModel.Id);
                if (existingClients == null)
                {
                    clientModel.ToDatabaseModel(client);
                    _context.SaveChanges();
                    return new Response("Клиент обновлён", true);
                }
                return new Response("Клиент с таким идентификационным номером уже существует", false);
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
                return new Response("Клиент удалён", true);
            }
            throw new HttpResponseException(404, $"Client with id={id} does not exist");
        }

        [HttpPost]
        public Response Post([FromBody] Models.Client clientModel)
        {
            var existingClients = _context.Clients.FirstOrDefault(c => c.PassportIdNumber == clientModel.PassportIdNumber);
            if (existingClients == null)
            {
                _context.Clients.Add(clientModel.ToDatabaseModel());
                _context.SaveChanges();
                return new Response("Клиент создан", true);
            }
            return new Response("Клиент с таким идентификационным номером уже существует", false);
        }
    }
}