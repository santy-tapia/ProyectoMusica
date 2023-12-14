using System.ComponentModel.DataAnnotations;

namespace ProyectoMusica.Models
{
    public class Grupo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre del grupo es necesario")]
        [Display(Name ="Nombre del grupo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El nombre del vocalista es necesario")]
        public string Vocalista { get; set; }
        [Required(ErrorMessage = "El número de integrantes del grupo es necesario")]
        [Display(Name = "Número de Integrantes")]
        [Range(1,10,ErrorMessage ="El número de integrantes debe estar comprendido entre 1 y 10")]
        public int NumIntegrantes { get; set; }
        [Display(Name = "Año de Formación")]
        public int AnoFormacion { get; set; }
        [Required(ErrorMessage = "El genero es necesario")]
        [Display(Name = "Género")]
        public string Genero { get; set; }

    }
}
