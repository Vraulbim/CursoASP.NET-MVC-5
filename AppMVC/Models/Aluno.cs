using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppMVC.Models
{
    public class Aluno
    {
        [Key]
        public int id { get; set; }

        [DisplayName("Nome Completo")]
        [Required(ErrorMessage ="{0} é um campo obrigatório")]
        [MaxLength(100, ErrorMessage ="Número máximo de caracteres atingido(100)")]
        public string name { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage ="É necessário informar um {0}")]
        [EmailAddress(ErrorMessage ="E-mail inválido, verifique e tente novamente")]
        public string email { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage ="{0} não pode ser nulo")]
        [MaxLength(11, ErrorMessage ="{0} deve conter 11 caracteres")]
        public string cpf { get; set; }

        public DateTime data_matricula { get; set; }

        public bool ativo { get; set; }

        public string descricao { get; set; }
    }
}