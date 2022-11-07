using LinkReduction.Infrastucture;
using LinkReduction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace LinkReduction.Context
{
    //TODO: СОБРАТЬ DTO Под БД!
    public class DBContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        //public class TrelloCloneDbContext 
        //{

        public DBContext(DbContextOptions<DBContext> options)
                : base(options)
        {

        }

        public DbSet<CompresedLink> CompresedLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CompresedLink>().ToTable("CompresedLink");
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }

    }
}
