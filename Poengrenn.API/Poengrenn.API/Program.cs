using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Poengrenn.DAL.EFRepository;
using Poengrenn.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Formatting = Formatting.Indented;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddDbContext<PoengrennContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PoengrennContext");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped(typeof(EFPoengrennRepository<>));

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
