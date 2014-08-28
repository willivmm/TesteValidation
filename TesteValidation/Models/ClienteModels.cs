using System;
using System.ComponentModel.DataAnnotations;
using TesteValidation.Helpers;

namespace TesteValidation.Models
{
    public class ClienteModels
    {
        [Display(Name = "CPF")]
        [Required(ErrorMessage = "• CPF Obrigatório")]
        [ValidaCpf(ErrorMessage = "• CPF Inválido")]
        public string Cpf { get; set; }

        [Display(Name = "Bandeira do Cartão")]
        public int Bandeira { get; set; }

        [Display(Name = "Titular")]
        [Required(ErrorMessage = "• Titular Obrigatório")]
        public string Titular { get; set; }

        [Display(Name = "Número do Cartão")]
        [Required(ErrorMessage = "• Número do Cartão Obrigatório")]
        [ValidaCartao("Bandeira", ErrorMessage = "• Número de cartão inválido")]
        public string NumeroCartao { get; set; }

        [ValidaVencimento("ValidadeAno", ErrorMessage = "• Mês de validade inválido")]
        public int ValidadeMes { get; set; }

        [ValidaVencimento("ValidadeMes", ErrorMessage = "• Ano de validade inválido")]
        public int ValidadeAno { get; set; }

        [Display(Name = "CRC")]
        [Required(ErrorMessage = "• CRC Obrigatório")]
        public int CodigoSeguranca { get; set; }

        [Display(Name = "Data de Pagamento")]
        [Required(ErrorMessage = "• Data de Pagamento Obrigatória")]
        [ValidaIntervaloDias(1, 25, ErrorMessage = "• Data fora do intervalo permitido")]
        public string DataPagamento { get; set; }

        public Decimal ValorMinimo { get; set; }
        public Decimal ValorMaximo { get; set; }

        [Display(Name = "Valor de Pagamento")]
        [Required(ErrorMessage = "• Valor Obrigatório")]
        [ValidaIntervaloValores("ValorMinimo", "ValorMaximo", ErrorMessage = "• Valor fora do limite permitido")]
        public String Valor { get; set; }
    }
}