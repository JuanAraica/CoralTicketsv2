using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoralTickets.Server.Models.db_a905b1_coraldb
{
    [Table("Equipo", Schema = "dbo")]
    public partial class Equipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idequipo { get; set; }

        public string TipoEquipo { get; set; }

        public string Marca { get; set; }

        public string Poseedor { get; set; }

        public string Modelo { get; set; }

        public string NumeroSerie { get; set; }

        public string Estado { get; set; }

        public string Ubicacion { get; set; }

        public string Descripcion { get; set; }

        public DateTime? UltimoMantenimiento { get; set; }

        public DateTime? MantenimientoProgramado { get; set; }

        public DateTime? FechaCompra { get; set; }

        public DateTime? FechaGarantia { get; set; }

        public string CantidadBodega { get; set; }

        public string AnyDesk { get; set; }

        public string AnyDeskPass { get; set; }

        public string TeamVewr { get; set; }

        public string TeamVewrPass { get; set; }

        public string Ipnum { get; set; }

        public string FechaIngreso { get; set; }

        public string HoraIngreso { get; set; }

        public IEnumerable<Mantenimiento> Mantenimientos { get; set; }

    }
}