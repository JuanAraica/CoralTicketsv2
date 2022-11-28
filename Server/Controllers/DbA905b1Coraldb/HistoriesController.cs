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
    [Route("odata/db_a905b1_coraldb/Histories")]
    public partial class HistoriesController : ODataController
    {
        private CoralTickets.Server.Data.db_a905b1_coraldbContext context;

        public HistoriesController(CoralTickets.Server.Data.db_a905b1_coraldbContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CoralTickets.Server.Models.db_a905b1_coraldb.History> GetHistories()
        {
            var items = this.context.Histories.AsQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.History>();
            this.OnHistoriesRead(ref items);

            return items;
        }

        partial void OnHistoriesRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.History> items);

        partial void OnHistoryGet(ref SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.History> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/db_a905b1_coraldb/Histories(idHistory={idHistory})")]
        public SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.History> GetHistory(int key)
        {
            var items = this.context.Histories.Where(i => i.idHistory == key);
            var result = SingleResult.Create(items);

            OnHistoryGet(ref result);

            return result;
        }
        partial void OnHistoryDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.History item);
        partial void OnAfterHistoryDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.History item);

        [HttpDelete("/odata/db_a905b1_coraldb/Histories(idHistory={idHistory})")]
        public IActionResult DeleteHistory(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Histories
                    .Where(i => i.idHistory == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnHistoryDeleted(item);
                this.context.Histories.Remove(item);
                this.context.SaveChanges();
                this.OnAfterHistoryDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHistoryUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.History item);
        partial void OnAfterHistoryUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.History item);

        [HttpPut("/odata/db_a905b1_coraldb/Histories(idHistory={idHistory})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutHistory(int key, [FromBody]CoralTickets.Server.Models.db_a905b1_coraldb.History item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.idHistory != key))
                {
                    return BadRequest();
                }
                this.OnHistoryUpdated(item);
                this.context.Histories.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Histories.Where(i => i.idHistory == key);
                ;
                this.OnAfterHistoryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/db_a905b1_coraldb/Histories(idHistory={idHistory})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchHistory(int key, [FromBody]Delta<CoralTickets.Server.Models.db_a905b1_coraldb.History> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Histories.Where(i => i.idHistory == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnHistoryUpdated(item);
                this.context.Histories.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Histories.Where(i => i.idHistory == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHistoryCreated(CoralTickets.Server.Models.db_a905b1_coraldb.History item);
        partial void OnAfterHistoryCreated(CoralTickets.Server.Models.db_a905b1_coraldb.History item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CoralTickets.Server.Models.db_a905b1_coraldb.History item)
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

                this.OnHistoryCreated(item);
                this.context.Histories.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Histories.Where(i => i.idHistory == item.idHistory);

                ;

                this.OnAfterHistoryCreated(item);

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
