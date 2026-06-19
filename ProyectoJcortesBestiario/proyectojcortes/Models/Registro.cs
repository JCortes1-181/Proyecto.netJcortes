using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectojcortes.Models
{
    public class Registro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int CriaturaId { get; set; }

        public int CantidadDerrotas { get; set; } = 0;

        public string? Notas { get; set; }

        [Required]
        public bool EstaDerrotado { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("CriaturaId")]
        public Criatura? Criatura { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
