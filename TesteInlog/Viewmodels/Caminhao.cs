using System;
using System.Collections.Generic;
using System.Text;
using TesteInlog.Models;

namespace TesteInlog.Viewmodels
{
    public class Caminhao:Veiculo
    {
        public Caminhao()
        {
            this.NumeroPassageiros = 2;
            this.Tipo = TipoVeículo.Caminhao;
        }
    }
}
