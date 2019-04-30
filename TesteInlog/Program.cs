using System;
using TesteInlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TesteInlog.Viewmodels;
using TesteInlog.Extensions;
using TesteInlog.Repositories;
using System.Collections.Generic;

namespace TesteInlog
{
    class Program
    {
        private static IVeiculoRepository _veiculoRepository;
        private static bool _keepRunning = true;
        static void Main(string[] args)
        {
            _veiculoRepository = new VeiculoRepository();
            while (_keepRunning)
            {
                Console.WriteLine("Escolha uma opção: ");
                Console.WriteLine("1 - Inserir um veículo");
                Console.WriteLine("2 - Editar veículo existente");
                Console.WriteLine("3 - Deletar veículo");
                Console.WriteLine("4 - Listar veículos");
                Console.WriteLine("5 - Encontrar veículo por chassi");
                Console.WriteLine("0 - Sair");

                var keyPress = Console.ReadKey();

                switch (keyPress.KeyChar)
                {
                    case '0':
                        Sair();
                        Console.Clear();
                        break;
                    case '1':
                        InserirVeiculo();
                        Console.Clear();
                        break;
                    case '2':
                        EditarVeiculo();
                        Console.Clear();
                        break;
                    case '3':
                        DeletarVeiculo();
                        Console.Clear();
                        break;
                    case '4':
                        ListarVeiculos();
                        Console.Clear();
                        break;
                    case '5':
                        ProcurarVeículo();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("\n\nOpção inválida, tente novamente.\n\n");
                        break;

                }



            }

            Console.WriteLine("Hello World!");
        }


        static void InserirVeiculo()
        {
            Veiculo veiculo;
            Console.Clear();

            while (true)
            {
                string chassi = null;
                string cor = null;

                Console.WriteLine("\nSelecione o tipo de veículo: ");
                Console.WriteLine("1 - Caminhão");
                Console.WriteLine("2 - Ônibus");
                Console.WriteLine("0 - Voltar");

                var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '0':
                        return;
                    case '1':
                        veiculo = new Caminhao();
                        break;
                    case '2':
                        veiculo = new Onibus();
                        break;
                    default:
                        Console.WriteLine("\n\nOpção inválida, tente novamente.\n\n");
                        continue;
                }


                while (string.IsNullOrEmpty(chassi))
                {
                    Console.WriteLine("Digite o chassi do veículo");
                    chassi = Console.ReadLine();

                    if (string.IsNullOrEmpty(chassi))
                    {
                        Console.WriteLine("\n\nEste campo não pode permanecer em branco\n\n");
                    }
                }


                if (_veiculoRepository.VerificarVeiculoExistente(chassi))
                {
                    Console.WriteLine("\n\nJá existe um veículo com o chassi digitado\n\n");
                    continue;
                }

                while (string.IsNullOrEmpty(cor))
                {
                    Console.WriteLine("\nDigite a cor do veículo");
                    cor = Console.ReadLine();

                    if (string.IsNullOrEmpty(cor))
                    {
                        Console.WriteLine("\n\nEste campo não pode permanecer em branco\n\n");
                    }
                }


                veiculo.Chassi = chassi;
                veiculo.Cor = cor;

                try
                {
                    bool success = _veiculoRepository.InserirVeiculo(veiculo);
                    string message = success ? "Veículo salvo com sucesso." : "Não foi possível salvar seu veículo, tente novamentekd";
                    Console.WriteLine("\n\n" + message + "\n\n");
                }
                catch(Exception e)
                {
                    Console.WriteLine("Houve um erro ao tentar salvar o veículo.");
                }
            }
        }

