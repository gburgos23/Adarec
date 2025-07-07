using Adarec.Application.Services;
using Adarec.Application.ServicesImpl;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestAdarec
{
    public class CommentsTest
    {
        private adarecContext _context; //Contexto de la base de datos para pruebas
        private ICommentService _commentService; //Servicio de comentarios para pruebas

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<adarecContext>()
                .UseSqlServer("Data Source=LAPTOP-ALEXIS\\SQLEXPRESS;Initial Catalog=DesarrolloI;Integrated Security=True;Encrypt=True; TrustServerCertificate=True")
                .Options;

            _context = new adarecContext(options);
            _commentService = new CommentServiceImpl(_context); //Inicializa el servicio con el contexto
        }

        [Test]
        public async Task Test()
        {
            var result = await _commentService.ListCommentsByOrderAsync();
            Console.WriteLine($"Resultado: {JsonConvert.SerializeObject(result)}");

            Assert.Pass();
        }


        [TearDown]
        public void Cleanup()
        {
            _context.Dispose(); //Libera recursos del contexto después de cada prueba
        }
    }
}
