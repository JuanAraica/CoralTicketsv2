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
    [Route("odata/db_a905b1_coraldb/Coraltickets")]
    public partial class CoralticketsController : ODataController
    {
        private CoralTickets.Server.Data.db_a905b1_coraldbContext context;

        public CoralticketsController(CoralTickets.Server.Data.db_a905b1_coraldbContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> GetCoraltickets()
        {
            var items = this.context.Coraltickets.AsQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket>();
            this.OnCoralticketsRead(ref items);

            return items;
        }

        partial void OnCoralticketsRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> items);

        partial void OnCoralticketGet(ref SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/db_a905b1_coraldb/Coraltickets(idTicket={idTicket})")]
        public SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> GetCoralticket(int key)
        {
            var items = this.context.Coraltickets.Where(i => i.idTicket == key);
            var result = SingleResult.Create(items);

            OnCoralticketGet(ref result);

            return result;
        }
        partial void OnCoralticketDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);
        partial void OnAfterCoralticketDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);

        [HttpDelete("/odata/db_a905b1_coraldb/Coraltickets(idTicket={idTicket})")]
        public IActionResult DeleteCoralticket(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Coraltickets
                    .Where(i => i.idTicket == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnCoralticketDeleted(item);
                this.context.Coraltickets.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCoralticketDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCoralticketUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);
        partial void OnAfterCoralticketUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);

        [HttpPut("/odata/db_a905b1_coraldb/Coraltickets(idTicket={idTicket})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCoralticket(int key, [FromBody]CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.idTicket != key))
                {
                    return BadRequest();
                }
                this.OnCoralticketUpdated(item);
                this.context.Coraltickets.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Coraltickets.Where(i => i.idTicket == key);
                ;
                this.OnAfterCoralticketUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/db_a905b1_coraldb/Coraltickets(idTicket={idTicket})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCoralticket(int key, [FromBody]Delta<CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Coraltickets.Where(i => i.idTicket == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnCoralticketUpdated(item);
                this.context.Coraltickets.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Coraltickets.Where(i => i.idTicket == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCoralticketCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);
        partial void OnAfterCoralticketCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CoralTickets.Server.Models.db_a905b1_coraldb.Coralticket item)
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

                this.OnCoralticketCreated(item);
                this.context.Coraltickets.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Coraltickets.Where(i => i.idTicket == item.idTicket);

                ;

                this.OnAfterCoralticketCreated(item);

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
