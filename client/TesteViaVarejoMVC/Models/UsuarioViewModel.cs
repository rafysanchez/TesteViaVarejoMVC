using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteViaVarejoMVC.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string EMail { get; set; }
        public string SenhaHash { get; set; }

        public bool EhValido()
        {
            return !String.IsNullOrEmpty(this.Nome) && !String.IsNullOrEmpty(this.EMail) && !String.IsNullOrEmpty(this.SenhaHash);
        }
    }
}
