using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoralTickets.Server.Models.db_a905b1_coraldb
{
    [Table("Mantenimiento", Schema = "dbo")]
    public partial class Mantenimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMantenimiento { get; set; }

        public int? idequipo { get; set; }

        public DateTime? FechaProgramada { get; set; }

        public string TipoMantenimiento { get; set; }

        public string Estado { get; set; }

        public string Hora { get; set; }

        public string Descripcion { get; set; }

        public string TIManager { get; set; }

        public Equipo Equipo { get; set; }

    }
}