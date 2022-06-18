using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPersistenceServices(); // IoC Container
builder.Services.AddInfrastructureServices();
builder.Services.AddStorage<LocalStorage>();
builder.Services.AddApplicationServices();
// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options=> options.SuppressModelStateInvalidFilter=true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options => //Authentication middleware çaðýrma iþlemi
{
    //Bu uygulamaya token üzerinden istek geliyorsa bu token'a ait tür ve içerik bilgisi tanýmlanýr (doðrulamada kullanýlacak olan içerikler)
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true, //Oluþturulacak olan token deðerini kimlerin kullanacaðý bilgisini barýndýrýr
        ValidateIssuer = true, // Oluþacak token deðerini kimin daðýttýný ifade edeceði alandýr
        ValidateLifetime = true,//Oluþturulan token deðerinin süresini kontrol edecek alandýr
        ValidateIssuerSigningKey = true, //Üretilecek olan token deðerinin uygulamaya ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr.
        //yukarýda bool olarak iþaretlenen noktalar  gelen tokenlerde kontrol edilecek noktalardýr


        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))

        
    };
}); //Schema adý


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
