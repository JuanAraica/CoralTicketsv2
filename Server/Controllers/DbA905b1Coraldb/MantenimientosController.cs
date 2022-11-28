using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoralTickets.Server.Controllers.db_a905b1_coraldb
{
    [Route("odata/db_a905b1_coraldb/Mantenimientos")]
    public partial class MantenimientosController : ODataController
    {
        private CoralTickets.Server.Data.db_a905b1_coraldbContext context;

        public MantenimientosController(CoralTickets.Server.Data.db_a905b1_coraldbContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> GetMantenimientos()
        {
            var items = this.context.Mantenimientos.AsQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento>();
            this.OnMantenimientosRead(ref items);

            return items;
        }

        partial void OnMantenimientosRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> items);

        partial void OnMantenimientoGet(ref SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/db_a905b1_coraldb/Mantenimientos(idMantenimiento={idMantenimiento})")]
        public SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> GetMantenimiento(int key)
        {
            var items = this.context.Mantenimientos.Where(i => i.idMantenimiento == key);
            var result = SingleResult.Create(items);

            OnMantenimientoGet(ref result);

            return result;
        }
        partial void OnMantenimientoDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);
        partial void OnAfterMantenimientoDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);

        [HttpDelete("/odata/db_a905b1_coraldb/Mantenimientos(idMantenimiento={idMantenimiento})")]
        public IActionResult DeleteMantenimiento(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Mantenimientos
                    .Where(i => i.idMantenimiento == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnMantenimientoDeleted(item);
                this.context.Mantenimientos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMantenimientoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMantenimientoUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);
        partial void OnAfterMantenimientoUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);

        [HttpPut("/odata/db_a905b1_coraldb/Mantenimientos(idMantenimiento={idMantenimiento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMantenimiento(int key, [FromBody]CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.idMantenimiento != key))
                {
                    return BadRequest();
                }
                this.OnMantenimientoUpdated(item);
                this.context.Mantenimientos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Mantenimientos.Where(i => i.idMantenimiento == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Equipo");
                this.OnAfterMantenimientoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/db_a905b1_coraldb/Mantenimientos(idMantenimiento={idMantenimiento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMantenimiento(int key, [FromBody]Delta<CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Mantenimientos.Where(i => i.idMantenimiento == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnMantenimientoUpdated(item);
                this.context.Mantenimientos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Mantenimientos.Where(i => i.idMantenimiento == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Equipo");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMantenimientoCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);
        partial void OnAfterMantenimientoCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CoralTickets.Server.Models.db_a905b1_coraldb.Mantenimiento item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnMantenimientoCreated(item);
                this.context.Mantenimientos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Mantenimientos.Where(i => i.idMantenimiento == item.idMantenimiento);

                Request.QueryString = Request.QueryString.Add("$expand", "Equipo");

                this.OnAfterMantenimientoCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
