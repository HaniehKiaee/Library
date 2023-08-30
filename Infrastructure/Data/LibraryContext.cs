using Domain;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Information> Information { get; set; }

        public override int SaveChanges()
        {
            BeforeSave();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            BeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void BeforeSave()
        {
            var now = DateTime.Now;
            foreach (var item in ChangeTracker.Entries<Entity<int>>())
            {
                var entry = item.Entity;
                switch (item.State)
                {
                    case EntityState.Added:
                        entry.CreateDate = now;
                        break;
                    case EntityState.Modified:
                        entry.ModifiedDate = now;
                        Entry(entry).Property(x => x.CreateDate).IsModified = false;
                        break;
                    case EntityState.Deleted:
                        item.State = EntityState.Unchanged;
                        entry.DeleteDate = now;
                        entry.IsDeleted = true;

                        break;
                }
            }
        }
    }
}
