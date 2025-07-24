using Adarec.Application.Services;
using Adarec.Application.ServicesImpl;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestAdarec
{
    public class CustomerTest
    {
        private adarecContext _context; //Contexto de la base de datos para pruebas
        private ICustomerService _customerService; //Servicio de comentarios para pruebas

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<adarecContext>()
                .UseSqlServer("Data Source=LAPTOP-ALEXIS\\SQLEXPRESS;Initial Catalog=DesarrolloI;Integrated Security=True;Encrypt=True; TrustServerCertificate=True")
                .Options;

            _context = new adarecContext(options);
            _customerService = new CustomerServiceImpl(_context); //Inicializa el servicio con el contexto
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose(); //Libera recursos del contexto después de cada prueba
        }
    }
}
