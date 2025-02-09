using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình JSON file cho Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Đăng ký các dịch vụ vào DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(builder.Configuration);

// Thêm CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Cấu hình middleware cho ứng dụng
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Kích hoạt CORS
app.UseCors("AllowAllOrigins");

// Kích hoạt HTTPS Redirection
app.UseHttpsRedirection();

// Kích hoạt Authorization
app.UseAuthorization();

// Map các controller
app.MapControllers();

// Kích hoạt Ocelot Middleware
await app.UseOcelot();

app.Run();
