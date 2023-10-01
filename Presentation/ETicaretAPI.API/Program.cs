using ETicaretAPI.Application.Extension;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Persistence.Extension;
using ETicaretAPI.Infrastructure.Extension;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ETicaretAPI.API.Extensions;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using ETicaretAPI.API.Configuration;
using Serilog.Context;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200")));
// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>());//.ConfigureApiBehaviorOptions()
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Host.UseSerilog(
    new LoggerConfiguration()
    .WriteTo.File("logs/log.txt")
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", needAutoCreateTable: true,
    columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        {"message",new RenderedMessageColumnWriter() },
        {"message_template", new MessageTemplateColumnWriter() },
        {"level",new LevelColumnWriter() },
        {"time_stamp",new TimestampColumnWriter() },
        {"exception", new ExceptionColumnWriter() },
        {"log_evet",new LogEventSerializedColumnWriter() },
        {"username", new UsernameColumnWriter()}
    }).Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger()
    ) ;

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.ExceptionHandlerExtension<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();

app.Run();
