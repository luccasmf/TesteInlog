using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteInlog.Models;

namespace TesteInlog.Repositories
{
    public interface IVeiculoRepository
    {
        /// <summary>
        /// Busca um veículo com base em um chassi informado
        /// </summary>
        /// <param name="chassi"></param>
        /// <returns></returns>
        Veiculo BuscarVeiculo(string chassi);
        /// <summary>
        /// Persiste na base um veículo ou sua alteração
        /// </summary>
        /// <param name="veiculo"></param>
        /// <returns></returns>
        bool InserirVeiculo(Veiculo veiculo);
        /// <summary>
        /// Retorna uma lista de todos os veículos cadastrados
        /// </summary>
        /// <returns></returns>
        List<Veiculo> ListarVeiculos();
        /// <summary>
        /// Verifica se já existe algum veículo com o chassi informado
        /// </summary>
        /// <param name="chassi"></param>
        /// <returns></returns>
        bool VerificarVeiculoExistente(string chassi);
        /// <summary>
        /// Deleta veículos da base
        /// </summary>
        /// <param name="veiculo"></param>
        /// <returns></returns>
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
           

            _context.Veiculos.Add(veiculo);

            return _context.SaveChanges() > 0;
        }

        public bool EditarVeiculo(Veiculo veiculo)
        {
            if (_context.Veiculos.Any(x => x.Chassi.Equals(veiculo.Chassi)))
            {
                _context.Entry(veiculo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            return _context.SaveChanges() > 0;
        }

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
