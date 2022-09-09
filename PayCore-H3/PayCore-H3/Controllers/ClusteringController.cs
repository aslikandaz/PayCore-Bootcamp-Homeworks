using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayCore_H3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NHibernate.Linq.Functions;
using PayCore_H3.Model;

namespace PayCore_H3.Controllers
{
    [Route("api/nhb/[controller]")]
    [ApiController]
    public class ClusteringController : ControllerBase
    {

      
        private readonly IMapperSession _session;
        public ClusteringController(IMapperSession session)
        {
            _session = session;
        }

        [HttpGet]
        public ActionResult Get(int VehicleId, int NumberOfCluster)
        {

            List<Container> results = _session.Containers.Where(x=>x.Id == VehicleId).ToList();
            List<List<Container>> response = new List<List<Container>>(NumberOfCluster);

             response = results.Select((x, i) => new {Index = i, value = x})
                .GroupBy(x => x.Index % NumberOfCluster)
                .Select(x => x.Select(v => v.value).ToList())
                .ToList();

            return Ok(response);
        }

    }
}
