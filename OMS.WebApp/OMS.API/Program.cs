using OMS.API;
using OMS.API.Infrastructure.Extensions;
using OMS.Bll;
using OMS.Bll.Interfaces;
using OMS.Bll.Services;
using OMS.Dal;
using OMS.Dal.Interfaces;
using OMS.Dal.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerTypeService, CustomerTypeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderTypeService, OrderTypeService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IPaymentTermService, PaymentTermService>();
builder.Services.AddScoped<IPaymentStatusService, PaymentStatusService>();

builder.Services.AddAutoMapper(typeof(BllAssemblyMarker));

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});

builder.Services.AddDbContext<OMSDbContext>(optionBuilder =>
{
    optionBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("OMSConnection"));
});

var authOptions = builder.Services.ConfigureAuthOptions(builder.Configuration);
builder.Services.AddJwtAuthentication(authOptions);
builder.Services.AddSwagger(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(LocalizationHelper.GetLocalizationOptions());

app.UseRouting();

app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.UseDbTransaction();

app.MapControllers();

app.Run();
