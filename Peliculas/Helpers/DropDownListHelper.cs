using Peliculas.Data;
using Peliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Peliculas.Helpers
{
    public class DropDownListHelper : IDisposable
    {
        private static DataContext db = new DataContext();

        public static List<Categoria> ObtenerCategorias()
        {
            var categorias = db.Categorias.ToList();

            categorias.Add(new Categoria
            {
                Id = 0,
                Descripcion = "[Seleccione...]"
            });

            return categorias.OrderBy(c => c.Descripcion).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}