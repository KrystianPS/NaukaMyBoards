﻿using Microsoft.EntityFrameworkCore;

namespace MyBoards.Entities
{
    public class MyBoardContext : DbContext

    {

        public MyBoardContext(DbContextOptions<MyBoardContext> options) : base(options)
        {

        }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<WorkItem>()
            //    .Property(x => x.State)
            //    .IsRequired();
            //modelBuilder.Entity<WorkItem>()
            //    .Property(x => x.Area)
            //    .HasColumnType("varchar(200)");
            //** REFACTORED FOR entity type builder lambda ** 

            modelBuilder.Entity<Epic>()
                .Property(e => e.EndDate).HasPrecision(3);

            modelBuilder.Entity<Task>(eb =>
            {
                eb.Property(t => t.RemainingWork).HasPrecision(14, 2);
                eb.Property(t => t.Activity).HasMaxLength(200);

            });


            modelBuilder.Entity<Issue>()
                .Property(i => i.Effort).HasColumnType("decimal(5,2)");

            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(x => x.Area).HasColumnType("varchar(200)");
                eb.Property(x => x.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(x => x.Priority).HasDefaultValue(1);

                eb.HasMany(x => x.Comments)
                    .WithOne(c => c.WorkItem)
                    .HasForeignKey(c => c.Id);

                eb.HasOne(x => x.Author)
                    .WithMany(u => u.WorkItems)
                    .HasForeignKey(u => u.AuthorId);


                eb.HasMany(x => x.Tags)
                    .WithMany(t => t.WorkItems)
                    .UsingEntity<WorkItemTag>(
                        x => x.HasOne(wit => wit.Tag)
                            .WithMany()
                            .HasForeignKey(wit => wit.TagId),

                        x => x.HasOne(wit => wit.WorkItem)
                            .WithMany()
                            .HasForeignKey(wit => wit.WorkItemId),

                        wit =>
                        {
                            wit.HasKey(x => new { x.TagId, x.WorkItemId });
                            wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                        }




                    );

                eb.HasOne(x => x.State)
                    .WithMany(s => s.WorkItems)
                    .HasForeignKey(x => x.StateId);


            });
            //configure default values
            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedCommentDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdatedCommentDate).ValueGeneratedOnUpdate();
                eb.HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(u => u.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<State>()
                .Property(s => s.CurrentState).IsRequired().HasColumnType("varchar(50)");






        }
    }
}
