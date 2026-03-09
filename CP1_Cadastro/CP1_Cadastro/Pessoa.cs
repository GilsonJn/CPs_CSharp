using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP1_Cadastro
{
    internal class Pessoa
    {
        public string Nome { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
