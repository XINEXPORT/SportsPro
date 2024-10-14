using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsPro.Models
{
    public class Country
    {
        [Required(ErrorMessage = "Please enter a country ID.")]
        [ValidateNever]
        public string CountryID { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a country name.")]
        public string Name { get; set; } = string.Empty;

        [ValidateNever]
        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    }
}
