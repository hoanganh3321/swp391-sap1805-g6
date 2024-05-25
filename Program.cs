var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//phai map services de co the chuyen ve cho controller xu ly
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
