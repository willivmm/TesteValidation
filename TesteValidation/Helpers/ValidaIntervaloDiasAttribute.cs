using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteValidation.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidaIntervaloDiasAttribute : ValidationAttribute, IClientValidatable
    {
        protected int FirstDateDay { get; set; }
        protected int SecondDateDay { get; set; }

        public ValidaIntervaloDiasAttribute(int firstDateDay, int secondDateDay)
            : base()
        {
            FirstDateDay = firstDateDay;
            SecondDateDay = secondDateDay;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            DateTime dateStart = DateTime.Now.AddDays(FirstDateDay).Date;
            DateTime dateEnd = DateTime.Now.AddDays(SecondDateDay).Date;
            DateTime date;

            if (value == null)
                return result;

            if (DateTime.TryParse(value.ToString(), out date))
            {
                if (date < dateStart && date > dateEnd)
                    return new ValidationResult(ErrorMessage);
            }
            else
                return new ValidationResult(ErrorMessage);

            return result;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "validaintervalodias"
            };
            rule.ValidationParameters.Add("diainicial", FirstDateDay);
            rule.ValidationParameters.Add("diafinal", SecondDateDay);
            yield return rule;
        }
    }
}