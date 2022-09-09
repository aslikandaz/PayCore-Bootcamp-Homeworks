using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterestCalculator.Models
{
    public class InterestResponseModel
    //created class to return results
    {
        public double InterestRate { get; set; }
        public int InterestYield { get; set; }
        public int TotalBalance { get; set; }
    }
}
