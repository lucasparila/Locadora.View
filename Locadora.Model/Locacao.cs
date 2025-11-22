using Locadora.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Locacao
    {
        public readonly static string INSERTLOCACAO = @"INSERT INTO tblLocacoes(ClienteID, VeiculoID, DataLocacao, DataDevolucaoPrevista, DataDevolucaoReal, 
                                                       ValorDiaria, ValorTotal, Multa, Status)
                                                       VALUES
                                                       (@ClienteID, @VeiculoID, @DataLocacao, @DataDevolucaoPrevista, @DataDevolucaoReal, 
                                                       @ValorDiaria, @ValorTotal, @Multa, @Status);";

        public readonly static string SELECTALLLOCACOES = @"SELECT LocacaoID, ClienteID, VeiculoID, DataLocacao, DataDevolucaoPrevista, DataDevolucaoReal, 
                                                          ValorDiaria, ValorTotal, Multa, Status
                                                          FROM tblLocacoes;";
                                                         
        public Guid LocacaoID { get; private set; }
        public Cliente cliente { get; private set; }
        public  Veiculo veiculo { get; private set; }
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; } 
        public decimal ValorDiaria { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal Multa { get; private set; }
        public string Status { get; private set; }

        public Locacao(Cliente cliente, Veiculo veiculo, decimal valorDiaria, int diasLocacao)
        {
            this.cliente = cliente;
            this.veiculo = veiculo;
            this.DataLocacao = DateTime.Now;
            this.DataDevolucaoPrevista = DateTime.Now.AddDays(diasLocacao);
            this.DataDevolucaoReal = null;
            this.ValorDiaria = valorDiaria;
            this.ValorTotal = valorDiaria * diasLocacao;
            this.Multa = 0.5m * (decimal)this.veiculo.categoria.Diaria;
            this.Status = "Ativa";
        }

        public Locacao(Guid locacaoID, Cliente cliente, Veiculo veiculo, DateTime dataLocacao, 
            DateTime dataDevolucaoPrevista, DateTime? dataDevolucaoReal, decimal valorDiaria, 
            decimal valorTotal, decimal multa, string status)
        {
            LocacaoID = locacaoID;
            this.cliente = cliente;
            this.veiculo = veiculo;
            DataLocacao = dataLocacao;
            DataDevolucaoPrevista = dataDevolucaoPrevista;
            DataDevolucaoReal = dataDevolucaoReal;
            ValorDiaria = valorDiaria;
            ValorTotal = valorTotal;
            Multa = multa;
            Status = status;
        }

        public void setDataDevolucaoReal(DateTime dataDevolucao)
        {
            this.DataDevolucaoReal = dataDevolucao;
        }

        public override string ToString()
        {
            return $"Id: {this.LocacaoID}\n" +
                   $"Nome: {this.cliente.Nome}\n" +
                   $"Modelo: {this.veiculo.Modelo}\n" +
                   $"Data de Locação: {DataLocacao}\n" +
                   $"Data de Devolução Prevista: {DataDevolucaoPrevista}\n" +
                   $"Data de Devolução Real: {DataDevolucaoReal}\n" +
                   $"Valor da Diária: {ValorDiaria:C}\n" +
                   $"Valor Total: {ValorTotal:C}\n" +
                   $"Multa: {Multa:C} + valor da diária por dia de atraso\n" +
                   $"Status: {Status}\n";
        }

    }
}
