using System.ComponentModel.DataAnnotations;

namespace CRUDColegio.Models
{
    public class Profesor
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
    }
}
