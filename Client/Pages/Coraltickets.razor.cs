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
    public partial class Coraltickets
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

        protected IEnumerable<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> coraltickets;

        protected RadzenDataGrid<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> grid0;
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
                var result = await db_a905b1_coraldbService.GetCoraltickets(filter: $@"(contains(Usuario,""{search}"") or contains(Operacion,""{search}"") or contains(HoraExpiracion,""{search}"") or contains(TipoConsulta,""{search}"") or contains(Descripcion,""{search}"") or contains(Evidencia1,""{search}"") or contains(Evidencia2,""{search}"") or contains(Evidencia3,""{search}"") or contains(Hora,""{search}"") or contains(Estado,""{search}"") or contains(Observacion,""{search}"") or contains(TIManager,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                coraltickets = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Coraltickets" });
            }
        }    

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCoralticket>("Add Coralticket", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> args)
        {
            await DialogService.OpenAsync<EditCoralticket>("Edit Coralticket", new Dictionary<string, object> { {"idTicket", args.Data.idTicket} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket coralticket)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await db_a905b1_coraldbService.DeleteCoralticket(idTicket:coralticket.idTicket);

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
                    Detail = $"Unable to delete Coralticket" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await db_a905b1_coraldbService.ExportCoralticketsToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "Coraltickets");
            }

            if (args == null || args.Value == "xlsx")
            {
                await db_a905b1_coraldbService.ExportCoralticketsToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "Coraltickets");
            }
        }
    }
}