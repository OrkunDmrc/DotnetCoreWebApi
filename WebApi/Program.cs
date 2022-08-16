//entity framewok kullanmak için www.nutger.org dan "emntityFrameworkcore" ile "entityframeworkcore.inmemory"
//dotnet cli üzerinden yükleneek
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DbOperations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//bu ise database e bağlanmadan sanki oradan değer okuyormuşıuz gibi çalışmayı sağlar.
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
//bu dönüşümlerinin konfigürasyonlarını sağlar. Yani tektek sınıfları birbirine eşitlemeden hızlıca eşitler.
//dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 11.0.0
//dotnet add package AutoMapper --version 11.0.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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

using (var scope = app.Services.CreateScope()) { 
    var services = scope.ServiceProvider; 
    DataGenerator.Initialize(services);
}

app.Run();


