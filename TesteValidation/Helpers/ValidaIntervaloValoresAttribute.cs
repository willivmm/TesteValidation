using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TesteValidation.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidaIntervaloValoresAttribute : ValidationAttribute, IClientValidatable
    {
        protected string MinValue { get; set; }
        protected string MaxValue { get; set; }

        public ValidaIntervaloValoresAttribute(string minValue, string maxValue)
            : base()
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            var valorMinimo = validationContext.ObjectType.GetProperty(MinValue).GetValue(validationContext.ObjectInstance, null);
            var valorMaximo = validationContext.ObjectType.GetProperty(MaxValue).GetValue(validationContext.ObjectInstance, null);

            if (value != null)
            {
                var valor = decimal.Parse(value.ToString());
                if (valorMinimo != null && valorMaximo != null)
                {
                    if (valor <= (decimal)valorMinimo || valor >= (decimal)valorMaximo)
                    {
                        result = new ValidationResult(ErrorMessage);
                    }
                }
            }

            return result;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "validaintervalovalores"
            };
            rule.ValidationParameters.Add("valorminimo", MinValue);
            rule.ValidationParameters.Add("valormaximo", MaxValue);
            yield return rule;
        }
    }
}