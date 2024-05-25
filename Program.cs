using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using swp391_sap1805_g6;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//phai map services de co the chuyen ve cho controller xu ly
builder.Services.AddControllers();
//endpoint
builder.Services.AddEndpointsApiExplorer();
//cho phep tat ca dung api
builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
//dang ky database
builder.Services.AddDbContext<BanhangContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
