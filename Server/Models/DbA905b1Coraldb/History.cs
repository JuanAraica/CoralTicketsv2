using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoralTickets.Server.Models.db_a905b1_coraldb
{
    [Table("History", Schema = "dbo")]
    public partial class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idHistory { get; set; }

        public string Registro { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }

    }
}