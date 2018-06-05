using LaRutaDelSoftware.DomainEntities;
using System.Configuration;
using System.Data.Entity;

namespace LaRutaDelSoftware.DataAccess.EntityFramework
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base()
        {
            string dbName = ConfigurationManager.AppSettings["DataBase"];
            base.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
        }

        public DbSet<Subject> SubjectsSet { get; set; }
        public DbSet<User> UsersSet { get; set; }
        public DbSet<Student> StudentsSet { get; set; }
        public DbSet<StudentSubject> StudentSubjectsSet { get; set; }
    }
}
