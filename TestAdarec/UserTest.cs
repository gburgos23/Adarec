using Adarec.Application.Services;
using Adarec.Application.ServicesImpl;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestAdarec
{
    public class UserTest
    {
        private adarecContext _context; // Contexto de la base de datos para pruebas
        private IUserService _userService; // Servicio de usuarios para pruebas
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<adarecContext>()
                .UseSqlServer("Data Source=LAPTOP-ALEXIS\\SQLEXPRESS;Initial Catalog=DesarrolloI;Integrated Security=True;Encrypt=True; TrustServerCertificate=True")
                .Options;
            _context = new adarecContext(options);
            _userService = new UserServiceImpl(_context); // Inicializa el servicio con el contexto
        }

        [Test]
        public async Task TestTechnicianWorkload()
        {
            var userService = new UserServiceImpl(_context);
            var result = await userService.GetTechnicianWorkloadAsync();
            Console.WriteLine($"Resultado: {JsonConvert.SerializeObject(result)}");
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose(); // Libera recursos del contexto después de cada prueba
        }
    }
}
