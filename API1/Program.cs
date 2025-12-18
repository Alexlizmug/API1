using API1.Helpers;
using API1.Repositorioak;
using NHibernate;
using NHSession = NHibernate.ISession;
using NHSessionFactory = NHibernate.ISessionFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---- NHIBERNATE ----
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Creamos el SessionFactory de NHibernate
NHSessionFactory sessionFactory = NHibernateHelper.CreateSessionFactory(connectionString);

// Registramos la factoría y la sesión de NHibernate en DI
builder.Services.AddSingleton<NHSessionFactory>(sessionFactory);
builder.Services.AddScoped<NHSession>(sp =>
{
    var factory = sp.GetRequiredService<NHSessionFactory>();
    return factory.OpenSession();
});

// Repositorio
builder.Services.AddScoped<LangileakRepository>();
builder.Services.AddScoped<ErreserbakRepository>();
builder.Services.AddScoped<ZerbitzuaRepository>();
builder.Services.AddScoped<EskaerakRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
