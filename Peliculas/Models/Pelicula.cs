using System;
using System.ComponentModel.DataAnnotations;

namespace Peliculas.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el titulo de la pelicula.")]
        [MaxLength(50)]
        public string Titulo { get; set; }

        [MaxLength(50)]
        public string Director { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaPublicacion { get; set; }

        [Required(ErrorMessage = "Seleccione la {0} de la pelicula.")]
        [Display(Name = "Categoria")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccione una {0}")]
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}