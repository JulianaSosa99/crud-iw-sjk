﻿using System.ComponentModel.DataAnnotations;

namespace login_api_iw_js.Models
{
    public class Hito
    {
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int ObjetivoId { get; set; }
        public Objetivo Objetivo { get; set; }

        [Required]
        public int TemaId { get; set; }
        public Tema Tema { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? Calificacion { get; set; } // número total de bolitas (máximo)

        public ICollection<Progreso> Progresos { get; set; }
        public ICollection<Subtema> Subtemas { get; set; }


    }

}
