using System;
using System.ComponentModel.DataAnnotations;

namespace proyectojcortes.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string Contrasena { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
