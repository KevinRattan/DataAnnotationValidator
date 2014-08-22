using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnnotationValidatorTestWeb.Models
{
    public class TestModel
    {

        public static TestModel GetTestModel()
        {
            return new TestModel();
        }

        [StringLength(10, ErrorMessage="Name must not exceed 10 characters")]
        [Required(ErrorMessage = "The name must be filled in")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage="The email must be a valid email address")]
        [Required(ErrorMessage = "The email must be filled in")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "The phone number must be a valid telephone number")]
        [Required(ErrorMessage = "The tel number must be filled in")]
        public string Telephone { get; set; }

        [Url(ErrorMessage="The url must be a valid internet url")]
        [Required(ErrorMessage = "The url must be filled in")]
        public string Url { get; set; }

        [Range(0, 150, ErrorMessage="Age must be realistic")]
        [Required(ErrorMessage = "The age must be filled in")]
        public string Age { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage="Must be a valid zip code")]
        [MinLength(5, ErrorMessage="Must be at least 5 characters")]
        [MaxLength(10, ErrorMessage="Cannot be more than 10 characters long")]
        [Required(ErrorMessage = "The zipcode must be filled in")]
        public string ZipCode { get; set; }

        [CreditCard(ErrorMessage = "CreditCard must be a credit card")]
        [Required(ErrorMessage = "The credit card must be filled in")]
        public string CreditCard { get; set; }



    }
}
