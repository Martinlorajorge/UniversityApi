//1. Using para trabajar con EntityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApiBackend;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

//2.Coneccion con la base de datos
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);


//3. Establesco contexto --- se toma el conectionString de arriva que esta declarado en el var
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));


//7. Add Service of JWT Autorization
builder.Services.AddJwtTokenServices(builder.Configuration);


// Add services to the container.

builder.Services.AddControllers();

//4. Add custom Services (Folder Services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
//Aqui abajo agragaria el resto de los servicios



//8. agregar autorizacion al proyecto

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//9.Configurar Swwager para la Autorizacion de JWT
builder.Services.AddSwaggerGen(options =>
        {   //Define la seguridad de autentificacion en la cabecera
            options.AddSecurityDefinition("Bearer", new OpenApiSegurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization Header using Bearer Scheme"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[]{}
                    }
                });
        }
);

//5. CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//6. Use Cors

app.UseCors("CorsPolicy");

app.Run();
