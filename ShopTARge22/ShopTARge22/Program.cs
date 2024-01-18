using ShopTARge22.Data;
using Microsoft.EntityFrameworkCore;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.ApplicationServices.Services;
using Microsoft.Extensions.FileProviders;
using ShopTARge22.Hubs;
using Microsoft.AspNetCore.Identity;
using ShopTARge22.Core.Domain;
using ShopTARge22.Security;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IRealEstatesServices, RealEstatesServices>();
builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();
builder.Services.AddScoped<IChuckNorrisServices, ChuckNorrisServices>();
builder.Services.AddScoped<ICocktailServices, CocktailServices>();
builder.Services.AddScoped<IEmailsServices, EmailsServices>();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ShopTARge22Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequiredLength = 3;

            options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            options.Lockout.MaxFailedAccessAttempts = 2;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
        .AddEntityFrameworkStores<ShopTARge22Context>()
        .AddDefaultTokenProviders()
        .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
        .AddDefaultUI();

//all tokens
builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
    o.TokenLifespan = TimeSpan.FromHours(5));

//email tokens confirmation
builder.Services.Configure<CustomEmailConfirmationTokenProviderOptions>(o =>
o.TokenLifespan = TimeSpan.FromDays(3));

builder.Services.AddAuthentication()

    .AddFacebook(options =>
    {
        options.AppId = "330287349968902";
        options.AppSecret = "b254802e897c7859499e194a50320109";
    })

    .AddGoogle(options =>
    {
        options.ClientId = "253677322216-5d6osssg6eo3fog9blubssvb50t4fc8q.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });


//alternative way
//builder.Services.AddDefaultIdentity<IdentityUser>()
//        .AddEntityFrameworkStores<ShopTARge22Context>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider
    (Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
    RequestPath = "/multipleFileUpload"
});

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chatHub");

app.Run();
