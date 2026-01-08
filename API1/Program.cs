using API1.Helpers;
using API1.Repositorioak;
using NHibernate;
using NHSession = NHibernate.ISession;
using NHSessionFactory = NHibernate.ISessionFactory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---- NHIBERNATE ----
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection in appsettings.json");
}

NHSessionFactory sessionFactory = NHibernateHelper.CreateSessionFactory(connectionString);

builder.Services.AddSingleton<NHSessionFactory>(sessionFactory);
builder.Services.AddScoped<NHSession>(sp =>
{
    var factory = sp.GetRequiredService<NHSessionFactory>();
    return factory.OpenSession();
});

// Repos (todas las tablas)
builder.Services.AddScoped<ErabiltzaileakRepository>();
builder.Services.AddScoped<ErregistroakRepository>();
builder.Services.AddScoped<ErreserbakRepository>();
builder.Services.AddScoped<EskaerakRepository>();
builder.Services.AddScoped<FakturaHistorikoakRepository>();
builder.Services.AddScoped<FakturakRepository>();
builder.Services.AddScoped<LangileakRepository>();
builder.Services.AddScoped<MahaiakRepository>();
builder.Services.AddScoped<PlateraMotakRepository>();
builder.Services.AddScoped<PlaterakRepository>();
builder.Services.AddScoped<ProduktuakRepository>();
builder.Services.AddScoped<ProduktuenEskaerakRepository>();
builder.Services.AddScoped<ProduktuenMotakRepository>();
builder.Services.AddScoped<ZerbitzuaRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
