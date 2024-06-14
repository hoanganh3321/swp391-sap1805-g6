using System.Text;
using BackEnd;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BackEnd.Reporitories;
using BackEnd.Services;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
using BackEnd.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
//loop handle
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
//Multithreading handle
builder.Services.AddTransient<Banhang3Context>();

// Cho phép tất cả các nguồn gốc sử dụng API
builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
));

// Đăng ký cơ sở dữ liệu
builder.Services.AddDbContext<Banhang3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB"))
);


// Đăng ký Repo, service
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
//
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
//
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
//
builder.Services.AddScoped<IOrderRepository, OrderRepositoty>();
//
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
//
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.AddScoped<ILoyalPointRepository, LoyalPointRepository>();
builder.Services.AddScoped<ILoyalPointService, LoyalPointService>();
//
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
//
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
//
builder.Services.AddScoped<IProductReturnService, ProductReturnService>();
builder.Services.AddScoped<IProductReturnRepository, ProductReturnRepository>();
//
builder.Services.AddScoped<IReturnPolicyService, ReturnPolicyService>();
builder.Services.AddScoped<IReturnPolicyRepository, ReturnPolicyRepository>();
//
builder.Services.AddScoped<IGoldPriceDisplayRepository, GoldPriceDisplayRepository>();
builder.Services.AddScoped<IGoldPriceDisplayService, GoldPriceDisplayService>();
//
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();
// Đăng ký filter 
builder.Services.AddScoped<JwtAuthorizationFilter>();
builder.Services.AddScoped<AdminAuthorizationFilter>();
builder.Services.AddScoped<StaffAuthorizerFilter>();
builder.Services.AddScoped<ProductValidationFilter>();

// Thêm cấu hình xác thực JWT vào services collection
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
//app
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
