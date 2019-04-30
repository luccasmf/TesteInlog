using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteInlog.Models;

namespace TesteInlog.Repositories
{
    public interface IVeiculoRepository
    {
        Veiculo BuscarVeiculo(string chassi);
        bool InserirVeiculo(Veiculo veiculo);
        List<Veiculo> ListarVeiculos();
        bool VerificarVeiculoExistente(string chassi);
        bool RemoveVeiculo(Veiculo veiculo);
    }


    public class VeiculoRepository : IVeiculoRepository
    {
        private InLogContext _context;

        public VeiculoRepository()
        {
            _context = new InLogContext();
        }

        public bool InserirVeiculo(Veiculo veiculo)
        {
            if(_context.Veiculos.Any(x=>x.Chassi.Equals(veiculo.Chassi)))
            {
                _context.Entry(veiculo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            _context.Veiculos.Add(veiculo);

            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// Verifica se o já existe um veículo com o chassi informado
        /// </summary>
        /// <param name="chassi"></param>
        /// <returns></returns>
        public bool VerificarVeiculoExistente(string chassi)
        {
            return _context.Veiculos.Where(x => x.Chassi.Equals(chassi)).Any();
        }

        public Veiculo BuscarVeiculo(string chassi)
        {
            return _context.Veiculos.FirstOrDefault(x => x.Chassi.Equals(chassi));
        }

        public List<Veiculo> ListarVeiculos()
        {
            return _context.Veiculos.ToList();
        }

        public bool RemoveVeiculo(Veiculo veiculo)
        {
            _context.Entry(veiculo).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }
    }
}
