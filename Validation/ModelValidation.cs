using dotnet_api_test.Exceptions.ExceptionResponses;
using dotnet_api_test.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace dotnet_api_test.Validation
{
    public static class ModelValidation
    {
        public static void ValidateCreateDishDto(CreateDishDto entity)
        {
            var validationResults = new List<ValidationResult>();
            ValidateEntityPropertyIsNotNull(nameof(entity.Name), entity.Name, validationResults);
            ValidateEntityPropertyIsNotNull(nameof(entity.Cost), entity.Cost, validationResults);
            ValidateEntityPropertyIsNotNull(nameof(entity.MadeBy), entity.MadeBy, validationResults);

            if (validationResults.Count > 0)
                throw new BadRequestExceptionResponse("Model is not valid because following properties are missing: " +
                                                      string.Join(", ", validationResults.Select(s => s.ErrorMessage).ToArray()));
        }

        public static void ValidateUpdateDishDto(UpdateDishDto entity, double raise)
        {
            var validationResults = new List<ValidationResult>();
            ValidateEntityPropertyIsNotNull(nameof(entity.Name), entity.Name, validationResults);
            ValidateEntityProperytDoubleBiggerRule(nameof(entity.Cost), entity.Cost, validationResults, raise);
            ValidateEntityPropertyIsNotNull(nameof(entity.Cost), entity.Cost, validationResults);
            ValidateEntityPropertyIsNotNull(nameof(entity.MadeBy), entity.MadeBy, validationResults);

            if (validationResults.Count > 0)
                throw new BadRequestExceptionResponse("Model is not valid because following properties are missing: " +
                                                      string.Join(", ", validationResults.Select(s => s.ErrorMessage).ToArray()));
        }

        private static void ValidateEntityPropertyIsNotNull<T>(string entityName, T value, ICollection<ValidationResult> validationResults)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) validationResults.Add(new ValidationResult(entityName));
        }

        private static void ValidateEntityProperytDoubleBiggerRule(string entityName, double? value, ICollection<ValidationResult> validationResults, double value2)
        {
            if (value > (value2 * 1.2)) validationResults.Add(new ValidationResult(entityName + "\nBusiness rule - Price cant be raised with more than 20%"));
        }
    }
}