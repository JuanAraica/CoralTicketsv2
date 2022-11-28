using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoralTickets.Server.Models.db_a905b1_coraldb
{
    [Table("TicketUser", Schema = "dbo")]
    public partial class TicketUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TicketUser")]
        public int TicketUser1 { get; set; }

        public string TicketUserCorreo { get; set; }

        public string TicketUserPass { get; set; }

        public string TicketUserRole { get; set; }

    }
}