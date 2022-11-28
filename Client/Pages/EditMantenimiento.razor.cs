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
    public partial class EditMantenimiento
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

        [Parameter]
        public int idMantenimiento { get; set; }

        protected override async Task OnInitializedAsync()
        {
            mantenimiento = await db_a905b1_coraldbService.GetMantenimientoByIdMantenimiento(idMantenimiento:idMantenimiento);
        }
        protected bool errorVisible;
        protected CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento mantenimiento;

        protected IEnumerable<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> equiposForidequipo;


        protected int equiposForidequipoCount;
        protected CoralTickets.Server.Models.db_a905b1_coraldb.Equipo equiposForidequipoValue;

        [Inject]
        protected SecurityService Security { get; set; }
        protected async Task equiposForidequipoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await db_a905b1_coraldbService.GetEquipos();
                equiposForidequipo = result.Value.AsODataEnumerable();
                equiposForidequipoCount = equiposForidequipo.Count();

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Radzen.Design.EntityProperty" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await db_a905b1_coraldbService.UpdateMantenimiento(idMantenimiento:idMantenimiento, mantenimiento);
                DialogService.Close(mantenimiento);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}