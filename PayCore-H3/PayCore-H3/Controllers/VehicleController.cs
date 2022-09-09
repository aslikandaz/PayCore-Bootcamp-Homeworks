using Microsoft.AspNetCore.Mvc;
using PayCore_H3.Context;
using PayCore_H3.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayCore_H3.Controllers
{
    [ApiController]
    [Route("api/nhb/[controller]")]
    public class VehicleController: ControllerBase
    {
        private readonly IMapperSession _session;
        public VehicleController(IMapperSession session)
        {
            _session = session;
        }

        [HttpGet]
        public List<Vehicle> Get()
        {
            List<Vehicle> result = _session.Vehicles.ToList();
            return result;
        }


        [HttpPost]
        public void Post([FromBody] Vehicle vehicle)
        {
            try
            {
                _session.BeginTransaction();
                _session.Save(vehicle);
                _session.Commit();
            }
            catch (Exception ex)
            {
                _session.Rollback();
                Log.Error(ex, "Vehicle Insert Error");
            }
            finally
            {
                _session.CloseTransaction();
            }
        }

        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            Vehicle vehicle = _session.Vehicles.Where(x => x.Id == request.Id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                _session.BeginTransaction();

                vehicle.Id = request.Id;
                vehicle.VehicleName = request.VehicleName;
                vehicle.VehiclePlate = request.VehiclePlate;

                _session.Update(vehicle);

                _session.Commit();
            }
            catch (Exception ex)
            {
                _session.Rollback();
                Log.Error(ex, "Vehicle Update Error");
            }
            finally
            {
                _session.CloseTransaction();
            }


            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Vehicle> Delete(int id)
        {
            Vehicle vehicle = _session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            List<Container> containers = _session.Containers.Where(x => x.VehicleId == id).ToList();
            if (vehicle == null)
            {
                return NotFound();
            }
            
            try
            {
                _session.BeginTransaction();
                foreach (var container in containers)
                {
                    _session.Delete(container);
                }
                _session.Delete(vehicle);
                _session.Commit();
            }
            catch (Exception ex)
            {
                _session.Rollback();
                Log.Error(ex, "Vehicle Delete Error");
            }
            finally
            {
                _session.CloseTransaction();
            }

            return Ok();
        }

    }
}
