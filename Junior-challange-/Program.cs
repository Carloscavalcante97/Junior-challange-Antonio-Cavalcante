using Junior_challange_.Services;
using Junior_Challenge.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem (use com cautela em produ��o)
              .AllowAnyMethod()  // Permite qualquer m�todo HTTP (GET, POST, DELETE, etc.)
              .AllowAnyHeader(); // Permite qualquer cabe�alho
    });
});

// Configura��o do banco de dados
var connectionString = builder.Configuration.GetConnectionString("AnelDatabase");
builder.Services.AddDbContext<AnelContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar o servi�o com lifetime Scoped
builder.Services.AddScoped<ServicosAnel>();

// Adicionar suporte a controladores
builder.Services.AddControllers();

// Adicionar suporte a Razor Pages
builder.Services.AddRazorPages();

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adicionando HttpClient
builder.Services.AddHttpClient();


var app = builder.Build();  // Aqui o build � chamado uma �nica vez

// Configura��o do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Ativar o CORS
app.UseCors("AllowAllOrigins");


// Mapeando os controladores (API)
app.MapControllers();

// Mapeando as Razor Pages (Frontend)
app.MapRazorPages();

// Rodar a aplica��o
app.Run();
