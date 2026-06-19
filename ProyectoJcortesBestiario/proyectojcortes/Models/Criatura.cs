using System.ComponentModel.DataAnnotations;

namespace proyectojcortes.Models
{
    public class Criatura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public string Bioma { get; set; }

        [Required]
        public int RarezaBase { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
