using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using CoralTickets.Server.Data;

namespace CoralTickets.Server
{
    public partial class db_a905b1_coraldbService
    {
        db_a905b1_coraldbContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly db_a905b1_coraldbContext context;
        private readonly NavigationManager navigationManager;

        public db_a905b1_coraldbService(db_a905b1_coraldbContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);


        public async Task ExportCoralticketsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/coraltickets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/coraltickets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCoralticketsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/coraltickets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/coraltickets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCoralticketsRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> items);

        public async Task<IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket>> GetCoraltickets(Query query = null)
        {
            var items = Context.Coraltickets.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCoralticketsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCoralticketGet(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> GetCoralticketByIdTicket(int idticket)
        {
            var items = Context.Coraltickets
                              .AsNoTracking()
                              .Where(i => i.idTicket == idticket);

  
            var itemToReturn = items.FirstOrDefault();

            OnCoralticketGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCoralticketCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);
        partial void OnAfterCoralticketCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> CreateCoralticket(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket coralticket)
        {
            OnCoralticketCreated(coralticket);

            var existingItem = Context.Coraltickets
                              .Where(i => i.idTicket == coralticket.idTicket)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Coraltickets.Add(coralticket);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(coralticket).State = EntityState.Detached;
                throw;
            }

            OnAfterCoralticketCreated(coralticket);

            return coralticket;
        }

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> CancelCoralticketChanges(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCoralticketUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);
        partial void OnAfterCoralticketUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> UpdateCoralticket(int idticket, CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket coralticket)
        {
            OnCoralticketUpdated(coralticket);

            var itemToUpdate = Context.Coraltickets
                              .Where(i => i.idTicket == coralticket.idTicket)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(coralticket);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCoralticketUpdated(coralticket);

            return coralticket;
        }

        partial void OnCoralticketDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);
        partial void OnAfterCoralticketDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> DeleteCoralticket(int idticket)
        {
            var itemToDelete = Context.Coraltickets
                              .Where(i => i.idTicket == idticket)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCoralticketDeleted(itemToDelete);


            Context.Coraltickets.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCoralticketDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEquiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/equipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/equipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEquiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/equipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/equipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEquiposRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> items);

