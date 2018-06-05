using LaRutaDelSoftware.DataAccess.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LaRutaDelSoftware.DataAccess.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private EFDbContext dbContext;

        public UnitOfWork()
        {
            dbContext = new EFDbContext();
        }

        internal DbSet<TEntity> GetSetOf<TEntity>() where TEntity : class
        {
            return dbContext.Set<TEntity>();
        }
        
        /// <summary>
        /// <see cref="https://code.msdn.microsoft.com/How-to-undo-the-changes-in-00aed3c4"/>
        /// </summary>
        public void Rollback()
        {
            // Undo the changes of the all entries. 
            foreach (DbEntityEntry entry in dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from  
                    // Modified to Unchanged first sets the values of all  
                    // properties to the original values that were read from  
                    // the database when it was queried, and then marks the  
                    // entity as Unchanged. This will also reject changes to  
                    // FK relationships since the original value of the FK  
                    // will be restored. 
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
