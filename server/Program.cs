using KePass.Server.Middlewares;

DotNetEnv.Env.Load(); // Load Environment Variables

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();