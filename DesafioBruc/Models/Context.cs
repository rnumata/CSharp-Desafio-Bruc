using System;
using Microsoft.EntityFrameworkCore;

namespace DesafioBruc.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Cliente> Clientes { get; set; }


    }
}
