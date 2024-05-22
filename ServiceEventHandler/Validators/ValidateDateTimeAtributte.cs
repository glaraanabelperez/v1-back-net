using ServiceEventHandler.Command.CreateCommand;
using System.ComponentModel.DataAnnotations;

namespace ServiceEventHandler.Validators
{
    public class ValidateDateTimeAtributte : ValidationAttribute
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
                if (dateRange.dateInit >= dateRange.dateFinish)
                {
                    return new ValidationResult($"La fecha de inicio ({dateRange.dateInit}) no puede ser mayor que la fecha de fin ({dateRange.dateFinish}).");
                }
   
            }

            return ValidationResult.Success;
        }
    }

    public static class ValidateDateTime
    {
        public static  ValidationResult IsValiddateTime(DateTime dateInit, DateTime dateFinish)
        {

            if (dateInit >= dateFinish)
            {
                return new ValidationResult($"La fecha de inicio ({dateInit}) no puede ser mayor que la fecha de fin ({dateFinish}).");
            }
            return ValidationResult.Success;
        }
    }
}
