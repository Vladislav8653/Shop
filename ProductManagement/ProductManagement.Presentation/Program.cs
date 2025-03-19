using MediatR;
using ProductManagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureRepository();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddValidators();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorizationPolicy();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
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

app.ApplyMigrations();

app.Run();