using Microsoft.AspNetCore.Mvc;
using OMS.Common.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Api.Validations
{
    public static class ValidationManager
    {
        public static IActionResult Validate<T>(T model, string methodName)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                var errors = validationResults
                    .GroupBy(v => v.MemberNames.FirstOrDefault())
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                return ResponseHelper.CreateValidationResponse(methodName, errors);
            }

            return null; // No validation errors
        }
    }
}
