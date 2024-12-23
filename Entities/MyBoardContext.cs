using Microsoft.EntityFrameworkCore;

namespace MyBoards.Entities
{
    public class MyBoardContext : DbContext

    {

        public MyBoardContext(DbContextOptions<MyBoardContext> options) : base(options)
        {

        }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<WorkItem>()
            //    .Property(x => x.State)
            //    .IsRequired();
            //modelBuilder.Entity<WorkItem>()
            //    .Property(x => x.Area)
            //    .HasColumnType("varchar(200)");
            //** REFACTORED FOR entity type builder lambda ** 

            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(x => x.State).IsRequired();
                eb.Property(x => x.Area).HasColumnType("varchar(200)");
                eb.Property(x => x.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(x => x.Effort).HasColumnType("decimal(5,2");
                eb.Property(x => x.Activity).HasMaxLength(200);
                eb.Property(x => x.RemainingWork).HasPrecision(14, 2);
                eb.Property(x => x.Priority).HasDefaultValue(1);


            });
            //configure default values
            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedCommentDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdatedCommentDate).ValueGeneratedOnUpdate();
            });
        }
    }
}
