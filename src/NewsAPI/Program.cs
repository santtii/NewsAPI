using System.Text;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewsAPI.Core;
using NewsAPI.Core.Constant;
using NewsAPI.Core.Entities;
using NewsAPI.Infrastructure;
using NewsAPI.Infrastructure.Data;
using NewsAPI.Infrastructure.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration).WriteTo.Console());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsAPI", Version = "v1" });

      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
      {
          Name = "Authorization",
          Type = SecuritySchemeType.Http,
          Scheme = "Bearer",
          BearerFormat = "JWT",
          In = ParameterLocation.Header,
          Description = "JWT Authorization header using the Bearer scheme."
      });
      c.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
  });

var connectionString = builder.Configuration.BindSettings<DbSettings>(SettingsConstants.DbSettings);
builder.Services.AddDbContext(connectionString.DefaultConnection);

//builder.WebHost.UseUrls("https://+;http://+"); //Comment to work in ec2 and http added to comunicate without ssl certificate

//builder.Services.AddHttpsRedirection(options =>
//{
//  options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
//});

//builder.Services.Configure<ForwardedHeadersOptions>(options =>
//{
//  options.ForwardedHeaders =
//      ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
//});

// Adding Authentication
var appSettings = builder.Configuration.BindSettings<AppSettings>(SettingsConstants.AppSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = appSettings.Domain,
        ValidIssuer = appSettings.Domain,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.SecurityKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development", builder.Configuration));
});

var app = builder.Build();
app.UsePathBase("/NewsAPI/");
app.UseRouting();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseHttpsRedirection(); // move and comment temporally to test in dev
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // dataContext.Database.Migrate();

    // In memory database data
    _ = (dataContext?.Categories?.Add(new CategoryEntity { Id = 1, Name = "Economy", }));
    _ = (dataContext?.Categories?.Add(new CategoryEntity { Id = 2, Name = "Sports", }));
    _ = (dataContext?.Categories?.Add(new CategoryEntity { Id = 3, Name = "Science", }));
    dataContext?.SaveChanges();
}
app.UseHsts(); // move to work in dev
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

// TODO: Get pathbase from appsettings?

app.Run();
