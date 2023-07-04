using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options) 
        {


        }

        //Add DbSet (Esto va a crear Tablas de la base de datos)

        public DbSet<User>? Users { get; set; }

    }
}
