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
    [Route("odata/db_a905b1_coraldb/Equipos")]
    public partial class EquiposController : ODataController
    {
        Seguridad vigilante = new Seguridad();
        private CoralTickets.Server.Data.db_a905b1_coraldbContext context;

        public void RegistrarEvento(string mensaje)
        {
            CoralTickets.Server.Models.db_a905b1_coraldb.History histo = new Models.db_a905b1_coraldb.History();
            histo.Registro = mensaje;
            histo.Fecha = DateTime.UtcNow.ToString("MM-dd-yyyy");
            histo.Hora = DateTime.Now.ToString("hh:mm:ss");


            this.context.Histories.Add(histo);
            this.context.SaveChanges();

        }



        public EquiposController(CoralTickets.Server.Data.db_a905b1_coraldbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> GetEquipos()
        {
            var items = this.context.Equipos.AsQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo>();
            this.OnEquiposRead(ref items);

            return items;
        }

        partial void OnEquiposRead(ref IQueryable<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> items);

        partial void OnEquipoGet(ref SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/db_a905b1_coraldb/Equipos(idequipo={idequipo})")]
        public SingleResult<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> GetEquipo(int key)
        {
            var items = this.context.Equipos.Where(i => i.idequipo == key);
            var result = SingleResult.Create(items);

            OnEquipoGet(ref result);

            return result;
        }
        partial void OnEquipoDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);
        partial void OnAfterEquipoDeleted(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);

        [HttpDelete("/odata/db_a905b1_coraldb/Equipos(idequipo={idequipo})")]
        public IActionResult DeleteEquipo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Equipos
                    .Where(i => i.idequipo == key)
                    .Include(i => i.Mantenimientos)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnEquipoDeleted(item);
                this.context.Equipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEquipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEquipoUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);
        partial void OnAfterEquipoUpdated(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);

        [HttpPut("/odata/db_a905b1_coraldb/Equipos(idequipo={idequipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEquipo(int key, [FromBody]CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.idequipo != key))
                {
                    return BadRequest();
                }
                this.OnEquipoUpdated(item);
                this.context.Equipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Equipos.Where(i => i.idequipo == key);
                ;
                this.OnAfterEquipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/db_a905b1_coraldb/Equipos(idequipo={idequipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEquipo(int key, [FromBody]Delta<CoralTickets.Server.Models.db_a905b1_coraldb.Equipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Equipos.Where(i => i.idequipo == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnEquipoUpdated(item);
                this.context.Equipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Equipos.Where(i => i.idequipo == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }






        partial void OnEquipoCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);
        partial void OnAfterEquipoCreated(CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CoralTickets.Server.Models.db_a905b1_coraldb.Equipo item)
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
                RegistrarEvento("Se ha agregado un/una " + item.TipoEquipo + ", marca: " + item.Marca + ", Modelo " + item.Modelo + ", para " + item.Poseedor + " " + item.Ubicacion);



                this.OnEquipoCreated(item);
                this.context.Equipos.Add(item);
                this.context.SaveChanges();


                var itemToReturn = this.context.Equipos.Where(i => i.idequipo == item.idequipo);

                ;

                this.OnAfterEquipoCreated(item);

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
