using System.Reflection;
using MediatR;
using ProductManagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureRepository();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddValidators();
builder.Services.ConfigureAutoMapper();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
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

