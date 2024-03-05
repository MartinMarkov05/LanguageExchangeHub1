using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LanguageExchangeHub1.Data;
using LanguageExchangeHub1.Data.Models;
using System.Data;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Repository;
using AutoMapper;
using LanguageExchangeHub1.Utilities;
using LanguageExchangeHub1.Services;
using LanguageExchangeHub1.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(connectionString, b => b.MigrationsAssembly("LanguageExchangeHub1")).EnableSensitiveDataLogging()
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthorization();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Login";
    options.LogoutPath = "/Identity/Logout";
    
});
builder.Services.AddSingleton(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
builder.Services.AddScoped(x => x.GetRequiredService<MapperConfiguration>().CreateMapper());

// Add services to the container.
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEfRepository<User>, EfRepository<User>>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserData, UserData>();
builder.Services.AddTransient<IdentityRole>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<ILanguageService, LanguageService>();
builder.Services.AddTransient<IEfRepository<Language>, EfRepository<Language>>();
builder.Services.AddTransient<IEfRepository<Course>, EfRepository<Course>>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IEfRepository<Role>, EfRepository<Role>>();

builder.Services.AddTransient<IEfRepository<CourseUser>, EfRepository<CourseUser>>();
builder.Services.AddTransient<ICourseUserService, CourseUserService>();
builder.Services.AddTransient<IEfRepository<Request>, EfRepository<Request>>();
builder.Services.AddTransient<IRequestService, RequestService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();



app.UseHttpsRedirection()
               .UseCookiePolicy()
               .UseStaticFiles()
               .UseAuthentication();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller}/{action}/{id?}",
        defaults: new { Controller = "Identity", action = "Login" });
});

app.MapRazorPages();

app.Run();

