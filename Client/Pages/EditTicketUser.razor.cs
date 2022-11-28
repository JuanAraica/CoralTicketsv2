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
    public partial class EditTicketUser
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
        public int TicketUser1 { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ticketUser = await db_a905b1_coraldbService.GetTicketUserByTicketUser1(ticketUser1:TicketUser1);
        }
        protected bool errorVisible;
        protected CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser ticketUser;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await db_a905b1_coraldbService.UpdateTicketUser(ticketUser1:TicketUser1, ticketUser);
                DialogService.Close(ticketUser);
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