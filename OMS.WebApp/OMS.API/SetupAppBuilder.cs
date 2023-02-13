using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using OMS.Bll;
using OMS.Bll.Interfaces;
using OMS.Bll.Services;
using OMS.Dal;
using OMS.Dal.Interfaces;
using OMS.Dal.Repositories;
using OMS.Domain;

namespace OMS.API
{
    public static class SetupAppBuilder
    {
        public static void Setup(this WebApplicationBuilder builder)
        {
            ConfigureDbContext(builder);
            RegisterServices(builder.Services);
            ConfigureAuthentication(builder);
            //ConfigureAuthorization(builder.Services);
        }

        private static void ConfigureDbContext(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configurations = builder.Configuration;
            services.AddDbContext<OMSDbContext>(optionBuilder =>
            {
                optionBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("OMSConnection"));
            });

            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole<int>>()
                .AddUserManager<UserManager<User>>()
                .AddEntityFrameworkStores<OMSDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        }


        private static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerTypeService, CustomerTypeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderTypeService, OrderTypeService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPaymentTermService, PaymentTermService>();
            services.AddScoped<IPaymentStatusService, PaymentStatusService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(typeof(BllAssemblyMarker));


            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var client = builder.Configuration["client"];
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = "UserLoginCookie";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.Events.OnRedirectToAccessDenied = (context) =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    options.Cookie.HttpOnly = true;
                    // Only use this when the sites are on different domains
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                });


            services.AddCors(options =>
            options.AddPolicy("Cors", builder =>
            {
                builder.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS", "PUT")
                .WithHeaders(
                    HeaderNames.Accept,
                    HeaderNames.ContentType,
                    HeaderNames.Authorization)
                .AllowCredentials()
                .SetIsOriginAllowed(origin =>
                {
                    if (string.IsNullOrWhiteSpace(origin)) return false;
                    if (origin.ToLower().StartsWith(client)) return true;
                    return false;
                });
            })
            );
        }

        //private static void ConfigureAuthorization(IServiceCollection services)
        //{
        //    services.AddAuthorization(o =>
        //    {
        //        o.AddPolicy(PolicyName.ClubAdmin, policy => policy.RequireAuthenticatedUser().Requirements.Add(new ClubAdminRequirement()));
        //        o.AddPolicy(PolicyName.ClubMember, policy => policy.RequireAuthenticatedUser().Requirements.Add(new ClubMemberRequirement()));
        //        o.AddPolicy(PolicyName.CanExcludeDefaultPlayer, policy => policy.RequireAuthenticatedUser().Requirements.Add(new CanExcludeDefaultRequirement()));
        //    });
        //    services.AddScoped<IAuthorizationHandler, ClubAdminHandler>();
        //    services.AddScoped<IAuthorizationHandler, ClubMemberHandler>();
        //    services.AddScoped<IAuthorizationHandler, CanExcludeDefaultHandler>();
        //}

    }
}
