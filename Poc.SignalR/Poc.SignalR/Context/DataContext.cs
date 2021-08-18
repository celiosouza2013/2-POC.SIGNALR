using Microsoft.EntityFrameworkCore;
using Poc.SignalR.Models;

namespace Poc.SignalR.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
          : base(options) { }

        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Dispositivo>().HasKey(m => m.Id);
        //    builder.Entity<Mensagem>().HasKey(m => m.Id);            
        //    builder.Entity<Mensagem>().HasOne(m => m.Dispositivo).WithMany().HasForeignKey(u => u.Dispositivo);

        //    base.OnModelCreating(builder);
        //}
    }
}