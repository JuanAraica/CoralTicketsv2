
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace CoralTickets.Client
{
    public partial class db_a905b1_coraldbService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public db_a905b1_coraldbService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/db_a905b1_coraldb/");
        }


        public async System.Threading.Tasks.Task ExportCoralticketsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/coraltickets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/coraltickets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCoralticketsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/coraltickets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/coraltickets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCoraltickets(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket>> GetCoraltickets(Query query)
        {
            return await GetCoraltickets(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket>> GetCoraltickets(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Coraltickets");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCoraltickets(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket>>(response);
        }

        partial void OnCreateCoralticket(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> CreateCoralticket(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket coralticket = default(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket))
        {
            var uri = new Uri(baseUri, $"Coraltickets");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(coralticket), Encoding.UTF8, "application/json");

            OnCreateCoralticket(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket>(response);
        }

        partial void OnDeleteCoralticket(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCoralticket(int idTicket = default(int))
        {
            var uri = new Uri(baseUri, $"Coraltickets({idTicket})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCoralticket(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCoralticketByIdTicket(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> GetCoralticketByIdTicket(string expand = default(string), int idTicket = default(int))
        {
            var uri = new Uri(baseUri, $"Coraltickets({idTicket})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCoralticketByIdTicket(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket>(response);
        }

        partial void OnUpdateCoralticket(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCoralticket(int idTicket = default(int), CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket coralticket = default(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket))
        {
            var uri = new Uri(baseUri, $"Coraltickets({idTicket})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(coralticket), Encoding.UTF8, "application/json");

            OnUpdateCoralticket(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEquiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/equipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/equipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEquiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/equipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/equipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEquipos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo>> GetEquipos(Query query)
        {
            return await GetEquipos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo>> GetEquipos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Equipos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEquipos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo>>(response);
        }

        partial void OnCreateEquipo(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> CreateEquipo(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo equipo = default(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo))
        {
            var uri = new Uri(baseUri, $"Equipos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(equipo), Encoding.UTF8, "application/json");

            OnCreateEquipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo>(response);
        }

        partial void OnDeleteEquipo(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEquipo(int idequipo = default(int))
        {
            var uri = new Uri(baseUri, $"Equipos({idequipo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEquipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEquipoByIdequipo(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> GetEquipoByIdequipo(string expand = default(string), int idequipo = default(int))
        {
            var uri = new Uri(baseUri, $"Equipos({idequipo})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEquipoByIdequipo(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo>(response);
        }

        partial void OnUpdateEquipo(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEquipo(int idequipo = default(int), CoralTickets.Server.Models.db_a905b1_coraldb.Equipo equipo = default(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo))
        {
            var uri = new Uri(baseUri, $"Equipos({idequipo})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(equipo), Encoding.UTF8, "application/json");

            OnUpdateEquipo(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportHistoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/histories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/histories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportHistoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/histories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/histories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetHistories(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.History>> GetHistories(Query query)
        {
            return await GetHistories(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.History>> GetHistories(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Histories");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHistories(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.History>>(response);
        }

        partial void OnCreateHistory(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.History> CreateHistory(CoralTickets.Server.Models.db_a905b1_coraldb.History history = default(CoralTickets.Server.Models.db_a905b1_coraldb.History))
        {
            var uri = new Uri(baseUri, $"Histories");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(history), Encoding.UTF8, "application/json");

            OnCreateHistory(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.History>(response);
        }

        partial void OnDeleteHistory(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteHistory(int idHistory = default(int))
        {
            var uri = new Uri(baseUri, $"Histories({idHistory})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteHistory(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetHistoryByIdHistory(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.History> GetHistoryByIdHistory(string expand = default(string), int idHistory = default(int))
        {
            var uri = new Uri(baseUri, $"Histories({idHistory})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHistoryByIdHistory(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.History>(response);
        }

        partial void OnUpdateHistory(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateHistory(int idHistory = default(int), CoralTickets.Server.Models.db_a905b1_coraldb.History history = default(CoralTickets.Server.Models.db_a905b1_coraldb.History))
        {
            var uri = new Uri(baseUri, $"Histories({idHistory})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(history), Encoding.UTF8, "application/json");

            OnUpdateHistory(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMantenimientosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/mantenimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/mantenimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMantenimientosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/mantenimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/mantenimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMantenimientos(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>> GetMantenimientos(Query query)
        {
            return await GetMantenimientos(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>> GetMantenimientos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Mantenimientos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMantenimientos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>>(response);
        }

        partial void OnCreateMantenimiento(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> CreateMantenimiento(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento mantenimiento = default(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento))
        {
            var uri = new Uri(baseUri, $"Mantenimientos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mantenimiento), Encoding.UTF8, "application/json");

            OnCreateMantenimiento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>(response);
        }

        partial void OnDeleteMantenimiento(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMantenimiento(int idMantenimiento = default(int))
        {
            var uri = new Uri(baseUri, $"Mantenimientos({idMantenimiento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMantenimiento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMantenimientoByIdMantenimiento(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> GetMantenimientoByIdMantenimiento(string expand = default(string), int idMantenimiento = default(int))
        {
            var uri = new Uri(baseUri, $"Mantenimientos({idMantenimiento})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMantenimientoByIdMantenimiento(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>(response);
        }

        partial void OnUpdateMantenimiento(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMantenimiento(int idMantenimiento = default(int), CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento mantenimiento = default(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento))
        {
            var uri = new Uri(baseUri, $"Mantenimientos({idMantenimiento})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(mantenimiento), Encoding.UTF8, "application/json");

            OnUpdateMantenimiento(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTicketUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/ticketusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/ticketusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTicketUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/ticketusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/ticketusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTicketUsers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser>> GetTicketUsers(Query query)
        {
            return await GetTicketUsers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser>> GetTicketUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TicketUsers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTicketUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser>>(response);
        }

        partial void OnCreateTicketUser(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> CreateTicketUser(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser ticketUser = default(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser))
        {
            var uri = new Uri(baseUri, $"TicketUsers");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(ticketUser), Encoding.UTF8, "application/json");

            OnCreateTicketUser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser>(response);
        }

        partial void OnDeleteTicketUser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTicketUser(int ticketUser1 = default(int))
        {
            var uri = new Uri(baseUri, $"TicketUsers({ticketUser1})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTicketUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTicketUserByTicketUser1(HttpRequestMessage requestMessage);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> GetTicketUserByTicketUser1(string expand = default(string), int ticketUser1 = default(int))
        {
            var uri = new Uri(baseUri, $"TicketUsers({ticketUser1})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTicketUserByTicketUser1(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser>(response);
        }

        partial void OnUpdateTicketUser(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTicketUser(int ticketUser1 = default(int), CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser ticketUser = default(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser))
        {
            var uri = new Uri(baseUri, $"TicketUsers({ticketUser1})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(ticketUser), Encoding.UTF8, "application/json");

            OnUpdateTicketUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}