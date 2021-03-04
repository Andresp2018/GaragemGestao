using GaragemGestao.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaragemGestao.Data;


namespace GaragemGestao.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        //DB Set
       

        public DbSet<Client> Clients { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<RepairDetail> RepairDetails { get; set; }
        public DbSet<RepairDetailTemp> RepairDetailTemps { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Message> Messages { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {



        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                 .Property(p => p.RepairPrice)
                 .HasColumnType("decimal(18,2)");


            // Habilitar a cascade delete rule
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //Message
            modelBuilder.Entity<Message>()
                .HasOne<User>
                (a => a.Sender)
                .WithMany(d => d.Messages)
                .HasForeignKey(d => d.UserId);

            base.OnModelCreating(modelBuilder);
        }


    }
}