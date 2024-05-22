using ServiceEventHandler.Command.CreateCommand;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Validators
{
    public class ValidateDateTime : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateRanges = value as List<Rangedate>;
            if (dateRanges == null)
            {
                return new ValidationResult("El campo DateTimes es obligatorio.");
            }

            foreach (var dateRange in dateRanges)
            {
                if (dateRange.dateInit > dateRange.dateFinish)
                {
                    return new ValidationResult($"La fecha de inicio ({dateRange.dateInit}) no puede ser mayor que la fecha de fin ({dateRange.dateFinish}).");
                }
            }

            return ValidationResult.Success;
        }
    }
}
