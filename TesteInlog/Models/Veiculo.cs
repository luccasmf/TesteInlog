using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TesteInlog.Models
{
    public class Veiculo
    {
        [Key]
        [Required]
        public string Chassi { get; set; }
        [Required]
        public TipoVeículo Tipo { get; set; }
        [Required]
        public byte NumeroPassageiros { get; protected set; }
        [Required]
        public string Cor { get; set; }

    }

    public enum TipoVeículo : byte
    {
        [Description("Caminhão")]
        Caminhao = 1,
        [Description("Ônibus")]
        Onibus = 2
    }
}