        static void EditarVeiculo()
        {
            Console.Clear();
            Console.WriteLine("Digite o chassi do veículo desejado:");
            string chassi = Console.ReadLine();

            Veiculo v = _veiculoRepository.BuscarVeiculo(chassi);
            if (v == null)
            {
                Console.WriteLine("\nVeículo não encontrado, aperte qualquer tecla para retornar ao menu anterior.");
                Console.ReadKey();
                return;

            }

            string cor = null;

            while (string.IsNullOrEmpty(cor))
            {
                Console.WriteLine("\nDigite uma nova cor para o veículo:");

                cor = Console.ReadLine();

                if (string.IsNullOrEmpty(cor))
                {
                    Console.WriteLine("Cor não pode ser vazio, tente novamente.");
                }
            }

            v.Cor = cor;

            try
            {
                bool success = _veiculoRepository.InserirVeiculo(v);
                string message = success ? "Veículo alterado com sucesso." : "Não foi possível alterar seu veículo, tente novamentekd";
                Console.WriteLine("\n\n" + message + "\n\n");
            }
            catch
            {
                Console.WriteLine("Houve um erro ao tentar alterar o veículo.");
            }

        }

        static void DeletarVeiculo()
        {
            Console.Clear();
            Console.WriteLine("Digite o chassi do veículo a ser apagado:");
            string chassi = Console.ReadLine();

            Veiculo veiculo = _veiculoRepository.BuscarVeiculo(chassi);
            if (veiculo == null)
            {
                Console.WriteLine("\nVeículo não encontrado, aperte qualquer tecla para retornar ao menu anterior.");
                Console.ReadKey();
                return;

            }

            Console.WriteLine("\nChassi: {0}     Cor: {1}     Tipo: {2}     Quantidade de Lugares: {3}", veiculo.Chassi, veiculo.Cor, veiculo.Tipo.GetDescription(), veiculo.NumeroPassageiros);

            bool keep = true;
            while (keep)
            {


                Console.WriteLine("\nTem certeza que deseja excluir este veículo? (S/N)");
                var key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case 'S':
                    case 's':
                        try
                        {
                            if (_veiculoRepository.RemoveVeiculo(veiculo))
                            {
                                Console.WriteLine("\nVeículo removido com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("\nNão foi possível remover o veículo selecionado.");
                            }

                            Console.WriteLine("Aperte qualquer tecla para retornar ao menu anterior.");
                            Console.ReadKey();
                        }
                        catch
                        {
                            Console.WriteLine("\nFalha ao tentar remover o veículo, tente novamente mais tarde.");
                        }
                        keep = false;
                        break;
                    case 'N':
                    case 'n':
                        keep = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente");
                        break;
                }
            }
        }

        static void ListarVeiculos()
        {
            Console.Clear();

            List<Veiculo> veiculos = _veiculoRepository.ListarVeiculos();

            if (veiculos.Any())
            {
                Console.WriteLine("Encontramos os seguintes veículos: ");

                veiculos.ForEach(x => Console.WriteLine("\nChassi: {0}     Cor: {1}     Tipo: {2}     Quantidade de Lugares: {3}", x.Chassi, x.Cor, x.Tipo.GetDescription(), x.NumeroPassageiros));

            }
            else
                Console.WriteLine("\nNão encontramos nenhum veículo em nossa base.");

            Console.WriteLine("\nAperte qualquer botão para retornar à tela anterior.");
            Console.ReadKey();
            return;
        }

        static void ProcurarVeículo()
        {
            Console.Clear();
            Console.WriteLine("Digite o chassi do veículo desejado:");
            string chassi = Console.ReadLine();

            Veiculo veiculo = _veiculoRepository.BuscarVeiculo(chassi);
            if (veiculo == null)
            {
                Console.WriteLine("\nVeículo não encontrado, aperte qualquer tecla para retornar ao menu anterior.");
                Console.ReadKey();
                return;

            }

            Console.WriteLine("Chassi: {0}     Cor: {1}     Tipo: {2}     Quantidade de Lugares: {3}", veiculo.Chassi, veiculo.Cor, veiculo.Tipo.GetDescription(), veiculo.NumeroPassageiros);
            Console.WriteLine();
            Console.WriteLine("Aperte qualquer tecla para retornar ao menu anterior.");
            Console.ReadKey();
            return;
        }

        static void Sair()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Deseja mesmo sair? (S/N)");

                var key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case 'S':
                    case 's':
                        _keepRunning = false;
                        return;
                    case 'N':
                    case 'n':
                        return;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente");
                        break;
                }
            }
        }

    }

}
