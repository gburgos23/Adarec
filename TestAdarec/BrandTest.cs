using Adarec.Application.Services;
using Adarec.Application.ServicesImpl;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestAdarec
{
    public class BrandTest
    {
        private adarecContext _context;
        private IBrandService _brandService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<adarecContext>()
                .UseSqlServer("Data Source=LAPTOP-ALEXIS\\SQLEXPRESS;Initial Catalog=DesarrolloI;Integrated Security=True;Encrypt=True; TrustServerCertificate=True")
                .Options;
            _context = new adarecContext(options);
            _brandService = new BrandServiceImpl(_context);
        }

        [Test]
        public async Task TestGetActiveBrands()
        {
            var result = await _brandService.GetActiveBrandsAsync();
            Console.WriteLine($"Resultado: {JsonConvert.SerializeObject(result)}");
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}