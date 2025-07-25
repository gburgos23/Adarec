using Adarec.Application.Services;
using Adarec.Application.ServicesImpl;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//Se configura contexto de la base de datos
builder.Services.AddDbContext<adarecContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Se a�ade las interfaces de los servicios
builder.Services.AddScoped<IBrandService, BrandServiceImpl>();
builder.Services.AddScoped<IModelService, ModelServiceImpl>();
builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailServiceImpl>();
builder.Services.AddScoped<IOrderAssignmentService, OrderAssignmentServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IDeviceTypeService, DeviceTypeServiceImpl>();
builder.Services.AddScoped<ICommentService, CommentServiceImpl>();
builder.Services.AddScoped<IRoleService, RoleServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
