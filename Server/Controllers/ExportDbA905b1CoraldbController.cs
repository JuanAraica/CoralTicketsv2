using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using CoralTickets.Server.Data;

namespace CoralTickets.Server.Controllers
{
    public partial class Exportdb_a905b1_coraldbController : ExportController
    {
        private readonly db_a905b1_coraldbContext context;
        private readonly db_a905b1_coraldbService service;

        public Exportdb_a905b1_coraldbController(db_a905b1_coraldbContext context, db_a905b1_coraldbService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/db_a905b1_coraldb/coraltickets/csv")]
        [HttpGet("/export/db_a905b1_coraldb/coraltickets/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCoralticketsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCoraltickets(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/coraltickets/excel")]
        [HttpGet("/export/db_a905b1_coraldb/coraltickets/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCoralticketsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCoraltickets(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/equipos/csv")]
        [HttpGet("/export/db_a905b1_coraldb/equipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEquiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEquipos(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/equipos/excel")]
        [HttpGet("/export/db_a905b1_coraldb/equipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEquiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEquipos(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/histories/csv")]
        [HttpGet("/export/db_a905b1_coraldb/histories/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHistoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetHistories(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/histories/excel")]
        [HttpGet("/export/db_a905b1_coraldb/histories/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHistoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetHistories(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/mantenimientos/csv")]
        [HttpGet("/export/db_a905b1_coraldb/mantenimientos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMantenimientosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMantenimientos(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/mantenimientos/excel")]
        [HttpGet("/export/db_a905b1_coraldb/mantenimientos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMantenimientosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMantenimientos(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/ticketusers/csv")]
        [HttpGet("/export/db_a905b1_coraldb/ticketusers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTicketUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTicketUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/db_a905b1_coraldb/ticketusers/excel")]
        [HttpGet("/export/db_a905b1_coraldb/ticketusers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTicketUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTicketUsers(), Request.Query), fileName);
        }
    }
}
