using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Serilog;
using EasyTesting.Core.Data;
using Microsoft.EntityFrameworkCore;
using EasyTesting.Core.Service;
using EasyTesting.Web.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = options.DefaultPolicy;
//});

//builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
//{
//    options.Events = new OpenIdConnectEvents
//    {

//        OnTokenValidated = async context =>
//        {
//            var httpContext = context.HttpContext;
//            httpContext.Response.Redirect("/");
//            context.HandleResponse();
//        },

//        OnRemoteFailure = context =>
//        {
//            context.Response.Redirect("/AccessDenied");
//            context.HandleResponse();
//            return Task.CompletedTask;
//        }
//    };
//});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Service API",
        Version = "v1",
        Description = "User Service API"
    });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "EasyTestingApi.xml"));
});

builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
           sqlOptions => sqlOptions.MigrationsAssembly("EasyTesting.Web")));

builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7067");
});
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    if (!context.User.Identity.IsAuthenticated)
//    {
//        context.Response.Redirect("/MicrosoftIdentity/Account/SignIn");
//        return;
//    }
//    await next();
//});

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.MapControllers();
app.MapRazorPages();

try
{
    app.Run();
}
finally
{
    Log.CloseAndFlush();
}

