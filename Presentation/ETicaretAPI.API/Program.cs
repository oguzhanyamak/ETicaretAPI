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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options => //Authentication middleware ?a??rma i?lemi
{
    //Bu uygulamaya token ?zerinden istek geliyorsa bu token'a ait t?r ve i?erik bilgisi tan?mlan?r (do?rulamada kullan?lacak olan i?erikler)
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true, //Olu?turulacak olan token de?erini kimlerin kullanaca?? bilgisini bar?nd?r?r
        ValidateIssuer = true, // Olu?acak token de?erini kimin da??tt?n? ifade edece?i aland?r
        ValidateLifetime = true,//Olu?turulan token de?erinin s?resini kontrol edecek aland?r
        ValidateIssuerSigningKey = true, //?retilecek olan token de?erinin uygulamaya ait bir de?er oldu?unu ifade eden security key verisinin do?rulanmas?d?r.
        //yukar?da bool olarak i?aretlenen noktalar  gelen tokenlerde kontrol edilecek noktalard?r


        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))

        
    };
}); //Schema ad?


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
