var builder = WebApplication.CreateBuilder(args);

// Habilitar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Habilitar Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();  // Esta línea muestra /swagger
}

// Omitimos HTTPS redirection
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
