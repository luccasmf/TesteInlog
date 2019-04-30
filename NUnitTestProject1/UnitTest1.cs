using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteInlog.Models;
using TesteInlog.Repositories;
using TesteInlog.Viewmodels;

namespace Tests
{
    public class Tests
    {
        IVeiculoRepository _veiculoRepository;
        [SetUp]
        public void Setup()
        {
            _veiculoRepository = new VeiculoRepository();

        }

        [Test]
        public void Test1()
        {
            Veiculo veiculo = new Onibus();
            veiculo.Chassi = "abc";
            veiculo.Cor = "azul";
            bool insert = _veiculoRepository.InserirVeiculo(veiculo);

            if (insert)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void Test2()
        {
            Veiculo veiculo = new Onibus();
            veiculo.Chassi = "cde";
            veiculo.Cor = "azul";

            bool insert = _veiculoRepository.InserirVeiculo(veiculo);
            List<Veiculo> veiculos = _veiculoRepository.ListarVeiculos();


            if (veiculos.Any())
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void Test3()
        {
            Veiculo veiculo = new Caminhao();
            veiculo.Chassi = "abc";
            veiculo.Cor = "verde";
            try
            {
                bool insert = _veiculoRepository.InserirVeiculo(veiculo);

                if (!insert)
                    Assert.Pass();
                else
                    Assert.Fail();
            }
            catch(ArgumentException e)
            {
                Assert.Pass();

            }

        }

        [Test]
        public void Test4()
        {
            Veiculo veiculo = new Onibus();
            veiculo.Chassi = "abc1";
            veiculo.Cor = "azul";
           _veiculoRepository.InserirVeiculo(veiculo);
            Veiculo veiculo2 = new Caminhao();
            veiculo2.Chassi = "abc2";
            veiculo2.Cor = "verde";
            _veiculoRepository.InserirVeiculo(veiculo2);

            Veiculo v = _veiculoRepository.BuscarVeiculo("abc1");
            Veiculo v2 = _veiculoRepository.BuscarVeiculo("abc2");


            if (v != null && v2 != null)
                Assert.Pass();
            else
                Assert.Fail();

        }
    }
}