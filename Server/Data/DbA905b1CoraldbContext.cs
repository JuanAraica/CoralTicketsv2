using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using CoralTickets.Server.Models.db_a905b1_coraldb;

namespace CoralTickets.Server.Data
{
    public partial class db_a905b1_coraldbContext : DbContext
    {
        public db_a905b1_coraldbContext()
        {
        }

        public db_a905b1_coraldbContext(DbContextOptions<db_a905b1_coraldbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>()
              .HasOne(i => i.Equipo)
              .WithMany(i => i.Mantenimientos)
              .HasForeignKey(i => i.idequipo)
              .HasPrincipalKey(i => i.idequipo);
        }

        public DbSet<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> Coraltickets { get; set; }

        public DbSet<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> Equipos { get; set; }

        public DbSet<CoralTickets.Server.Models.db_a905b1_coraldb.History> Histories { get; set; }

        public DbSet<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> Mantenimientos { get; set; }

        public DbSet<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> TicketUsers { get; set; }
    }
}