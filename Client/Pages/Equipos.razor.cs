using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CoralTickets.Client.Pages
{
    public partial class Equipos
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public db_a905b1_coraldbService db_a905b1_coraldbService { get; set; }

        protected IEnumerable<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> equipos;

        protected RadzenDataGrid<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> grid0;
        protected int count;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            await grid0.Reload();
        }

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await db_a905b1_coraldbService.GetEquipos(filter: $@"(contains(TipoEquipo,""{search}"") or contains(Marca,""{search}"") or contains(Poseedor,""{search}"") or contains(Modelo,""{search}"") or contains(NumeroSerie,""{search}"") or contains(Estado,""{search}"") or contains(Ubicacion,""{search}"") or contains(Descripcion,""{search}"") or contains(CantidadBodega,""{search}"") or contains(AnyDesk,""{search}"") or contains(AnyDeskPass,""{search}"") or contains(TeamVewr,""{search}"") or contains(TeamVewrPass,""{search}"") or contains(Ipnum,""{search}"") or contains(FechaIngreso,""{search}"") or contains(HoraIngreso,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                equipos = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Equipos" });
            }
        }    

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddEquipo>("Add Equipo", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> args)
        {
            await DialogService.OpenAsync<EditEquipo>("Edit Equipo", new Dictionary<string, object> { {"idequipo", args.Data.idequipo} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, CoralTickets.Server.Models.db_a905b1_coraldb.Equipo equipo)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await db_a905b1_coraldbService.DeleteEquipo(idequipo:equipo.idequipo);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                { 
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error", 
                    Detail = $"Unable to delete Equipo" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await db_a905b1_coraldbService.ExportEquiposToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "Equipos");
            }

            if (args == null || args.Value == "xlsx")
            {
                await db_a905b1_coraldbService.ExportEquiposToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "Equipos");
            }
        }
    }
}