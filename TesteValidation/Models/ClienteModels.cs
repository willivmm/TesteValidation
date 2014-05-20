using System.ComponentModel.DataAnnotations;
using TesteValidation.Helpers;

namespace TesteValidation.Models
{
    public class ClienteModels
    {
        [Display(Name = "CPF")]
        [Required(ErrorMessage = "CPF Obrigatório")]
        [ValidaCpf(ErrorMessage = "CPF Inválido")]
        public string Cpf { get; set; }
    }
}