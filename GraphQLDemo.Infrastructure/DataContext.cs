using GraphQLDemo.Service.Models;
using GraphQLDemo.Service.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Infrastructure;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Human>? Humans { get; set; }
    public DbSet<Trait>? Traits { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Human>().HasKey(human => human.Id);
        builder.Entity<Trait>().HasKey(trait => trait.Id);
        builder.Entity<User>().HasKey(user => user.Id);

        builder.Entity<Human>()
            .HasMany(human => human.Traits)
            .WithMany(trait => trait.Humans)
            .UsingEntity<Dictionary<string, object>>(
                "HumanTraits",
                j => j
                    .HasOne<Trait>()
                    .WithMany()
                    .HasForeignKey("TraitId"),
                j => j
                    .HasOne<Human>()
                    .WithMany()
                    .HasForeignKey("IconId"));

        builder.Entity<User>()
            .HasIndex(user => user.Name)
            .IsUnique();
    }
}