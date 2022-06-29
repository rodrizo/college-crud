using System.ComponentModel.DataAnnotations;

namespace CRUDColegio.Models
{
    public class AlumnoGrado
    {
        [Key]
        public int Id { get; set; }
        public int AlumnoId{ get; set; }
        public int GradoId{ get; set; }
        public string Seccion { get; set; }
    }
}
