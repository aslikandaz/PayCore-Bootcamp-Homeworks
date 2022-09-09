using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterestCalculator.Models
{
    //The class that will meet the data received from the body
    public class InterestRequestModel
    {
        [Required(ErrorMessage = "Please enter Principal Amount ")]
        [Range(0, double.MaxValue,ErrorMessage = "Please enter a valid number")] 
        public double Principal { get; set; }

        [Required(ErrorMessage = "Please enter  Interest Rate")]
        [Range(0,1, ErrorMessage = "Please enter a valid number")]
        public double InterestRate { get; set; }

        [Required(ErrorMessage = "Please enter Duration")]
        [Range(1,double.MaxValue, ErrorMessage = "Please enter a valid number")] //The minimum value is 1 because compound interest is applied to loan transactions with a duration of more than 1 year.
        public double Duration { get; set; }

    }
}
