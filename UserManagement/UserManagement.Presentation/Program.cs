using MediatR;
using UserManagement.Application.MappingProfiles;
using UserManagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureIdentity();
builder.Services.AddAuthorizationPolicy();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureAuthenticationManager();
builder.Services.AddControllers();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Inno shop");
});

app.UseRouting();

app.ConfigureExceptionHandler();

app.MapControllers();

app.Run();
