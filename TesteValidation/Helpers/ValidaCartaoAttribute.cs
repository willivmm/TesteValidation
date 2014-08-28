using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace TesteValidation.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidaCartaoAttribute : ValidationAttribute, IClientValidatable
    {
        protected string Bandeira;

        public ValidaCartaoAttribute(string bandeira)
            : base()
        {
            Bandeira = bandeira;
        }

        public enum CardType
        {
            Outro = 0,
            Visa = 1,
            MasterCard = 2,
            Amex = 5,
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object item = validationContext.ObjectInstance;
            int tipo = (int)item.GetType().GetProperty(Bandeira).GetValue(item, null);
            bool valido = false;
            string cartao = (string)value;

            if (!String.IsNullOrEmpty(cartao))
            {
                switch ((CardType)tipo)
                {
                    case CardType.Visa:
                        valido = Regex.IsMatch(cartao, "^(4)") && (cartao.Length == 13 || cartao.Length == 16);
                        break;

                    case CardType.MasterCard:
                        valido = Regex.IsMatch(cartao, "^(51|52|53|54|55)") && cartao.Length == 16;
                        break;

                    case CardType.Amex:
                        valido = Regex.IsMatch(cartao, "^(34|37)") && cartao.Length == 15;
                        break;
                }
            }

            return valido ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationType = "validacartao";
            rule.ValidationParameters.Add("bandeira", Bandeira);
            yield return rule;
        }
    }
}