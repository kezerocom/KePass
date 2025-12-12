using System.Text.Json.Serialization;
using KePass.Server.Data;
using KePass.Server.Middlewares;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using KePass.Server.Repositories.Implementations;
using KePass.Server.Services.Definitions;
using KePass.Server.Services.Implementations;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationWithToken();
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IEnvironmentService, EnvironmentService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IRepository<Account>, AccountRepository>();
builder.Services.AddScoped<IRepository<Attachment>, AttachmentRepository>();
builder.Services.AddScoped<IRepository<Audit>, AuditRepository>();
builder.Services.AddScoped<IRepository<Blob>, BlobRepository>();
builder.Services.AddScoped<IRepository<Subscription>, SubscriptionRepository>();
builder.Services.AddScoped<IRepository<Vault>, VaultRepository>();

builder.Services.AddDbContext<DatabaseContext>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();