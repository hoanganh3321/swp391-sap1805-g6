using System.ComponentModel.DataAnnotations;
using swp391_sap1805_g6.Entities;

namespace swp391_sap1805_g6.Validations
{
    public class DurationEnsure : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var warranty = validationContext.ObjectInstance as Warranty;
                //code here
            return ValidationResult.Success;
        }
    }
}
