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
    public partial class EditCoralticket
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
        public int idTicket { get; set; }

        protected override async Task OnInitializedAsync()
        {
            coralticket = await db_a905b1_coraldbService.GetCoralticketByIdTicket(idTicket:idTicket);
        }
        protected bool errorVisible;
        protected CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket coralticket;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await db_a905b1_coraldbService.UpdateCoralticket(idTicket:idTicket, coralticket);
                DialogService.Close(coralticket);
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