using AutoMapper;
using Fiap.Clientes.Domain.Clientes;
using Fiap.Clientes.Write.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<ClienteEntity, ClienteInput>().ReverseMap();
});

builder.Services.AddMemoryCache();


builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
builder.Services.AddScoped<IClientRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteFactory, ClienteFactory>();
builder.Services.AddScoped<IClienteService, ClienteService>();

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
