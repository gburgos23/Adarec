using Adarec.Application.Services;
using Adarec.Application.ServicesImpl;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestAdarec
{
    public class DeviceTypeTest
    {
        private adarecContext _context;
        private IDeviceTypeService _deviceTypeService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<adarecContext>()
                .UseSqlServer("Data Source=LAPTOP-ALEXIS\\SQLEXPRESS;Initial Catalog=DesarrolloI;Integrated Security=True;Encrypt=True; TrustServerCertificate=True")
                .Options;
            _context = new adarecContext(options);
            _deviceTypeService = new DeviceTypeServiceImpl(_context);
        }

        [Test]
        public async Task TestGetActiveDeviceTypes()
        {
            var result = await _deviceTypeService.GetActiveDeviceTypesAsync();
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