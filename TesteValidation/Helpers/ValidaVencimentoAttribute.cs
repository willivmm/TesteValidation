using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TesteValidation.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidaVencimentoAttribute : ValidationAttribute, IClientValidatable
    {
        protected string OutroAtributo { get; private set; }

        public ValidaVencimentoAttribute(string outroAtributo)
        {
            OutroAtributo = outroAtributo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            var outroValor = validationContext.ObjectType.GetProperty(OutroAtributo).GetValue(validationContext.ObjectInstance, null);
            int anoAtual = DateTime.Now.Year;
            int mesAtual = DateTime.Now.Month;

            if (value != null)
            {
                if (outroValor != null)
                {
                    if (!OutroAtributo.ToLower().Contains("mes"))
                    {
                        if (mesAtual > (int)value && anoAtual >= (int)outroValor)
                        {
                            result = new ValidationResult(ErrorMessage);
                        }
                    }
                    else
                    {
                        if (mesAtual > (int)outroValor && anoAtual >= (int)value)
                        {
                            result = new ValidationResult(ErrorMessage);
                        }
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
                ValidationType = "validavencimento"
            };
            rule.ValidationParameters.Add("outroatributo", OutroAtributo);
            yield return rule;
        }
    }
}