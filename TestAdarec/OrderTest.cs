using Adarec.Application.Services;
using Adarec.Application.ServicesImpl;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestAdarec
{
    public class OrderTest
    {
        private adarecContext _context; //Contexto de la base de datos para pruebas
        private IOrderService _orderService; //Servicio de comentarios para pruebas

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<adarecContext>()
                .UseSqlServer("Data Source=LAPTOP-ALEXIS\\SQLEXPRESS;Initial Catalog=DesarrolloI;Integrated Security=True;Encrypt=True; TrustServerCertificate=True")
                .Options;

            _context = new adarecContext(options);
            _orderService = new OrderServiceImpl(_context); //Inicializa el servicio con el contexto
        }

        [Test]
        public async Task TestOrderDetailById()
        {
            var result = await _orderService.GetOrderDetailByIdAsync(1);
            Console.WriteLine($"Resultado: {JsonConvert.SerializeObject(result)}");

            Assert.Pass();
        }

        [Test]
        public async Task TestTicketCountByStatus()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int? technicianId = null; // O prueba con un ID válido

            var result = await _orderService.GetTicketCountByStatusAsync(year, month, technicianId);
            Console.WriteLine($"Resultado: {JsonConvert.SerializeObject(result)}");
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose(); //Libera recursos del contexto después de cada prueba
        }
    }
}
