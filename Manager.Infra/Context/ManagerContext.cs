using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context
{
    public class ManagerContext : DbContext
    {

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data source=DESKTOP-PC\SQLEXPRESS;Initial Catalog=ManagerApi;Integrated Security=true");
        }

    }
}
