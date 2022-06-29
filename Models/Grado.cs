using System.ComponentModel.DataAnnotations;

namespace CRUDColegio.Models
{
    public class Grado
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ProfesorId { get; set; }
    }
}
