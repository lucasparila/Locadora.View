using Locadora.Controller.Interfaces;
using Locadora.Models;
using Locadora.Models.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utils;

namespace Locadora.Controller
{
    public class LocacaoController : ILocacaoController
    {
        public void AdicionarLocacao(Locacao locacao)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Locacao.INSERTLOCACAO, connection, transaction);
                    command.Parameters.AddWithValue("@ClienteID", locacao.cliente.ClienteID);
                    command.Parameters.AddWithValue("VeiculoID", locacao.veiculo.VeiculoID);
                    command.Parameters.AddWithValue("@DataLocacao", locacao.DataLocacao);
                    command.Parameters.AddWithValue("@DataDevolucaoPrevista", locacao.DataDevolucaoPrevista);
                    command.Parameters.AddWithValue("@DataDevolucaoReal", locacao.DataDevolucaoReal.HasValue ? (object)locacao.DataDevolucaoReal : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ValorDiaria", locacao.veiculo.categoria.Diaria);
                    command.Parameters.AddWithValue("@ValorTotal", locacao.ValorTotal);
                    command.Parameters.AddWithValue("@Multa", locacao.Multa);
                    command.Parameters.AddWithValue("@Status", locacao.Status);
                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar ao criar a locação" + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao criar a locação" + ex.Message);
                }
            }

        }

        public void AtualizarDataDevolucaoRealLocacao(Locacao locacao, DateTime? devolucao)
        {
            

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Locacao.UPDATELOCACAODEVOLUCAOREAL, connection, transaction);
                    command.Parameters.AddWithValue("@DataDevolucaoReal", devolucao ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LocacaoID", locacao.LocacaoID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar a data de devolução do veículo: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar a data de devolução do veículo: " + ex.Message);
                }
            }
        }

        public void AtualizarValorTotalLocacao(Locacao locacao)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Locacao.UPDATELOCACAOVALORTOTAL, connection, transaction);
                    command.Parameters.AddWithValue("@ValorTotal", locacao.ValorTotal);
                    command.Parameters.AddWithValue("@LocacaoID", locacao.LocacaoID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar o valor total da locação: erro ao conectar com o banco: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar o valor total da locação: " + ex.Message);
                }
            }
        }
        public void AtualizarStatusLocacao(Locacao locacao, string status)
        {
            
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Locacao.UPDATELOCACAOSTATUS, connection, transaction);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@LocacaoID", locacao.LocacaoID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar o status da locação: erro ao conectar com o banco: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar o status da locação: " + ex.Message);
                }
            }
        }

        public Locacao BuscarLocacaoPorId(Guid id)
        {
            var clienteController = new ClienteController();
            var veiculoController = new VeiculoController();
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

           
            using (SqlCommand command = new SqlCommand(Locacao.SELECTLOCACOESBYID, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@LocacaoID", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var cliente = clienteController.BuscaClientePorId(reader.GetInt32(1));
                            var veiculo = veiculoController.BuscarVeiculoId(reader.GetInt32(2));

                            var locacao = new Locacao(
                                reader.GetGuid(0),
                                cliente,
                                veiculo,
                                reader.GetDateTime(3),
                                reader.GetDateTime(4),
                                reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                reader.GetDecimal(6),
                                reader.GetDecimal(7),
                                reader.GetDecimal(8),
                                reader.GetString(9)
                             );
                            return locacao;
                        }

                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Locação não encontrada: erro no acesso ao banco" + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Locação não encontrada" + ex.Message);
                }
            }
        }
        
       

        public List<Locacao> ListarLocacoes()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            var clienteController = new ClienteController();
            var veiculoController = new VeiculoController();
            CategoriaController categoriaController = new CategoriaController();
            List<Locacao> locacoes = new List<Locacao>();
            using (SqlCommand command = new SqlCommand(Locacao.SELECTALLLOCACOES, connection))
            {
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = clienteController.BuscaClientePorId(reader.GetInt32(1));
                            var veiculo = veiculoController.BuscarVeiculoId(reader.GetInt32(2));

                            var locacao = new Locacao(
                                reader.GetGuid(0),
                                cliente,
                                veiculo,
                                reader.GetDateTime(3),
                                reader.GetDateTime(4),
                                reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                reader.GetDecimal(6),
                                reader.GetDecimal(7),
                                reader.GetDecimal(8),
                                reader.GetString(9)
                             );
                            locacoes.Add(locacao);
                        }

                        return locacoes;
                    }


                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar locações: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao listar locações: " + ex.Message);
                }
            }
        }
    }
}
