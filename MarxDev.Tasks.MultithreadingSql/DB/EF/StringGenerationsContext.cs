
using Microsoft.EntityFrameworkCore;

namespace MarxDev.Tasks.MultithreadingSql
{
    public class StringGenerationsContext:DbContext
    {
        public DbSet<StringGeneration> StringGenerations { get; set; }
        public StringGenerationsContext(DbContextOptions<StringGenerationsContext> options) : base(options)
        {
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StringGeneration>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<StringGeneration>()
              .Property(a => a.Id)
              .HasDefaultValueSql("NEWID()")
              .HasColumnName("ID");
            modelBuilder.Entity<StringGeneration>()
             .Property(a => a.Text)            
             .IsRequired();

        }

    }
}
