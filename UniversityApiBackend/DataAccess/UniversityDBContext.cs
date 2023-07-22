using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        //Registrar el CRUD en un logger 
        private readonly ILoggerFactory _loggerFactory;

 
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options, ILoggerFactory loggerFactory): base(options)
        { 
            _loggerFactory = loggerFactory;
        }

        //Add DbSet (Esto va a crear Tablas de la base de datos)

        public DbSet<User>? Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Student> Students { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<UniversityDBContext>();
            //las dos lineas de abajo muestrn toda la informacion
            //optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }));
            //optionsBuilder.EnableSensitiveDataLogging();


            //Este controla mejor la salida del log por la informacion
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
                ;
        }


    }
}
