using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string? nombre { get; set; }
        public string? apellidoP { get; set; }
        public string? apellidoM { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? status { get; set; }
        public string? role { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? createdAt { get; set; }

    }

    public class Productos
    {
        [Key]
        public int idProducto { get; set; } 

        [StringLength(50)] 
        public string? nombreProducto { get; set; }

        [StringLength(200)] 
        public string? comentarios { get; set; }

        [StringLength(100)] 
        public string? descripcion { get; set; }

        public int? categoria { get; set; }

        public int estatus { get; set; }

        public DateTime? UpdatedAt { get; set; } 

        public DateTime? CreatedAt { get; set; }
    }
}
