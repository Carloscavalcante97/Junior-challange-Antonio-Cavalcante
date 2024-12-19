using Junior_challange_.Services;
using Junior_Challenge.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem (use com cautela em produção)
              .AllowAnyMethod()  // Permite qualquer método HTTP (GET, POST, DELETE, etc.)
              .AllowAnyHeader(); // Permite qualquer cabeçalho
    });
});

// Configuração do banco de dados
var connectionString = builder.Configuration.GetConnectionString("AnelDatabase");
builder.Services.AddDbContext<AnelContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar o serviço com lifetime Scoped
builder.Services.AddScoped<ServicosAnel>();

// Adicionar suporte a controladores
builder.Services.AddControllers();

// Adicionar suporte a Razor Pages
builder.Services.AddRazorPages();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adicionando HttpClient
builder.Services.AddHttpClient();


var app = builder.Build();  // Aqui o build é chamado uma única vez

// Configuração do pipeline HTTP
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

// Rodar a aplicação
app.Run();
