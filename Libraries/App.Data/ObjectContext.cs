using App.Core.Domain.Contacts;
using App.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    /// <summary>
    /// Represents database context
    /// </summary>
    public class ObjectContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public ObjectContext(DbContextOptions<ObjectContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());

            modelBuilder.Seed();
        }
    }
}
