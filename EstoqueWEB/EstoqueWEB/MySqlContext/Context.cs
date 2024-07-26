using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EstoqueWEB.MySqlContext
{
    public class Context : IdentityDbContext<AplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<AplicationUser> Users {  get; set; }   
        public DbSet<Devolucao> Devolucao { get; set; }
        public DbSet<Celular> Celular { get; set; }

    }
}