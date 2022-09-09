using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterestCalculator.Models;

namespace InterestCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestCalculatorController : ControllerBase
    {
        [HttpPost]
        public ActionResult<InterestResponseModel> InterestCalculator([FromBody] InterestRequestModel interestRequestModel)
        {
            InterestResponseModel interestResponseModel = new InterestResponseModel();

            var principal = interestRequestModel.Principal;
            var interestRate = interestRequestModel.InterestRate;
            var duration = interestRequestModel.Duration;

            interestResponseModel.InterestRate = interestRequestModel.InterestRate;
            interestResponseModel.InterestRate = interestRate;
            interestResponseModel.TotalBalance = Convert.ToInt32(principal * Math.Pow(1 + interestRate, duration)); // compound interest calculation
            interestResponseModel.InterestYield = Convert.ToInt32(interestResponseModel.TotalBalance - principal);  

            return Ok(interestResponseModel);


        }
    }
}
