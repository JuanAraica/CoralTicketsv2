using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoralTickets.Server.Models.db_a905b1_coraldb
{
    [Table("Coralticket", Schema = "dbo")]
    public partial class Coralticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTicket { get; set; }

        public int? idequipo { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Operacion { get; set; }

        public DateTime? FechaExpiracion { get; set; }

        public string HoraExpiracion { get; set; }

        public string TipoConsulta { get; set; }

        public string Descripcion { get; set; }

        public string Evidencia1 { get; set; }

        public string Evidencia2 { get; set; }

        public string Evidencia3 { get; set; }

        public DateTime? Fecha { get; set; }

        public string Hora { get; set; }

        public string Estado { get; set; }

        public string Observacion { get; set; }

        public string TIManager { get; set; }

    }
}