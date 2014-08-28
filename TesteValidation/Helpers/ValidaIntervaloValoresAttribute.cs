using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TesteValidation.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidaIntervaloValoresAttribute : ValidationAttribute, IClientValidatable
    {
        protected string ValorMinimo { get; set; }
        protected string ValorMaximo { get; set; }

        public ValidaIntervaloValoresAttribute(string valorMinimo, string valorMaximo)
            : base()
        {
            ValorMinimo = valorMinimo;
            ValorMaximo = valorMaximo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            var valorMinimo = validationContext.ObjectType.GetProperty(ValorMinimo).GetValue(validationContext.ObjectInstance, null);
            var valorMaximo = validationContext.ObjectType.GetProperty(ValorMaximo).GetValue(validationContext.ObjectInstance, null);

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
            rule.ValidationParameters.Add("valorminimo", ValorMinimo);
            rule.ValidationParameters.Add("valormaximo", ValorMaximo);
            yield return rule;
        }
    }
}