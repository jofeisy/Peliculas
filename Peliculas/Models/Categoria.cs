using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peliculas.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Ingrese la descripción de la categoria.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
      
        public virtual ICollection<Pelicula> Peliculas { get; set; }

        [Display(Name = "Total de Peliculas")]
        public int TotalPeliculas => Peliculas == null ? 0 : Peliculas.Count;
    }
}