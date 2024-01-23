using MagicVill;
using MagicVill.Datos;
using MagicVill.Repositorio;
using MagicVill.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<_villaRepo>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IVillaRepositorio, VillaRepositorio>(); // el addscope permite crear servicios que se crean una vez y luego se destruyen
builder.Services.AddAutoMapper(typeof(MappingConfig));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
