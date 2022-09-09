using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayCore_H3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayCore_H3.Model;
using Serilog;

namespace PayCore_H3.Controllers
{
    [Route("api/nhb/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IMapperSession _session;
        public ContainerController(IMapperSession session)
        {
            _session = session;
        }


        [HttpGet]
        public List<Container> Get()
        {
            List<Container> result = _session.Containers.ToList();
            return result;
        }

        //girilen araçId sine göre aracın sahip olduğu contaainerları getiren metot
        [HttpGet("{VehicleId}")]
        public List<Container> GetByContainerId(int VehicleId)
        {
            List<Container> result = _session.Containers.Where(x=>x.VehicleId==VehicleId).ToList();
            return result;
        }


        [HttpPost]
        public void Post([FromBody] Container container)
        {
            try
            {
                _session.BeginTransaction();
                _session.Save(container);
                _session.Commit();
            }
            catch (Exception ex)
            {
                _session.Rollback();
                Log.Error(ex, "Container Insert Error");
            }
            finally
            {
                _session.CloseTransaction();
            }
        }

        [HttpPut]
        public ActionResult<Container> Put([FromBody] Container request)
        {
            Container container = _session.Containers.Where(x => x.Id == request.Id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                _session.BeginTransaction();

                container.Id = request.Id;
                container.ContainerName = request.ContainerName;
                container.Latitude = request.Latitude;
                container.Longitude = request.Longitude;
                container.VehicleId = container.VehicleId; //containerID nin güncellenmemesi sağlanıyor

                _session.Update(container);

                _session.Commit();
            }
            catch (Exception ex)
            {
                _session.Rollback();
                Log.Error(ex, "Container Update Error");
            }
            finally
            {
                _session.CloseTransaction();
            }


            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Container> Delete(int id)
        {
            Container container = _session.Containers.Where(x => x.Id== id).FirstOrDefault();
            
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                _session.BeginTransaction();
                _session.Delete(container);
                _session.Commit();
            }
            catch (Exception ex)
            {
                _session.Rollback();
                Log.Error(ex, "Container Delete Error");
            }
            finally
            {
                _session.CloseTransaction();
            }

            return Ok();
        }

    }
}
