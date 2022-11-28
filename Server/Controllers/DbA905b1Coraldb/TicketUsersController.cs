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
    [Route("odata/db_a905b1_coraldb/TicketUsers")]
    public partial class TicketUsersController : ODataController
    {
        private CoralTickets.Server.Data.db_a905b1_coraldbContext context;

        public TicketUsersController(CoralTickets.Server.Data.db_a905b1_coraldbContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> GetTicketUsers()
        {
            var items = this.context.TicketUsers.AsQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser>();
            this.OnTicketUsersRead(ref items);

            return items;
        }

        partial void OnTicketUsersRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> items);

        partial void OnTicketUserGet(ref SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/db_a905b1_coraldb/TicketUsers(TicketUser1={TicketUser1})")]
        public SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> GetTicketUser(int key)
        {
            var items = this.context.TicketUsers.Where(i => i.TicketUser1 == key);
            var result = SingleResult.Create(items);

            OnTicketUserGet(ref result);

            return result;
        }
        partial void OnTicketUserDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);
        partial void OnAfterTicketUserDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);

        [HttpDelete("/odata/db_a905b1_coraldb/TicketUsers(TicketUser1={TicketUser1})")]
        public IActionResult DeleteTicketUser(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.TicketUsers
                    .Where(i => i.TicketUser1 == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnTicketUserDeleted(item);
                this.context.TicketUsers.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTicketUserDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTicketUserUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);
        partial void OnAfterTicketUserUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);

        [HttpPut("/odata/db_a905b1_coraldb/TicketUsers(TicketUser1={TicketUser1})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTicketUser(int key, [FromBody]CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.TicketUser1 != key))
                {
                    return BadRequest();
                }
                this.OnTicketUserUpdated(item);
                this.context.TicketUsers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TicketUsers.Where(i => i.TicketUser1 == key);
                ;
                this.OnAfterTicketUserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/db_a905b1_coraldb/TicketUsers(TicketUser1={TicketUser1})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTicketUser(int key, [FromBody]Delta<CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.TicketUsers.Where(i => i.TicketUser1 == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnTicketUserUpdated(item);
                this.context.TicketUsers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TicketUsers.Where(i => i.TicketUser1 == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTicketUserCreated(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);
        partial void OnAfterTicketUserCreated(CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CoralTickets.Server.Models.db_a905b1_coraldb.TicketUser item)
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

                this.OnTicketUserCreated(item);
                this.context.TicketUsers.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TicketUsers.Where(i => i.TicketUser1 == item.TicketUser1);

                ;

                this.OnAfterTicketUserCreated(item);

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
