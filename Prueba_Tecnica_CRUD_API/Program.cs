using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_CRUD_API.Data;
using Prueba_Tecnica_CRUD_API.Services;

var builder = WebApplication.CreateBuilder(args);

//Agrega la instacia de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS simple para permitir cualquier origen (útil para pruebas)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("PermitirTodo");

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
