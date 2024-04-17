using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.Contexts;
using Entities.Models;
using Services.Base;
using Repositories;
using Services.Contracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
           .UseLazyLoadingProxies())
    .AddIdentity<User, Role>(options =>
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


builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Login";
    options.LogoutPath = "/Identity/Logout";
    
});

// Add services to the container.
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IServicesResourceProvider, SingleServiceResourceProvider>();
builder.Services.AddTransient(typeof(IEfRepository<>), typeof(EfRepository<>));
builder.Services.AddTransient<IUserData, UserData>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<ILanguageService, LanguageService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IRequestService, RequestService>();
builder.Services.AddTransient<IMessageService,MessageService>();
builder.Services.AddTransient<IAssignmentService, AssignmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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