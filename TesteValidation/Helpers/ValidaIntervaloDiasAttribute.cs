using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TesteValidation.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidaIntervaloDiasAttribute : ValidationAttribute, IClientValidatable
    {
        protected int DiaInicial { get; set; }
        protected int DiaFinal { get; set; }

        public ValidaIntervaloDiasAttribute(int diaInicial, int diaFinal)
            : base()
        {
            DiaInicial = diaInicial;
            DiaFinal = diaFinal;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            DateTime diaInicial = DateTime.Now.AddDays(DiaInicial).Date;
            DateTime diaFinal = DateTime.Now.AddDays(DiaFinal).Date;
            DateTime data;

            if (value == null)
                return result;

            if (DateTime.TryParse(value.ToString(), out data))
            {
                if (data < diaInicial && data > diaFinal)
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
            rule.ValidationParameters.Add("diainicial", DiaInicial);
            rule.ValidationParameters.Add("diafinal", DiaFinal);
            yield return rule;
        }
    }
}