        public async Task<IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo>> GetEquipos(Query query = null)
        {
            var items = Context.Equipos.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnEquiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEquipoGet(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> GetEquipoByIdequipo(int idequipo)
        {
            var items = Context.Equipos
                              .AsNoTracking()
                              .Where(i => i.idequipo == idequipo);

  
            var itemToReturn = items.FirstOrDefault();

            OnEquipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEquipoCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);
        partial void OnAfterEquipoCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> CreateEquipo(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo equipo)
        {
            OnEquipoCreated(equipo);

            var existingItem = Context.Equipos
                              .Where(i => i.idequipo == equipo.idequipo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Equipos.Add(equipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(equipo).State = EntityState.Detached;
                throw;
            }

            OnAfterEquipoCreated(equipo);

            return equipo;
        }

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> CancelEquipoChanges(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEquipoUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);
        partial void OnAfterEquipoUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> UpdateEquipo(int idequipo, CoralTickets.Server.Models.db_a905b1_coraldb.Equipo equipo)
        {
            OnEquipoUpdated(equipo);

            var itemToUpdate = Context.Equipos
                              .Where(i => i.idequipo == equipo.idequipo)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(equipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEquipoUpdated(equipo);

            return equipo;
        }

        partial void OnEquipoDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);
        partial void OnAfterEquipoDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> DeleteEquipo(int idequipo)
        {
            var itemToDelete = Context.Equipos
                              .Where(i => i.idequipo == idequipo)
                              .Include(i => i.Mantenimientos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEquipoDeleted(itemToDelete);


            Context.Equipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEquipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportHistoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/histories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/histories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportHistoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/histories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/histories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnHistoriesRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.History> items);

        public async Task<IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.History>> GetHistories(Query query = null)
        {
            var items = Context.Histories.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnHistoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnHistoryGet(CoralTickets.Server.Models.db_a905b1_coraldb.History item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.History> GetHistoryByIdHistory(int idhistory)
        {
            var items = Context.Histories
                              .AsNoTracking()
                              .Where(i => i.idHistory == idhistory);

  
            var itemToReturn = items.FirstOrDefault();

            OnHistoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnHistoryCreated(CoralTickets.Server.Models.db_a905b1_coraldb.History item);
        partial void OnAfterHistoryCreated(CoralTickets.Server.Models.db_a905b1_coraldb.History item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.History> CreateHistory(CoralTickets.Server.Models.db_a905b1_coraldb.History history)
        {
            OnHistoryCreated(history);

            var existingItem = Context.Histories
                              .Where(i => i.idHistory == history.idHistory)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Histories.Add(history);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(history).State = EntityState.Detached;
                throw;
            }

            OnAfterHistoryCreated(history);

            return history;
        }

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.History> CancelHistoryChanges(CoralTickets.Server.Models.db_a905b1_coraldb.History item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnHistoryUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.History item);
        partial void OnAfterHistoryUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.History item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.History> UpdateHistory(int idhistory, CoralTickets.Server.Models.db_a905b1_coraldb.History history)
        {
            OnHistoryUpdated(history);

            var itemToUpdate = Context.Histories
                              .Where(i => i.idHistory == history.idHistory)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(history);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterHistoryUpdated(history);

            return history;
        }

        partial void OnHistoryDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.History item);
        partial void OnAfterHistoryDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.History item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.History> DeleteHistory(int idhistory)
        {
            var itemToDelete = Context.Histories
                              .Where(i => i.idHistory == idhistory)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnHistoryDeleted(itemToDelete);


            Context.Histories.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterHistoryDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMantenimientosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/mantenimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/mantenimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMantenimientosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/mantenimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/mantenimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMantenimientosRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> items);

        public async Task<IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>> GetMantenimientos(Query query = null)
        {
            var items = Context.Mantenimientos.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnMantenimientosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMantenimientoGet(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> GetMantenimientoByIdMantenimiento(int idmantenimiento)
        {
            var items = Context.Mantenimientos
                              .AsNoTracking()
                              .Where(i => i.idMantenimiento == idmantenimiento);

                items = items.Include(i => i.Equipo);
  
            var itemToReturn = items.FirstOrDefault();

            OnMantenimientoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMantenimientoCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);
        partial void OnAfterMantenimientoCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> CreateMantenimiento(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento mantenimiento)
        {
            OnMantenimientoCreated(mantenimiento);

            var existingItem = Context.Mantenimientos
                              .Where(i => i.idMantenimiento == mantenimiento.idMantenimiento)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Mantenimientos.Add(mantenimiento);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(mantenimiento).State = EntityState.Detached;
                throw;
            }

            OnAfterMantenimientoCreated(mantenimiento);

            return mantenimiento;
        }

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> CancelMantenimientoChanges(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMantenimientoUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);
        partial void OnAfterMantenimientoUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> UpdateMantenimiento(int idmantenimiento, CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento mantenimiento)
        {
            OnMantenimientoUpdated(mantenimiento);

            var itemToUpdate = Context.Mantenimientos
                              .Where(i => i.idMantenimiento == mantenimiento.idMantenimiento)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(mantenimiento);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMantenimientoUpdated(mantenimiento);

            return mantenimiento;
        }

        partial void OnMantenimientoDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);
        partial void OnAfterMantenimientoDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> DeleteMantenimiento(int idmantenimiento)
        {
            var itemToDelete = Context.Mantenimientos
                              .Where(i => i.idMantenimiento == idmantenimiento)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMantenimientoDeleted(itemToDelete);


            Context.Mantenimientos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMantenimientoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTicketUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/ticketusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/ticketusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTicketUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_a905b1_coraldb/ticketusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_a905b1_coraldb/ticketusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTicketUsersRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> items);

        public async Task<IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser>> GetTicketUsers(Query query = null)
        {
            var items = Context.TicketUsers.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTicketUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTicketUserGet(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> GetTicketUserByTicketUser1(int ticketuser1)
        {
            var items = Context.TicketUsers
                              .AsNoTracking()
                              .Where(i => i.TicketUser1 == ticketuser1);

  
            var itemToReturn = items.FirstOrDefault();

            OnTicketUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTicketUserCreated(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);
        partial void OnAfterTicketUserCreated(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> CreateTicketUser(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser ticketuser)
        {
            OnTicketUserCreated(ticketuser);

            var existingItem = Context.TicketUsers
                              .Where(i => i.TicketUser1 == ticketuser.TicketUser1)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TicketUsers.Add(ticketuser);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(ticketuser).State = EntityState.Detached;
                throw;
            }

            OnAfterTicketUserCreated(ticketuser);

            return ticketuser;
        }

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> CancelTicketUserChanges(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTicketUserUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);
        partial void OnAfterTicketUserUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> UpdateTicketUser(int ticketuser1, CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser ticketuser)
        {
            OnTicketUserUpdated(ticketuser);

            var itemToUpdate = Context.TicketUsers
                              .Where(i => i.TicketUser1 == ticketuser.TicketUser1)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(ticketuser);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTicketUserUpdated(ticketuser);

            return ticketuser;
        }

        partial void OnTicketUserDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);
        partial void OnAfterTicketUserDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);

        public async Task<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> DeleteTicketUser(int ticketuser1)
        {
            var itemToDelete = Context.TicketUsers
                              .Where(i => i.TicketUser1 == ticketuser1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTicketUserDeleted(itemToDelete);


            Context.TicketUsers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTicketUserDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}