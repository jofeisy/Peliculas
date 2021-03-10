using Peliculas.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Peliculas.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DataBaseConnection")
        {
            Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

        public DbSet<Pelicula> Peliculas { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
    }
}