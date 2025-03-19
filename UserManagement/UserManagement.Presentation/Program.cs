using MediatR;
using UserManagement.Application.Contracts.AuthenticationContracts;
using UserManagement.Application.Contracts.SmtpContracts;
using UserManagement.Application.EmailService;
using UserManagement.Application.MappingProfiles;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Extensions;
using UserManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
builder.Services.AddScoped<ISmtpService, SmtpService>();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureIdentity();
builder.Services.AddAuthorizationPolicy();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddControllers();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Inno shop");
    s.RoutePrefix = string.Empty;
});

app.UseRouting();

app.ConfigureExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations(); // авто миграции для докера


app.Run();
