using System;
using System.Collections.Generic;
using System.Text;
using TesteInlog.Models;

namespace TesteInlog.Viewmodels
{
    public class Onibus:Veiculo
    {
        public Onibus()
        {
            this.NumeroPassageiros = 42;
            this.Tipo = TipoVeículo.Onibus;
        }
    }
}
