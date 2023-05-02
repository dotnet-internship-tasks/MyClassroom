using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MyClassroom.Application.Services;
using MyClassroom.Infrastructure.Data;
using MyClassroom.Infrastructure.Services;
using MyClassroom.MVC.Auth;
using MyClassroom.MVC.Github;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.Configure<GithubProductHeaderOptions>(builder.Configuration.GetSection(GithubProductHeaderOptions.Name));
builder.Services.Configure<GithubAppOptions>(builder.Configuration.GetSection(GithubAppOptions.Name));
builder.Services.AddScoped<IClaimsTransformation, RoleClaimsTransformation>();
builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.Events.OnSigningIn = AuthEventsFunctions.OnSigningIn;
    })
    .AddGithub(options =>
    {
        options.SignInScheme = "cookie";

        GithubAppOptions githubAppOptions = new();
        builder.Configuration.GetSection(GithubAppOptions.Name).Bind(githubAppOptions);
        options.ClientId = githubAppOptions.ClientId;
        options.ClientSecret = githubAppOptions.ClientSecret;

        options.ClaimActions.MapJsonKey("github_id", "id");
        options.ClaimActions.MapJsonKey("github_login", "login");
        options.ClaimActions.MapCustomJson("session_id", (e) => Guid.NewGuid().ToString());

        options.Events.OnCreatingTicket = AuthEventsFunctions.OnCreatingTicket;
    });

builder.Services.AddDbContext<MyClassroomContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MyClassroom.MVC"));
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IGitHubClientService, GitHubClientService>();
builder.Services.AddTransient<IOrganizationService, OrganizationService>();
builder.Services.AddTransient<IClassroomService, ClassroomService>();
builder.Services.AddTransient<IAssignmentService, AssignmentService>();
builder.Services.AddTransient<IRepositoryService,  RepositoryService>();

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

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
