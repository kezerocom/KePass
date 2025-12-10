using KePass.Server.Data;
using KePass.Server.Middlewares;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using KePass.Server.Repositories.Implementations;
using KePass.Server.Services.Definitions;
using KePass.Server.Services.Implementations;

DotNetEnv.Env.Load(); // Load Environment Variables

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEnvironmentService, EnvironmentService>();
builder.Services.AddScoped<IRepository<Account>, AccountRepository>();
builder.Services.AddScoped<IRepository<Attachment>, AttachmentRepository>();
builder.Services.AddScoped<IRepository<Audit>, AuditRepository>();
builder.Services.AddScoped<IRepository<Subscription>, SubscriptionRepository>();
builder.Services.AddScoped<IRepository<Vault>, VaultRepository>();
builder.Services.AddDbContext<DatabaseContext>();

var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();